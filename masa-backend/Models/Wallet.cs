namespace masa_backend.Models
{
    public class Wallet : BaseEntity
    {
        public string? Amount { get; set; }
        public Guid? PersonId { get; set; }
        public List<WalletHistory>? WalletHistories { get; set; }
        public PersonalInformation? PersonalInformation { get; set; }
    }
}