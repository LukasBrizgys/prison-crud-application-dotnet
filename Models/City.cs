using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EgzaminoProjektas.Models
{
    public partial class City
    {
        public City()
        {
            Prisoners = new HashSet<Prisoner>();
        }

        public byte Id { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }
}
