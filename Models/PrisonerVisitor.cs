using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EgzaminoProjektas.Models
{
    public partial class PrisonerVisitor
    {
        public long PrisonerId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Pradžios data")]
        public DateTime StartDate { get; set; }
        public long VisitorId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Pabaigos data")]
        public DateTime FinishDate { get; set; }

        public virtual Prisoner? Prisoner { get; set; } = null!;
        public virtual Visitor? Visitor { get; set; } = null!;
    }
}
