namespace masa_backend.ModelViews
{
    public class UserInfoModelView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public Guid WalletId { get; set; }
        public Guid PersonId { get; set; }
        public Guid UserId { get; set; }
        public int Type { get; set; } = 0;
    }
}
