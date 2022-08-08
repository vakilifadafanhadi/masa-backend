namespace masa_backend.ModelViews.NextPay
{
    public class GenerateTokenResponceDto
    {
        public string? code { get; set; }
        public string? amount { get; set; }
        public int? status { get; set; }
        public string? trans_id { get; set; }
        public GenerateTokenResponceDto()
        {
            var str = code?.Replace("-", "");
            if (!string.IsNullOrEmpty(str))
                status = int.Parse(str);
        }
    }
}
