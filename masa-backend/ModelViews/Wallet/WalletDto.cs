namespace masa_backend.ModelViews
{
    public class WalletDto : BaseEntityDto
    {
        public string Amount { get; set; } = "0";
        public Guid? PersonId { get; set; }
        public Guid? WalletHistoryId { get; set; }
        public WalletHistoryDto? WalletHistory { get; set; }
        public PersonalInformationDto? PersonalInformation { get; set; }
        //public WalletDto()
        //{
        //    PersonalInformation = new PersonalInformationDto();
        //    WalletHistories = new List<WalletHistoryDto>();
        //}
    }
}