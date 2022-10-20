using AppPortal.Data.Core.Enums;
using System.ComponentModel;

namespace ApplicantPortal.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        [DisplayName("Application Title")]
        public string Title { get; set; }
        public ApplicationStatus Status { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
