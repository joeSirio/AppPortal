using AppPortal.Data.Core.Enums;

namespace ApplicantPortal.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public QuestionType Type { get; set; }
        public List<string> AnswerOptions { get; set; }
        public string Answer { get; set; }
    }
}
