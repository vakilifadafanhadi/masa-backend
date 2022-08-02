namespace masa_backend.Models
{
    public class WalletHistory : BaseEntity
    {
        public Guid? WalletId { get; set; }
        public bool? Transaction { get; set; }
        public int? TransactionStatus { get; set; }
        public string? DtoRequest { get; set; }
        public Wallet? Wallet { get; set; }
    }
}