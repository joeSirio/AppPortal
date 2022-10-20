using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using AppPortal.Data;

namespace AppPortal.Data.Services
{
    public class FormQuestionService: IFormQuestionService
    {
        private readonly DataContext _dataContext;
        public FormQuestionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public FormQuestion Get(int Id)
        {
            try
            {
                return _dataContext.FormQuestions.Find(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<FormQuestion> GetAllByForm(int Id)
        {
            try
            {
                return _dataContext.FormQuestions.Where(w => w.FormId == Id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Add(FormQuestion dbo)
        {
            try
            {
                _dataContext.FormQuestions.Add(dbo);
                _dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddRange(List<FormQuestion> dbos)
        {
            try
            {
                _dataContext.FormQuestions.AddRange(dbos);
                _dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateRange(List<FormQuestion> dbos)
        {
            try
            {
                foreach (var question in dbos)
                {
                    if (question.Id == 0)
                        _dataContext.FormQuestions.Add(question);
                    else
                    {
                        var dbo = _dataContext.FormQuestions.Find(question.Id);
                        dbo.Type = question.Type;
                        dbo.AnswerOptions = question.AnswerOptions;
                        dbo.Question = question.Question;
                        _dataContext.FormQuestions.Update(dbo);
                    }

                    _dataContext.SaveChanges();
                }
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
                var dbo = Get(Id);
                _dataContext.FormQuestions.Remove(dbo);
                _dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
