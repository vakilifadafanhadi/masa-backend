using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class RegisterModelView
    {
        [Required]
        public string LastName { get; set; } = default!;
        [Required]
        [StringLength(10)]
        public string NationalCode { get; set; } = default!;
        [Required]
        [MinLength(10)]
        public string Mobile { get; set; } = default!;
    }
}
