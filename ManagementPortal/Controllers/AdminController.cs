using AppPortal.Data.Core.Enums;
using AppPortal.Data.Interfaces;
using ManagementPortal.Models;
using ManagementPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementPortal.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;
        private readonly MapperService _mapper;

        public AdminController(IUserService userService, IApplicationService applicationService,MapperService mapperService)
        {
            _userService = userService;
            _applicationService = applicationService;
            _mapper = mapperService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManager()
        {
            return View();
        }

        public IActionResult ManageApplicationStatus()
        {
            return View();
        }

        public IActionResult GetUsers()
        {
            return Json(_mapper.Map(_userService.GetAllUsers()));
        }

        [HttpGet]
        [Route("/Admin/ManageUser/{id}")]
        public async Task<IActionResult> ManageUser(int id)
        {
            if(id == 0){
                return View(new UserViewModel());
            }
            var model = _mapper.Map(_userService.Get(id));
            model.Roles = await _userService.GetRolesForUser(id);

            ViewData["Roles"] = _mapper.Map(_userService.getAllRoles());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUser([FromForm] UserViewModel dto)
        {
            try
            {
                if (dto.Id == 0)
                    _userService.AddUser(_mapper.Map(dto));
                else
                    _userService.UpdateUser(_mapper.Map(dto));

                await _userService.updateUserRole(dto.Id, dto.Roles);
                return Json(new {success = true});
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult ManageRoles()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            return Json(_mapper.Map(_userService.getAllRoles()));
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromForm] RoleViewModel dto)
        {
            try
            {
                await _userService.AddRole(_mapper.Map(dto));
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromForm] int id)
        {
            try
            {
                await _userService.RemoveRole(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult GetApplications()
        {
            return Json(_mapper.Map(_applicationService.GetAll().Where(w => w.Status != ApplicationStatus.Assigned).ToList()));
        }

        [HttpGet]
        public IActionResult TransitionApplication(int id)
        {
            var model = _mapper.Map(_applicationService.Get(id));
            return PartialView("_Transition", model);
        }

        [HttpPost]
        public IActionResult TransitionApplication([FromForm] int id, ApplicationStatus status)
        {
            try
            {
                _applicationService.TransitionApplication(id, status);
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
