namespace ManagementPortal.Models
{
    public class CycleViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<FormViewModel> Forms { get; set; } = new List<FormViewModel>(); 
    }
}
