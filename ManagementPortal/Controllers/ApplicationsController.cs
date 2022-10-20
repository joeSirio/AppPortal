using AppPortal.Data;
using AppPortal.Data.Core.Enums;
using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using ManagementPortal.Models;
using ManagementPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementPortal.Controllers
{
    [Authorize(Policy = "CanViewAll")]
    public class ApplicationsController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserService _userService;
        private readonly MapperService _mapper;
        public ApplicationsController(IApplicationService applicationService, IUserService userService, MapperService mapperService)
        {
            _applicationService = applicationService;
            _userService = userService;
            _mapper = mapperService;
        }

        [HttpGet]
        [Route("/Applications/{formId}/{status}")]
        public IActionResult Index(int formId, ApplicationStatus status)
        {
            try
            {
                var model = new Tuple<int, ApplicationStatus>(formId, status);

                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("/Applications/GetApplications/{formId}/{status}")]
        public IActionResult GetApplications(int formId, ApplicationStatus status)
        {
            try
            {
                var model = new List<ApplicationViewModel>();

                if (status == ApplicationStatus.Assigned)
                    model = _mapper.Map(_applicationService.GetAll(formId));
                else
                    model = _mapper.Map(_applicationService.GetAll(formId, status));

                foreach(var item in model)
                {
                    item.UserEmail = _userService.Get(item.UserId).UserName;
                }

                return Json(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Authorize(Policy = "CanAssign")]
        public IActionResult Applicants()
        {
            return PartialView("_Applicants");
        }

        [HttpGet]
        [Authorize(Policy = "CanAssign")]
        public IActionResult InviteApplicant()
        {
            return PartialView("_InviteApplicant");
        }

        [HttpPost]
        [Authorize(Policy = "CanAssign")]
        public IActionResult InviteApplicant(int formId, string email)
        {
            try
            {
                _applicationService.InviteApplicant(formId, email);

                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Authorize(Policy = "CanAssign")]
        public IActionResult AssignUsers(List<int> applicants, int formId)
        {
            try
            {
                _applicationService.AssignUsers(applicants, formId);
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Authorize(Policy = "CanAssign")]
        [Route("/Applications/GetApplicantList/{formId}")]
        public IActionResult GetApplicantList(int formId)
        {
            try
            {
                var model = _mapper.Map(_applicationService.GetUnassignedUsers(formId), false);
                var assignedUsers = _mapper.Map(_applicationService.GetAssignedUsers(formId), true);

                model.AddRange(assignedUsers);

                return Json(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Authorize(Policy = "CanTransition")]
        public IActionResult Transition(int id)
        {
            try
            {
                var dbo = _applicationService.Get(id);
                var model = _mapper.Map(dbo);

                model.Answers = _mapper.Map(_applicationService.GetAnswers(id));

                return PartialView("_Transition", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Authorize(Policy = "CanTransition")]
        public IActionResult ApproveApplication([FromForm] int id)
        {
            try
            {
                _applicationService.TransitionApplication(id, ApplicationStatus.Accepted);

                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Authorize(Policy = "CanTransition")]
        public IActionResult RejectApplication([FromForm] int id)
        {
            try
            {
                _applicationService.TransitionApplication(id, ApplicationStatus.Denied);

                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
