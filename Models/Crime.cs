using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EgzaminoProjektas.Models
{
    public partial class Crime
    {
        public Crime()
        {
            Prisonercrimes = new HashSet<PrisonerCrime>();
        }

        public long Id { get; set; }
        [Display(Name = "Nusikaltimas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public string Name { get; set; } = null!;

        public virtual ICollection<PrisonerCrime> Prisonercrimes { get; set; }
    }
}
