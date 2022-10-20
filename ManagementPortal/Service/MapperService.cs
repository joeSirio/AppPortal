using AppPortal.Data;
using AppPortal.Data.Core.Enums;
using AppPortal.Data.Core.Models;
using ManagementPortal.Models;

namespace ManagementPortal.Service
{
    public class MapperService
    {
        #region Applicant
        public List<ApplicantViewModel> Map(List<User> dbos, bool assigned)
        {
            var dto = new List<ApplicantViewModel>();

            foreach(var dbo in dbos)
            {
                dto.Add(Map(dbo, assigned));
            }

            return dto;
        }

        public ApplicantViewModel Map(User dbo, bool assigned)
        {
            var dto = new ApplicantViewModel
            {
                UserId = dbo.Id,
                Email = dbo.Email,
                Assigned = assigned
            };

            return dto;
        }
        #endregion

        #region Application
        public List<ApplicationViewModel> Map(List<Application> dbos)
        {
            var dto = new List<ApplicationViewModel>();
            foreach (var dbo in dbos)
            {
                dto.Add(Map(dbo));
            }
            return dto;
        }

        public ApplicationViewModel Map(Application dbo)
        {
            var dto = new ApplicationViewModel()
            {
                Id = dbo.Id,
                Title = dbo.Title,
                FormId = dbo.FormId,
                UserId = dbo.UserId,
                UserEmail = dbo.User?.Email,
                Status = dbo.Status,
                StatusText = Enum.GetName(typeof(ApplicationStatus), dbo.Status)
            };
            return dto;
        }

        public List<AnswerViewModel> Map(List<Answer> dbos)
        {
            var dto = new List<AnswerViewModel>();
            foreach(var dbo in dbos)
            {
                dto.Add(Map(dbo));
            }

            return dto;
        }

        public AnswerViewModel Map(Answer dbo)
        {
            return new AnswerViewModel
            {
                Id = dbo.Id,
                Text = dbo.Text,
                QuestionId = dbo.QuestionId,
                QuestionText = dbo.Question.Question,
                QuestionType = dbo.Question.Type,
                AnswerOptions = dbo.Question.AnswerOptions
            };
        }
        #endregion

        #region Cycle
        public List<CycleViewModel> Map(List<Cycle> dbos)
        {
            var dtos = new List<CycleViewModel>();
            foreach(var dbo in dbos)
            {
                dtos.Add(Map(dbo));
            }

            return dtos;
        }

        public CycleViewModel Map(Cycle dbo)
        {
            var dto = new CycleViewModel
            {
                Id = dbo.Id,
                Title = dbo.Title,
                StartDateTime = dbo.StartDateTime,
                EndDateTime = dbo.EndDateTime,
                Forms = Map(dbo.Forms)
            };

            return dto;
        }
        #endregion

        #region Form

        public FormViewModel Map(Form dbo)
        {
            var dto = new FormViewModel
            {
                Id = dbo.Id,
                CycleId = dbo.CycleId,
                Title = dbo.Title,
                Assigned = dbo.Applications.Count(),
                Accepted = dbo.Applications.Where(w => w.Status == ApplicationStatus.Accepted).Count(),
                Denied = dbo.Applications.Where(w => w.Status == ApplicationStatus.Denied).Count(),
                Submitted = dbo.Applications.Where(w => w.Status != ApplicationStatus.Assigned).Count(),
            };
            dto.Questions = Map(dbo.Questions);

            return dto;
        }

        public List<FormQuestionViewModel> Map(List<FormQuestion> dbos)
        {
            List<FormQuestionViewModel> dtos = new List<FormQuestionViewModel>();

            foreach (var dbo in dbos)
            {
                var dto = new FormQuestionViewModel
                {
                    Id = dbo.Id,
                    FormId = dbo.FormId,
                    Question = dbo.Question,
                    QuestionType = dbo.Type,
                    AnswerOptions = dbo.AnswerOptions
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public Form Map(FormViewModel dto)
        {
            var dbo = new Form
            {
                Id = dto.Id,
                CycleId = dto.CycleId ?? 0,
                Title = dto.Title
            };
            dbo.Questions = Map(dto.Questions, dto.Id);

            return dbo;
        }

        public List<FormQuestion> Map(List<FormQuestionViewModel> dto, int FormId = 0)
        {
            List<FormQuestion> dbos = new List<FormQuestion>();

            foreach (var question in dto)
            {
                var dboQ = new FormQuestion
                {
                    Id = question.Id,
                    FormId = question.FormId != 0 ? question.FormId : FormId,
                    Question = question.Question,
                    Type = question.QuestionType,
                    AnswerOptions = question.AnswerOptions
                };

                dbos.Add(dboQ);
            }

            return dbos;
        }

        public List<FormViewModel> Map(List<Form> dbos)
        {
            var dtos = new List<FormViewModel>();
            foreach(var dbo in dbos)
            {
                dtos.Add(Map(dbo));
            }

            return dtos;
        }

        #endregion

        #region User / Role
        public UserViewModel Map(User dbo)
        {
            return new UserViewModel
            {
                Id = dbo.Id,
                Email = dbo.Email,
                EmailConfirmed = dbo.EmailConfirmed,
                PhoneNumberConfirmed = dbo.PhoneNumberConfirmed
            };
        }

        public List<UserViewModel> Map(List<User> dbos)
        {
            var dto = new List<UserViewModel>();
            foreach (var dbo in dbos)
            {
                dto.Add(new UserViewModel
                {
                    Id = dbo.Id,
                    Email = dbo.Email,
                    EmailConfirmed = dbo.EmailConfirmed,
                    PhoneNumberConfirmed = dbo.PhoneNumberConfirmed
                });
            }

            return dto;
        }

        public User Map(UserViewModel dto)
        {
            return new User
            {
                Id = dto.Id,
                Email = dto.Email,
                EmailConfirmed = dto.EmailConfirmed,
                PhoneNumberConfirmed = dto.PhoneNumberConfirmed
            };
        }

        public List<RoleViewModel> Map(List<Role> dbos)
        {
            var dto = new List<RoleViewModel>();
            foreach (var dbo in dbos)
            {
                dto.Add(new RoleViewModel
                {
                    Id = dbo.Id,
                    Name = dbo.Name
                });
            }

            return dto;
        }

        public List<Role> Map(List<RoleViewModel> dtos)
        {
            var dbo = new List<Role>();
            foreach (var dto in dtos)
            {
                dbo.Add(Map(dto));
            }

            return dbo;
        }

        public Role Map(RoleViewModel dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
        #endregion
    }
}
