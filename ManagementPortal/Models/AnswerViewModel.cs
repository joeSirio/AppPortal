using AppPortal.Data.Core.Enums;

namespace ManagementPortal.Models
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public string AnswerOptions { get; set; }
    }
}
