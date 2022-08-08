namespace masa_backend.Models
{
    public class WalletHistory : BaseEntity
    {
        public bool? Transaction { get; set; }
        public Guid? WalletId { get; set; }
        public int? TransactionStatus { get; set; }
        public string? DtoRequest { get; set; }
        public List<Wallet>? Wallets { get; set; }
    }
}