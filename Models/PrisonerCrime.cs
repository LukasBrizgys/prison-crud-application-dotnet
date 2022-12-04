using System.ComponentModel.DataAnnotations;

namespace EgzaminoProjektas.Models
{
    public partial class PrisonerCrime
    {
        [Required(ErrorMessage = "Šis laukas reikalingas")]
        [Display(Name = "Nusikaltimo ID")]
        public long CrimeId { get; set; }

        [Required(ErrorMessage = "Šis laukas reikalingas")]
        [Display(Name = "Nusikaltimo data")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Šis laukas reikalingas")]
        [Display(Name = "Kalinys")]
        public long PrisonerId { get; set; }

        [Display(Name = "Nusikaltimas")]
        public virtual Crime? Crime { get; set; } = null!;

        [Display(Name = "Nusikaltėlis")]
        public virtual Prisoner? Prisoner { get; set; } = null!;
    }
}
