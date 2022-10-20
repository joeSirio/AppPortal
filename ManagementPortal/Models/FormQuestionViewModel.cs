using AppPortal.Data.Core.Enums;

namespace ManagementPortal.Models;

public class FormQuestionViewModel
{
    public int Id { get; set; }
    public int FormId { get; set; }
    public string? Question { get; set; }
    public QuestionType QuestionType { get; set; }
    public string? AnswerOptions { get; set; }
}
