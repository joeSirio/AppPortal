using AppPortal.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Interfaces;

public interface IFormService
{
    Form? Get(int id);
    IQueryable<Form> GetAllPerCycle(int cycleId);
    int Add(Form dbo);
    void Update(Form model);
    void Delete(int Id);
}
