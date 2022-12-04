
using EgzaminoProjektas.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EgzaminoProjektas.Models
{
    public partial class Visitor : Person
    {
        public Visitor()
        {
            Prisonervisitors = new HashSet<PrisonerVisitor>();
        }
        public Visitor(DateOnly birthDate, string name, string surname)
        {
            BirthDate = birthDate;
            Name = name;
            Surname = surname;
        }

        public long Id { get; set; }
        [Display(Name = "Gimimo data")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public DateOnly BirthDate { get; set; }

        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public new string Name { get; set; } = null!;
        [Display(Name = "Pavardė")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public new string Surname { get; set; } = null!;
        public override string GetFullName()
        {
            return $"{Name} {Surname}";
        }

        public virtual ICollection<PrisonerVisitor> Prisonervisitors { get; set; }
    }
}
