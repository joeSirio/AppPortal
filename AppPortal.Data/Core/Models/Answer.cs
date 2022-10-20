using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Models;

public class Answer: Entity
{
    public string? Text { get; set; }

    [ForeignKey("Application")]
    public int ApplicationId { get; set; }
    [ForeignKey("Question")]
    public int QuestionId { get; set; }

    public Application? Application { get; set; }
    public FormQuestion? Question { get; set; }
}
