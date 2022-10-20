using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using AppPortal.Data;

namespace AppPortal.Data.Services;

public class FormService: IFormService
{
    private readonly DataContext _dataContext;
    public FormService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Form? Get(int id)
    {
        try
        {
            return _dataContext.Forms.Find(id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public IQueryable<Form> GetAllPerCycle(int cycleId)
    {
        try
        {
            return _dataContext.Forms.Where(w => w.CycleId == cycleId);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public int Add(Form dbo)
    {
        try
        {
            _dataContext.Forms.Add(dbo);
            _dataContext.SaveChanges();
            return dbo.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Update(Form model)
    {
        try
        {
            var dbo = _dataContext.Forms.Find(model.Id);
            dbo.Title = model.Title;
            _dataContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Delete(int Id)
    {
        try
        {
            var form = Get(Id);
            _dataContext.FormQuestions.RemoveRange(form.Questions);
            _dataContext.Forms.Remove(form);
            _dataContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
