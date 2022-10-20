using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Core.Enums
{
    public enum QuestionType
    {
        Text,
        [Display(Name = "Multiple Choice")]
        MultipleChoice,
        [Display(Name = "Multi Select")]
        MultiSelect,
        [Display(Name = "Yes / No")]
        YesNo,
        [Display(Name ="Text If Yes (Yes/No)")]
        TextIfYes,
        [Display(Name = "Text If No (Yes/No)")]
        TextIfNo,
        [Display(Name = "No Answer")]
        NoAnswer
    }
}
