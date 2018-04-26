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
        private readonly IUserService userService;
        private readonly ISaver saver;

        public TakeTestController(UserManager<User> userManager, ITestService testService, IAnswerService answerService, IResultService resultService, IMappingProvider mapper, IUserService userService, ISaver saver)
        {
            this.userManager = userManager;
            this.testService = testService;
            this.answerService = answerService;
            this.resultService = resultService;
            this.userService = userService;
            this.mapper = mapper;
            this.saver = saver;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id) // id => categoryName
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            int check = resultService.CheckForTakenTest(user.Id, id);
            TestDto testDto = null;

            if (check == 3)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            if (check == 2)
            {
                testDto = this.userService.GetTestFromCategory(user.Id, id);
            }
            else
            {
                testDto = this.testService.GetRandomTestByCategory(id);
            }

            var questions = mapper.ProjectTo<QuestionViewModel>(testDto.Questions.AsQueryable()).ToList();

            var model = new IndexViewModel()
            {
                UserId = user.Id,
                TestId = testDto.Id,
                TestName = testDto.TestName,
                Duration = testDto.Duration,
                CategoryName = id,
                Questions = questions,
                StartedOn = DateTime.Now
            };

            if (check == 1)
            {
                var resultDto = mapper.MapTo<UserTestDto>(model);
                resultDto.Id = Guid.NewGuid();
                resultService.AddResult(resultDto);
                this.saver.SaveChanges();
            }

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

                var answeredQuestions = new List<AnsweredQuestionDto>();
                foreach (QuestionViewModel question in model.Questions)
                {
                    if (question.SelectedAnswer == null) { continue; }

                    AnswerDto answer = test.Questions
                        .Where(q => q.Id == question.Id)
                        .FirstOrDefault()
                        .Answers
                        .Where(a => a.Id == question.SelectedAnswer)
                        .FirstOrDefault();

                    if (answer.IsCorrect)
                    {
                        correctAnswers++;
                    }

                    var answeredQuestion = new AnsweredQuestionDto()
                    {
                        Id = Guid.NewGuid(),
                        UserTestId = userTestId,
                        AnswerId = new Guid(question.SelectedAnswer)
                    };

                    answeredQuestions.Add(answeredQuestion);
                }

                double score = (100.0*correctAnswers)/test.Questions.Count();

                var userTest = resultService.GetUserTest(model.UserId, model.TestId);
                userTest.Score = score;
                userTest.SubmittedOn = DateTime.Now;
                userTest.AnsweredQuestions = answeredQuestions;

                this.resultService.Update(userTest);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}