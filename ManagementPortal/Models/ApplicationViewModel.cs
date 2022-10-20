using AppPortal.Data.Core.Enums;
using System.ComponentModel;

namespace ManagementPortal.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FormId { get; set; }
        public string FormTitle { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public ApplicationStatus Status { get; set; }
        [DisplayName("Status")]
        public string StatusText { get; set; }
        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();
    }
}
