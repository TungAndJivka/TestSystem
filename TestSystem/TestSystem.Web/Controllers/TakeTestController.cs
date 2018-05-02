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
using TestSystem.Services;
using TestSystem.Services.Contracts;
using TestSystem.Web.Models.TakeTestViewModels;

namespace TestSystem.Web.Controllers
{
    public class TakeTestController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITestService testService;
        private readonly IMappingProvider mapper;
        private readonly IResultService resultService;
        private readonly IUserService userService;
        private readonly ISaver saver;

        public TakeTestController(UserManager<User> userManager, ITestService testService, IAnswerService answerService, IResultService resultService, IMappingProvider mapper, IUserService userService, ISaver saver)
        {
            this.userManager = userManager;
            this.testService = testService;
            this.resultService = resultService;
            this.userService = userService;
            this.mapper = mapper;
            this.saver = saver;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id) // id => categoryName
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            StatusType check = resultService.CheckForTakenTest(user.Id, id);
            TestDto testDto = null;
            var startTime = DateTime.Now;

            if (check == StatusType.TestSubmitted)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            if (check == StatusType.TestNotStarted)
            {
                testDto = this.resultService.GetTestFromCategory(user.Id, id);
                startTime = (DateTime)resultService.GetUserTest(user.Id, testDto.Id).StartTime;
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
                StartedOn = startTime
            };

            if (check == StatusType.TestNotStarted)
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
                UserTestDto userTest = null;

                if (!CheckForValidExecutionTime(model))
                {
                    userTest = EvaluateInvalid(model);
                    this.resultService.Update(userTest);
                    return View("InvalidExecutionTime");
                }

                userTest = Evaluate(model);
                this.resultService.Update(userTest);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        private bool CheckForValidExecutionTime(IndexViewModel model)
        {
            if (model.StartedOn + model.Duration > DateTime.Now)
            {
                return true;
            }

            return false;
        }

        private UserTestDto Evaluate(IndexViewModel model)
        {
            var test = testService.GetFullTestInfo(model.TestId);
            var answeredQuestions = new List<AnsweredQuestionDto>();
            int correctAnswers = 0;

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
                    UserTestId = Guid.NewGuid(),
                    AnswerId = new Guid(question.SelectedAnswer)
                };

                answeredQuestions.Add(answeredQuestion);
            }

            double score = (100.0 * correctAnswers) / test.Questions.Count();
            var userTest = resultService.GetUserTest(model.UserId, model.TestId);
            userTest.Score = score;
            userTest.SubmittedOn = DateTime.Now;
            userTest.AnsweredQuestions = answeredQuestions;

            return userTest;
        }

        private UserTestDto EvaluateInvalid(IndexViewModel model)
        {
            var test = testService.GetFullTestInfo(model.TestId);
            var answeredQuestions = new List<AnsweredQuestionDto>();
            var userTest = resultService.GetUserTest(model.UserId, model.TestId);

            userTest.Score = 0.0;
            userTest.SubmittedOn = DateTime.Now;
            userTest.AnsweredQuestions = answeredQuestions;

            return userTest;
        }
    }
}