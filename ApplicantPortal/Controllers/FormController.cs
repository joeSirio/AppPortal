using ApplicantPortal.Models;
using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplicantPortal.Controllers
{
    public class FormController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IFormService _formService;
        private readonly IFormQuestionService _formQuestionService;
        private readonly IHttpContextAccessor _contextAccessor;
        public FormController(IApplicationService applicationService, IFormService formService, IFormQuestionService formQuestionService, IHttpContextAccessor contextAccessor)
        {
            _applicationService = applicationService;
            _formService = formService;
            _formQuestionService = formQuestionService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Edit(int id)
        {
            var model = Map(_applicationService.Get(id));
            model.Questions = Map(_formQuestionService.GetAllByForm(model.FormId));
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit([FromBody] ApplicationViewModel dto)
        {
            try
            {
                _applicationService.SubmitApplication(Map(dto));
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ApplicationViewModel Map(Application dbo)
        {
            return new ApplicationViewModel
            {
                Id = dbo.Id,
                FormId = dbo.FormId,
                Title = dbo.Title,
                Status = dbo.Status
            };
        }

        public List<QuestionViewModel> Map(List<FormQuestion> dbos)
        {
            var dtos = new List<QuestionViewModel>();
            foreach(var dbo in dbos)
            {
                dtos.Add(Map(dbo));
            }
            return dtos;
        }

        public QuestionViewModel Map(FormQuestion dbo)
        {
            return new QuestionViewModel
            {
                Id = dbo.Id,
                AnswerOptions = dbo.AnswerOptions.Split("|").ToList(),
                Question = dbo.Question,
                Type = dbo.Type
            };
        }

        public Application Map(ApplicationViewModel dto)
        {
            return new Application
            {
                Id = dto.Id,
                Title = dto.Title,
                Answers = Map(dto.Questions, dto.Id)
            };
        }

        public List<Answer> Map(List<QuestionViewModel> dtos, int applicationId)
        {
            var dbo = new List<Answer>();
            foreach(var dto in dtos)
            {
                dbo.Add(new Answer
                {
                    ApplicationId = applicationId,
                    QuestionId = dto.Id,
                    Text = dto.Answer
                });
            }

            return dbo;
        }
    }
}
