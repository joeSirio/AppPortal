using ApplicantPortal.Models;
using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApplicantPortal.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IHttpContextAccessor _contextAccessor;
        public DashboardController(IApplicationService applicationService, IHttpContextAccessor contextAccessor)
        {
            _applicationService = applicationService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new List<FormViewModel>();
                var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var applications = _applicationService.GetAllByUser(userId);

                foreach(var app in applications)
                {
                    if(!model.Any(a => a.Id == app.FormId))
                        model.Add(Map(app.Form));

                    model.FirstOrDefault(f => f.Id == app.FormId).Applications.Add(Map(app));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult NewApplication(int formId)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var applicationId = _applicationService.NewApplication(formId, userId);
            var model = Map(_applicationService.Get(applicationId));
            return PartialView("_Application", model);
        }

        private FormViewModel Map(Form dbo)
        {
            return new FormViewModel
            {
                Id = dbo.Id,
                Title = dbo.Title,
                BeginDate = dbo.Cycle.StartDateTime,
                EndDate = dbo.Cycle.EndDateTime
            };
        }

        private ApplicationStatusViewModel Map(Application dbo)
        {
            return new ApplicationStatusViewModel
            {
                Id = dbo.Id,
                Title = dbo.Title,
                Status = dbo.Status
            };
        }
    }
}
