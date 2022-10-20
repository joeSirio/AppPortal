using AppPortal.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Interfaces
{
    public interface ICycleService
    {
        Cycle? Get(int id);
        IQueryable<Cycle> GetAll();
        void Add(Cycle dbo);
        void Update(int Id, string? Title, DateTime StartDateTime, DateTime EndDateTime);
        void Delete(int id);
    }
}
