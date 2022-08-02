using Dto.Other;
using Dto.Payment;
using Dto.Response.Payment;
using masa_backend.ModelViews;
using masa_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ZarinPal.Class;

namespace masa_backend.Controllers.payment
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        public WalletController(IRepositoryWrapper repository)
        {
            _repository = repository;
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }
        [HttpGet, Route(template: "[action]/{personId}")]
        public ActionResult<ResponceWithData<string>> Get([FromRoute]Guid personId)
        {
            ResponceWithData<string> responce = new();
            try
            {
                responce.Data = _repository.WalletRepository.GetBalance(personId);
                responce.Success = true;
                return Ok(responce);// );
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
                throw;
            }
        }
        [HttpPost, Route(template: "[action]/{personId}")]
        public async Task<IActionResult> Deposit(DtoRequest request, [FromRoute] Guid personId)
        {

            var result = await _payment.Request(request, Payment.Mode.sandbox);
            ResponceMV responce = new();
            var person = _repository.PersonalInformationRepository.Get(personId);
            if (person == null)
                return NotFound(responce);
            WalletDto searchWallet = new();
            if (person.WalletId == Guid.Empty)
            {
                searchWallet = _repository.WalletRepository.GetByPersonId(personId);
                if (searchWallet == null)
                {
                    searchWallet = await _repository.WalletRepository.AddAsync(
                        new WalletDto
                        {
                            PersonId = person.Id
                        });
                }
                person.WalletId = searchWallet.Id;
                await _repository.PersonalInformationRepository.UpdateAsync(person);
            }
            await _repository.WalletHistoryRepository.AddAsync(
                new WalletHistoryDto
                {
                    Transaction = true,//send
                    TransactionStatus = result.Status,
                    WalletId = person.WalletId,
                    DtoRequest = JsonSerializer.Serialize(request)
                });
            if (searchWallet == new WalletDto())
                searchWallet = _repository.WalletRepository.Get((Guid)person.WalletId!);
            if (searchWallet == null)
                return NoContent();
            searchWallet.Amount = (int.Parse(searchWallet.Amount) + request.Amount).ToString();
            await _repository.WalletRepository.UpdateAsync(searchWallet);
            await _repository.SaveAsync();
            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
        }
        [HttpGet, Route("[action]/{personId}")]
        public void SaveTransaction([FromRoute]Guid personId)
        {
            
        }
        /// <summary>
        /// ﻓﺮﺍﻳﻨﺪ ﺧﺮﻳﺪ ﺑﺎ ﺗﺴﻮﻳﻪ ﺍﺷﺘﺮﺍﻛﻲ 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RequestWithExtra()
        {
            var result = await _payment.Request(new DtoRequestWithExtra()
            {
                Mobile = "09121112222",
                CallbackUrl = "https://localhost:44310/home/validate",
                Description = "توضیحات",
                Email = "farazmaan@outlook.com",
                Amount = 1000000,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                AdditionalData = "{\"Wages\":{\"zp.1.1\":{\"Amount\":120,\"Description\":\" ﺗﻘﺴﻴﻢ \"}, \" ﺳﻮﺩ ﺗﺮﺍﻛﻨﺶ zp.2.5\":{\"Amount\":60,\"Description\":\" ﻭﺍﺭﻳﺰ \"}}} "
            }, Payment.Mode.sandbox);
            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
        }
        /// <summary>
        /// اعتبار سنجی خرید
        /// </summary>
        /// <param name="authority"></param>
        /// <param name="status"></param>
        /// <returns></returns>

        public async Task<IActionResult> Validate(string authority, string status)
        {
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = 1000000,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);
            return Ok();
        }

        /// <summary>
        /// ﺩﺭ ﺭﻭﺵ ﺍﻳﺠﺎﺩ ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﺑﺎ ﻃﻮﻝ ﻋﻤﺮ ﺑﺎﻻ ﻣﻤﻜﻦ ﺍﺳﺖ ﺣﺎﻟﺘﻲ ﭘﻴﺶ ﺁﻳﺪ ﻛﻪ ﺷﻤﺎ ﺑﻪ ﺗﻤﺪﻳﺪ ﺑﻴﺸﺘﺮ ﻃﻮﻝ ﻋﻤﺮ ﻳﻚ ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻧﻴﺎﺯ ﺩﺍﺷﺘﻪ ﺑﺎﺷﻴﺪ
        /// ﺩﺭ ﺍﻳﻦ ﺻﻮﺭﺕ ﻣﻲ ﺗﻮﺍﻧﻴﺪ ﺍﺯ ﻣﺘﺪ زیر ﺍﺳﺘﻔﺎﺩﻩ ﻧﻤﺎﻳﻴﺪ 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RefreshAuthority()
        {
            var refresh = await _authority.Refresh(new DtoRefreshAuthority
            {
                Authority = "",
                ExpireIn = 1,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
            }, Payment.Mode.sandbox);
            return Ok();
        }

        /// <summary>
        /// ﻣﻤﻜﻦ ﺍﺳﺖ ﺷﻤﺎ ﻧﻴﺎﺯ ﺩﺍﺷﺘﻪ ﺑﺎﺷﻴﺪ ﻛﻪ ﻣﺘﻮﺟﻪ ﺷﻮﻳﺪ ﭼﻪ ﭘﺮﺩﺍﺧﺖ ﻫﺎﻱ ﺗﻮﺳﻂ ﻭﺏ ﺳﺮﻭﻳﺲ ﺷﻤﺎ ﺑﻪ ﺩﺭﺳﺘﻲ ﺍﻧﺠﺎﻡ ﺷﺪﻩ ﺍﻣﺎ ﻣﺘﺪ  ﺭﻭﻱ ﺁﻧﻬﺎ ﺍﻋﻤﺎﻝ ﻧﺸﺪﻩ
        /// ، ﺑﻪ ﻋﺒﺎﺭﺕ ﺩﻳﮕﺮ ﺍﻳﻦ ﻣﺘﺪ ﻟﻴﺴﺖ ﭘﺮﺩﺍﺧﺖ ﻫﺎﻱ ﻣﻮﻓﻘﻲ ﻛﻪ ﺷﻤﺎ ﺁﻧﻬﺎ ﺭﺍ ﺗﺼﺪﻳﻖ ﻧﻜﺮﺩﻩ ﺍﻳﺪ ﺭﺍ ﺑﻪ PaymentVerification ﺷﻤﺎ ﻧﻤﺎﻳﺶ ﻣﻲ ﺩﻫﺪ.
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Unverified()
        {
            var refresh = await _transactions.GetUnverified(new DtoMerchant
            {
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
            }, Payment.Mode.sandbox);
            return Ok();
        }
    }
}
