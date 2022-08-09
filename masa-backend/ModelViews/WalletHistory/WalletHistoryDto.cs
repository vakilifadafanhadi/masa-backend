namespace masa_backend.ModelViews
{
    public class WalletHistoryDto : BaseEntityDto
    {
        public bool Transaction { get; set; }
        public Guid? WalletId { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionStatus { get; set; }
        public WalletDto? Wallet { get; set; }
        public string? DtoRequest { get; set; }
        //public WalletHistoryDto()
        //{
        //    Wallet = new WalletDto();
        //}
    }
}