using AppPortal.Data.Core.Enums;
using AppPortal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Models;

public class Application: Entity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ApplicationStatus Status { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("Form")]
    public int FormId { get; set; }

    public User? User { get; set; }
    public Form? Form { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
}
