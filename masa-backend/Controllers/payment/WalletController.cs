//using Dto.Other;
//using Dto.Payment;
//using Dto.Response.Payment;
using masa_backend.ModelViews;
using masa_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text;
//using RestSharp;
using System.Text.Json;
//using ZarinPal.Class;

namespace masa_backend.Controllers.payment
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private static Random random = new Random();
        public WalletController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet, Route(template: "[action]/{personId}")]
        public ActionResult<ResponceWithData<string>> Get([FromRoute]Guid personId)
        {
            ResponceWithData<string> responce = new();
            try
            {
                responce.Data = _repository.WalletRepository.GetBalance(personId);
                responce.Success = true;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
        }
        public async Task<WalletDto> GetWallet(Guid personId)
        {
            try
            {
                var person = _repository.PersonalInformationRepository.Get(personId);
                if (person == null)
                    return null!;
                if (person.WalletId == Guid.Empty || person.WalletId == null)
                {
                    return _repository.WalletRepository.GetByPersonId(personId)??
                        await _repository.WalletRepository.AddAsync(
                            new WalletDto
                            {
                                PersonId = person.Id
                            });
                }
                return _repository.WalletRepository.Get((Guid)person.WalletId);
            }
            catch
            {
                throw;
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~!@#$%^&*()_+`-=.,<>?/0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpGet, Route(template: "[action]/{personId}")]
        public async Task<ActionResult<ResponceWithData<List<WalletHistoryDto>>>> GetHistoryList([FromRoute] Guid personId)
        {
            ResponceWithData<List<WalletHistoryDto>> responce = new();
            try
            {
                var person = _repository.PersonalInformationRepository.Get(personId);
                if (person == null)
                    return NotFound(responce);
                if (person.WalletId == null)
                {
                    var wallet = await GetWallet(personId);
                    person.WalletId = wallet.Id;
                    await _repository.PersonalInformationRepository.UpdateAsync(person);
                }
                responce.Success = true;
                responce.Data = await _repository.WalletHistoryRepository.ListAsync(person.WalletId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
        }
        
        [HttpPost, Route(template: "[action]")]
        public async Task<ActionResult<ResponceWithData<string>>> Deposit(ModelViews.NextPay.GenerateTokenDto request)//DtoRequest request, [FromRoute] Guid personId)
        {
            ResponceWithData<string> responce = new();
            try
            {
                var person = _repository.PersonalInformationRepository.Get(request.PersonId);
                if (person == null)
                    return BadRequest(NotFound());
                string _baseUrl = "https://nextpay.org/nx/gateway/token";
                HttpClient _httpClient = new()
                {
                    Timeout = new TimeSpan(0, 40, 0)
                };
                var orderId = RandomString(10);
                string str = $"api_key=d0147452-022c-481e-bbc9-7f274f63e38f&amount={request.Amount}&order_id={orderId}&customer_phone={person.Mobile}&callback_uri=https://localhost:7071/wallet/transactionInquery/{request.PersonId}";
                //"custom_json_fields": new FormControl({productName:'Shoes752' , id\":52 }),
                var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
                var bankResult = await _httpClient.PostAsync(_baseUrl, content);
                if (bankResult.IsSuccessStatusCode)
                {
                    var bankResponse = await bankResult.Content.ReadAsStringAsync();
                    var wallet = await GetWallet(request.PersonId);
                    await _repository.WalletHistoryRepository.AddAsync(
                            new WalletHistoryDto
                            {
                                Transaction = true,//send
                                WalletId = wallet.Id,
                                DtoRequest = bankResponse
                            });
                    wallet.Amount = (long.Parse(wallet.Amount) + long.Parse(request.Amount)).ToString();
                    await _repository.WalletRepository.UpdateAsync(wallet);
                    await _repository.SaveAsync();
                        responce.Data = bankResponse;
                        if (bankResponse.Contains("\"code\":-1"))
                        {
                            responce.Success = true;
                            return Ok(responce);
                        }
                }
                return BadRequest(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
            finally
            {
                _repository.Dispose();
            }
        }
        [HttpPost, Route("[action]")]
        public async Task<ActionResult<ResponceWithData<string>>> Withdraw(ModelViews.NextPay.WithdrawDto request)
        {
            ResponceWithData<string> responce = new();
            try
            {
                var person = _repository.PersonalInformationRepository.Get(request.PersonId);
                if (person == null)
                    return BadRequest(NotFound());
                string _baseUrl = "https://nextpay.org/nx/gateway/token";
                HttpClient _httpClient = new()
                {
                    Timeout = new TimeSpan(0, 40, 0)
                };
                var orderId = RandomString(10);
                string str = $"wid=32&auth=7207c1690e3acb95dcd3aafc1ec1ab7a14ca6b8s&amount={request.Amount}&sheba={request.Sheba}&name={person.FirstName + ' ' + person.LastName}&tracker={orderId}&currency=IRR";
                var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
                var bankResult = await _httpClient.PostAsync(_baseUrl, content);
                if (bankResult.IsSuccessStatusCode)
                {
                    var bankResponse = await bankResult.Content.ReadAsStringAsync();
                    var wallet = await GetWallet(request.PersonId);
                    await _repository.WalletHistoryRepository.AddAsync(
                            new WalletHistoryDto
                            {
                                Transaction = true,//send
                                WalletId = wallet.Id,
                                DtoRequest = bankResponse
                            });
                    wallet.Amount = (long.Parse(wallet.Amount) + long.Parse(request.Amount)).ToString();
                    await _repository.WalletRepository.UpdateAsync(wallet);
                    await _repository.SaveAsync();
                    responce.Data = bankResponse;
                    if (bankResponse.Contains("\"code\":200"))
                    {
                        responce.Success = true;
                        return Ok(responce);
                    }
                }
                return BadRequest(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
            finally
            {
                _repository.Dispose();
            }
        }
        [HttpGet, Route("[action]/{personId}")]
        public void TransactionInquery(ModelViews.NextPay.GenerateTokenResponceDto request)
        {
            
        }
    }
}
