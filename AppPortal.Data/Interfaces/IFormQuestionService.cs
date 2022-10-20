using AppPortal.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Interfaces
{
    public interface IFormQuestionService
    {
        FormQuestion Get(int Id);
        List<FormQuestion> GetAllByForm(int Id);
        void AddRange(List<FormQuestion> dbos);
        void UpdateRange(List<FormQuestion> dbos);
        void Delete(int Id);
    }
}
