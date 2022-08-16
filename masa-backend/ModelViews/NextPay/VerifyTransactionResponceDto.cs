namespace masa_backend.ModelViews.NextPay
{
    public class VerifyTransactionResponceDto
    {
        public int Code { get; set; }
        public long Amount { get; set; }
        public string? Order_id { get; set; }
        public string? Card_holder { get; set; }
        public string? Shaparak_Ref_Id { get; set; }
        public string? Customer_phone { get; set; }
        public long? Partial_refunded_amount { get; set; }
        public string? Custom { get; set; }
        public string? Created_at { get; set; }
    }
}
