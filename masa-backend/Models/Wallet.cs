namespace masa_backend.Models
{
    public class Wallet : BaseEntity
    {
        public string? Amount { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? WalletHistoryId { get; set; }
        public WalletHistory? WalletHistory { get; set; }
        public PersonalInformation? PersonalInformation { get; set; }
    }
}