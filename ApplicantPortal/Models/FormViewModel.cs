namespace ApplicantPortal.Models
{
    public class FormViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ApplicationStatusViewModel> Applications { get; set; } = new List<ApplicationStatusViewModel>();
    }
}
