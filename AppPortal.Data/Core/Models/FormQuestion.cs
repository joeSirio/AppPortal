using AppPortal.Data.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Models;

public class FormQuestion: Entity
{
    public string? Question { get; set; }
    public QuestionType Type { get; set; }
    public string? AnswerOptions { get; set; }

    [ForeignKey("Form")]
    public int FormId { get; set; }
    public Form? Form { get; set; }
}
