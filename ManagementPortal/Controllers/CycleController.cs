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
    public class CycleController : Controller
    {
        private readonly ICycleService _cycleService;
        private readonly IFormService _formService;
        private readonly IApplicationService _applicationService;
        private readonly MapperService _mapperService;
        public CycleController(ICycleService cycleService, IFormService formService, IApplicationService applicationService, MapperService mapperService)
        {
            _cycleService = cycleService;
            _formService = formService;
            _applicationService = applicationService;
            _mapperService = mapperService;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var model = GetDashboardData();
            return View(model);
        }

        [HttpGet]
        public IActionResult GetCycleList()
        {
            var model = GetDashboardData();
            return PartialView("_CycleList", model);
        }

        private List<CycleViewModel> GetDashboardData()
        {
            var dbos = _cycleService.GetAll().ToList();

            var model = _mapperService.Map(dbos);

            return model;
        }

        [HttpGet]
        [Authorize(Policy = "CanCrud")]
        [Route("/Cycle/Edit/{cycleId}")]
        public IActionResult Edit(int cycleId)
        {
            if(cycleId == 0)
            {
                return PartialView("_Edit", new CycleViewModel {
                    StartDateTime = DateTime.Now.Date,
                    EndDateTime = DateTime.Now.AddMonths(6).Date
                });
            }

            var cycle = _cycleService.Get(cycleId);
            var dto = new CycleViewModel
            {
                Id = cycleId,
                Title = cycle.Title,
                StartDateTime = cycle.StartDateTime,
                EndDateTime = cycle.EndDateTime
            };
            return PartialView("_Edit", dto);
        }

        [HttpPost]
        [Authorize(Policy = "CanCrud")]
        public IActionResult Edit([FromForm] CycleViewModel dto)
        {
            try
            {
                if(dto.Id == 0)
                {
                    _cycleService.Add(new Cycle
                    {
                        Title = dto.Title,
                        StartDateTime = dto.StartDateTime,
                        EndDateTime = dto.EndDateTime
                    });
                    return Json(new { success = true });
                }

                _cycleService.Update(dto.Id, dto.Title, dto.StartDateTime, dto.EndDateTime);

                return Json(new {success = true});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Route("/Cycle/Delete/{cycleId}")]
        [Authorize(Policy = "CanCrud")]
        public IActionResult Delete(int cycleId)
        {
            try
            {
                _cycleService.Delete(cycleId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }
    }
}
