using System.ComponentModel.DataAnnotations;

namespace MasAcademyLab.Service.Models
{
    public class UserModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
