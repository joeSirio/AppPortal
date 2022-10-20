using AppPortal.Data.Core.Enums;
using AppPortal.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Interfaces
{
    public interface IApplicationService
    {
        Application Get(int id);
        IQueryable<Application> GetAll();
        List<Application> GetAll(int formId);
        List<Application> GetAll(int formId, ApplicationStatus status);
        IQueryable<Application> GetAllByUser(int userId);
        List<User> GetAssignedUsers(int formId);
        List<User> GetUnassignedUsers(int formId);
        void InviteApplicant(int formId, string email);
        int NewApplication(int formId, int userId);
        void SubmitApplication(Application dbo);
        IQueryable<Application> GetAllApplications(int formId);
        void AssignUsers(List<int> applicants, int formId);
        List<Answer> GetAnswers(int applicationId);
        void TransitionApplication(int id, ApplicationStatus status);
    }
}
