namespace masa_backend.Models
{
    public class WalletHistory : BaseEntity
    {
        public bool? Transaction { get; set; }
        public string? TransactionId { get; set; }
        public Guid? WalletId { get; set; }
        public string? TransactionStatus { get; set; }
        public string? DtoRequest { get; set; }
        public Wallet? Wallet { get; set; }
    }
}