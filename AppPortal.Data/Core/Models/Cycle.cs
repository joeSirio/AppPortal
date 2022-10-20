using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Models;

public class Cycle: Entity
{
    public string? Title { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public List<Form> Forms { get; set; } = new List<Form>();
}
