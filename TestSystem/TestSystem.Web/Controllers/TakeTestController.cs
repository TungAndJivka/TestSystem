using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;
using TestSystem.Web.Models.TakeTestViewModels;

namespace TestSystem.Web.Controllers
{
    public class TakeTestController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITestService testService;
        private readonly IAnswerService answerService;
        private readonly IMappingProvider mapper;
        private readonly IResultService resultService;
        private readonly ISaver saver;

        public TakeTestController(UserManager<User> userManager, ITestService testService, IAnswerService answerService, IResultService resultService, IMappingProvider mapper, ISaver saver)
        {
            this.userManager = userManager;
            this.testService = testService;
            this.answerService = answerService;
            this.resultService = resultService;
            this.mapper = mapper;
            this.saver = saver;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var testDto = this.testService.GetRandomTestByCategory(id);
            var questions = mapper.ProjectTo<QuestionViewModel>(testDto.Questions.AsQueryable()).ToList();
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            DateTime startedOn = DateTime.Now;

            var model = new IndexViewModel()
            {
                UserId = userId,
                TestId = testDto.Id,
                TestName = testDto.TestName,
                Duration = testDto.Duration,
                CategoryName = id,
                Questions = questions,
                StartedOn = startedOn
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var test = testService.GetFullTestInfo(model.TestId);

                Guid userTestId = Guid.NewGuid();

                int correctAnswers = 0;
                //int allQuestionsCount = testService.GetQuestionsCount(model.TestId);
                int allQuestionsCount = test.Questions.Count();

                var answeredQuestions = new List<AnsweredQuestionDto>();
                foreach (QuestionViewModel question in model.Questions)
                {
                    string selectedAnswerId = question.SelectedAnswer;
                    if (selectedAnswerId == null)
                    {
                        continue;
                    }

                    //AnswerDto answer = this.answerService.GetById(selectedAnswerId);
                    AnswerDto answer = test.Questions
                        .Where(q => q.Id == question.Id)
                        .FirstOrDefault()
                        .Answers
                        .Where(a => a.Id == selectedAnswerId)
                        .FirstOrDefault();

                    if (answer.IsCorrect)
                    {
                        correctAnswers++;
                    }

                    var answeredQuestion = new AnsweredQuestionDto()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserTestId = userTestId.ToString(),
                        AnswerId = selectedAnswerId
                    };

                    answeredQuestions.Add(answeredQuestion);
                }

                double score = (100.0*correctAnswers)/allQuestionsCount;

                var userTest = new UserTestDto()
                {
                    Id = userTestId,
                    UserId = model.UserId,
                    TestId = model.TestId,
                    StartTime = model.StartedOn,
                    SubmittedOn = DateTime.Now,
                    AnsweredQuestions = answeredQuestions,
                    Score = score
                };

                this.resultService.AddResult(userTest);
                this.saver.SaveChanges();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}