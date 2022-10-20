using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Models;

public class Form: Entity
{
    public string? Title { get; set; }

    [ForeignKey("Cycle")]
    public int CycleId { get; set; }
    public Cycle? Cycle { get; set; }

    public List<Application> Applications { get; set; } = new List<Application>();
    public List<FormQuestion> Questions { get; set; } = new List<FormQuestion>();
}
