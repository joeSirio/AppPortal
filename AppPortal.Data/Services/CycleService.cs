using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using AppPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace AppPortal.Data.Services;

public class CycleService: ICycleService
{
    private readonly DataContext _dataContext;
    public CycleService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public Cycle? Get(int id)
    {
        return _dataContext.Cycles.Find(id);
    }

    public IQueryable<Cycle> GetAll()
    {
        return _dataContext.Cycles.Include(i => i.Forms).ThenInclude(i => i.Applications);
    }

    public void Add(Cycle dbo)
    {
        try
        {
            _dataContext.Cycles.Add(dbo);
            _dataContext.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Update(int id, string? title, DateTime startDateTime, DateTime endDateTime)
    {
        try
        {
            var dbo = Get(id);
            dbo.Title = title;
            dbo.StartDateTime = startDateTime;
            dbo.EndDateTime = endDateTime;
            _dataContext.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Delete(int id)
    {
        try
        {
            var dbo = Get(id);
            _dataContext.Cycles.Remove(dbo);
            _dataContext.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
