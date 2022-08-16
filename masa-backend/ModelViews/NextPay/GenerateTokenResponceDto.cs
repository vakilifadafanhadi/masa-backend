namespace masa_backend.ModelViews.NextPay
{
    public class GenerateTokenResponceDto
    {
        public int? Code { get; set; }
        public string? Amount { get; set; }
        public string? Trans_id { get; set; }
    }
}
