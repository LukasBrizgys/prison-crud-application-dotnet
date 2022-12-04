using Microsoft.AspNetCore.Identity;

namespace EgzaminoProjektas.Models
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        [PersonalData]
        public string? Name { get; set; }

        [PersonalData]
        public string? Surname { get; set; }

    }
}
