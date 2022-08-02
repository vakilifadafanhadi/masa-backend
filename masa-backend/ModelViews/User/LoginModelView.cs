using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class LoginModelView
    {
        [Required]
        public string NationalCode { get; set; } = default!;
    }
}
