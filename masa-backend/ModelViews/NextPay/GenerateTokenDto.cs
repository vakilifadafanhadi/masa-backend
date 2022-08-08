using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews.NextPay
{
    public class GenerateTokenDto
    {
        [Required]
        public Guid PersonId { get; set; }
        [Required]
        public string Amount { get; set; } = "0";
    }
}
