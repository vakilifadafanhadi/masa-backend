using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class LoginModelView
    {
        [Required]
        public string PersonalIdentity { get; set; } = default!;
        [Required]
        public string Pass { get; set; } = default!;
    }
}
