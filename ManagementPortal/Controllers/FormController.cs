using AppPortal.Data.Core.Enums;
using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using ManagementPortal.Models;
using ManagementPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagementPortal.Controllers;

[Authorize(Policy = "CanViewAll")]
public class FormController : Controller
{
    private readonly IFormService _formService;
    private readonly IFormQuestionService _formQuestionService;
    private readonly MapperService _mapper;
    public FormController(IFormService formService, IFormQuestionService formQuestionService, MapperService mapperService)
    {
        _formService = formService;
        _formQuestionService = formQuestionService;
        _mapper = mapperService;
    }

    [HttpGet]
    [Route("Form/{Id}")]
    public IActionResult Index(int id)
    {
        var form = _formService.Get(id);
        form.Questions = _formQuestionService.GetAllByForm(id);
        return View(_mapper.Map(form));
    }

    [HttpGet]
    [Route("Form/Add/{cycleId}")]
    [Authorize(Policy = "CanCrud")]
    public IActionResult Add(int cycleId)
    {
        var dto = new FormViewModel
        {
            CycleId = cycleId
        };

        IList<SelectListItem> list = Enum.GetValues(typeof(QuestionType))
            .Cast<QuestionType>()
            .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() })
            .ToList();

        ViewData["QuestonTypeList"] = list;
        

        return View("Edit", dto);
    }

    [HttpGet]
    [Route("Form/Edit/{Id}")]
    [Authorize(Policy = "CanCrud")]
    public IActionResult Edit(int Id)
    {
        var dbo = _formService.Get(Id);
        dbo.Questions = _formQuestionService.GetAllByForm(Id);
        var model = _mapper.Map(dbo);

        IList<SelectListItem> list = Enum.GetValues(typeof(QuestionType))
            .Cast<QuestionType>()
            .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() })
            .ToList();

        ViewData["QuestonTypeList"] = list;
        
        return View(model);
    }

    [HttpPost]
    [Authorize(Policy = "CanCrud")]
    public IActionResult Edit([FromBody] FormViewModel model)
    {
        try
        {
            var dbo = _mapper.Map(model);

            if (dbo.Id == 0)
            {
                _formService.Add(dbo);
                return Json(new { success = true });
            }
            else
            {
                _formService.Update(dbo);
                _formQuestionService.UpdateRange(dbo.Questions);
                EditForm(model); // TODO FIX
            }

            return Json(new { success = true });
        }
        catch (Exception)
        {

            throw;
        }
    }

    [Authorize(Policy = "CanCrud")]
    public void EditForm(FormViewModel model)
    {

    }

    [HttpPost]
    [Route("Form/Delete/{Id}")]
    [Authorize(Policy = "CanCrud")]
    public IActionResult Delete(int Id)
    {
        try
        {
            _formService.Delete(Id);
            return Json(new { success = true});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [Route("Form/DeleteQuestion/{Id}")]
    [Authorize(Policy = "CanCrud")]
    public IActionResult DeleteQuestion(int Id)
    {
        try
        {
            _formQuestionService.Delete(Id);
            return Json(new { success = true });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    [Route("Form/AddQuestion")]
    [Authorize(Policy = "CanCrud")]
    public IActionResult AddQuestion()
    {
        return PartialView("_Question", new FormQuestionViewModel());
    }
}
