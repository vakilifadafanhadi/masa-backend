using masa_backend.ModelViews.NextPay;

namespace masa_backend.ModelViews
{
    public class AdminTotalReport
    {
        public GetBalanceResponceModelView Balance { get; set; }
        public int SumUsers { get; set; }
        public List<WalletHistoryDto> Transactions { get; set; }
        public AdminTotalReport()
        {
            Balance = new GetBalanceResponceModelView();
            Transactions = new List<WalletHistoryDto>();

        }

    }
}
