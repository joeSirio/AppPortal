using AppPortal.Data.Core.Enums;
using AppPortal.Data.Core.Models;
using AppPortal.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppPortal.Data.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        public ApplicationService(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager; 
        }

        public Application Get(int id)
        {
            return _dataContext.Applications.Find(id);
        }

        public IQueryable<Application> GetAll()
        {
            return _dataContext.Applications.Include(i => i.User);
        }

        public List<Application> GetAll(int formId)
        {
            return _dataContext.Applications.Where(w => w.FormId == formId).ToList();
        }

        public List<Application> GetAll(int formId, ApplicationStatus status)
        {
            return _dataContext.Applications
                .Where(w => w.FormId == formId && w.Status == status)
                .ToList();
        }

        public IQueryable<Application> GetAllByUser(int userId)
        {
            return _dataContext.Applications.Where(w => w.UserId == userId).Include(i => i.Form).ThenInclude(i => i.Cycle);
        }

        public List<User> GetAssignedUsers(int formId)
        {
            return _dataContext.Applications
                .Where(w => w.FormId == formId)
                .Select(s => s.User)
                .ToList();
        }

        public List<User> GetUnassignedUsers(int formId)
        {
            return _dataContext.Users
                .Where(w => !w.Applications.Any(a => a.FormId == formId))
                .ToList();
        }

        public void InviteApplicant(int formId, string email)
        {
            var user = new User { Email = email, UserName = email };
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            NewApplication(formId, user.Id);

            //TODO:: send invitation email
        }

        public int NewApplication(int formId, int userId)
        {
            var application = new Application
            {
                UserId = userId,
                FormId = formId,
                Status = ApplicationStatus.Assigned
            };
            _dataContext.Applications.Add(application);
            _dataContext.SaveChanges();

            return application.Id;
        }

        public void SubmitApplication(Application dbo)
        {
            var application = _dataContext.Applications.Find(dbo.Id);
            application.Title = dbo.Title;
            application.Status = ApplicationStatus.Submitted;

            foreach(var answer in dbo.Answers)
            {
                _dataContext.Answers.Add(new Answer
                {
                    ApplicationId = dbo.Id,
                    QuestionId = answer.QuestionId,
                    Text = answer.Text
                });
            }

            _dataContext.SaveChanges();
        }

        public IQueryable<Application> GetAllApplications(int formId)
        {
            var applications = _dataContext.Applications.Where(w => w.FormId == formId);
            return applications;
        }

        public void AssignUsers(List<int> applicants, int formId)
        {
            foreach(var applicant in applicants)
            {
                NewApplication(formId, applicant);
            }
        }

        public List<Answer> GetAnswers(int applicationId)
        {
            return _dataContext.Answers.Where(w => w.ApplicationId == applicationId).Include(i => i.Question).ToList();
        }

        public void TransitionApplication(int id, ApplicationStatus status)
        {
            Get(id).Status = status;
            _dataContext.SaveChanges();
        }
    }
}
