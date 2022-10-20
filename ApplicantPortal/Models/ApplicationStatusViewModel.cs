using AppPortal.Data.Core.Enums;

namespace ApplicantPortal.Models
{
    public class ApplicationStatusViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
