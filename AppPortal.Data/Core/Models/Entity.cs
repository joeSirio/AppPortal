using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Models;

public class Entity
{
    [Key]
    public int Id { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public string? UpdatedBy { get; set; }
}
