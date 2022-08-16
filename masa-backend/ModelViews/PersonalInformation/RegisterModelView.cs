using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class RegisterModelView
    {
        public string? FirstName { get; set; }
        [Required]
        public string LastName { get; set; } = default!;
        [Required]
        [StringLength(10)]
        public string NationalCode { get; set; } = default!;
        [Required]
        [MinLength(10)]
        public string Mobile { get; set; } = default!;
        [Required]
        [MinLength(6)]
        public string Pass { get; set; } = default!;
        public string? NewPass { get; set; }
        [Required]
        public CountryDto Country { get; set; }
        [Required]
        public int Gender { get; set; }
        public RegisterModelView()
        {
            Country = new CountryDto();
        }
    }
}
