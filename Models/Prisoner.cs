using EgzaminoProjektas.Classes;
using EgzaminoProjektas.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EgzaminoProjektas.Models
{
    public partial class Prisoner : Person
    {
        public Prisoner()
        {
            Prisonercrimes = new HashSet<PrisonerCrime>();
            Prisonervisitors = new HashSet<PrisonerVisitor>();
        }

        public long Id { get; set; }
        [Required(ErrorMessage="Šis laukas reikalingas")]
        [Display(Name = "Gimimo data")]
        public DateOnly? BirthDate { get; set; }

        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Vardą turi sudaryti bent 3 simboliai")]
        public new string Name { get; set; } = null!;
        [Display(Name = "Telefonas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public string Phone { get; set; } = null!;
        [Display(Name = "Pavardė")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Pavardę turi sudaryti bent 3 simboliai")]
        public new string Surname { get; set; } = null!;
        [Display(Name = "Miestas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        public byte? CityId { get; set; }
        [Display(Name = "Statusas")]
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        [EnumDataType(typeof(PrisonerStatus))]
        public PrisonerStatus StatusId { get; set; }
        public string? FileName { get; set; }
        [Display(Name = "Miestas")]
        public virtual City? City { get; set; }
        public override string GetFullName()
        {
            return $"{Name} {Surname}";
        }
        public virtual ICollection<PrisonerCrime> Prisonercrimes { get; set; }
        public virtual ICollection<PrisonerVisitor> Prisonervisitors { get; set; }
    }
}
