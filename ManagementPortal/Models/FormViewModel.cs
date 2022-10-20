namespace ManagementPortal.Models
{
    public class FormViewModel
    {
        public int Id { get; set; }
        public int? CycleId { get; set; }
        public string? Title { get; set; }
        public int Assigned { get; set; }
        public int Submitted { get; set; }
        public int Accepted { get; set; }
        public int Denied { get; set; }
        public List<FormQuestionViewModel> Questions { get; set; } = new List<FormQuestionViewModel>();
        
    }
}
