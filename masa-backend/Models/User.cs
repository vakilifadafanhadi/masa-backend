using System.ComponentModel.DataAnnotations;

namespace masa_backend.Models
{
    public class User:BaseEntity
    {
        [Required]
        [StringLength(16)]
        public string? Token { get; set; } = default!;
        [Required]
        public string? UserName { get; set; } = default!;
        [Required]
        public string? Key { get; set; } = default!;
        [Required]
        public string? Pass { get; set; } = default!;
        [Required]
        [StringLength(10)]
        public string NationalCode { get; set; } = default!;
        public PersonalInformation? PersonalInformation { get; set; }
        public User()
        {
            var random = new Random();
            if (string.IsNullOrEmpty(Token))
                Token = random.Next(100, 999) + NationalCode + random.Next(100, 999);
            PersonalInformation = new PersonalInformation();
        }
       
    }
}
