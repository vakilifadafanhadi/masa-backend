using masa_backend.ModelViews;
using masa_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace masa_backend.Controllers.payment
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private static Random random = new();

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
            finally
            {
                _repository.Dispose();
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
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_-0123456789";
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
            finally
            {
                _repository.Dispose();
            }
        }
        
        [HttpPost, Route(template: "[action]")]
        public async Task<ActionResult<ResponceWithData<string>>> Deposit(ModelViews.NextPay.GenerateTokenDto request)//DtoRequest request, [FromRoute] Guid personId)
        {
            ResponceWithData<string> responce = new();
            HttpClient _httpClient = new()
            {
                Timeout = new TimeSpan(0, 40, 0)
            };
            try
            {
                var person = _repository.PersonalInformationRepository.Get(request.PersonId);
                if (person == null)
                    return BadRequest(NotFound());
                string _baseUrl = "https://nextpay.org/nx/gateway/token";
                var orderId = RandomString(10);
                string str = $"api_key=20219a10-e7f8-4a25-b38c-01fdd5ebdb42&amount={request.Amount}&order_id={orderId}&customer_phone={person.Mobile}&callback_uri=https://masabank.bitmandotbotfother.ir/pay-res/{request.PersonId}";
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
                            TransactionId = orderId,
                            TransactionStatus = "0",
                            WalletId = wallet.Id,
                            DtoRequest = bankResponse
                        });
                    responce.Data = bankResponse;
                    var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var r = JsonSerializer.Deserialize<ModelViews.NextPay.GenerateTokenResponceDto>(bankResponse,jsonOptions);
                    await _repository.SaveAsync();
                    if (r.Code == -1)
                    {
                        responce.Success = true;
                        return Ok(responce);
                    }
                }
                responce.Warnings.Add("بانک پاسخگو نبود");
                return BadRequest(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
            finally
            {
                _httpClient.Dispose();
                _repository.Dispose();
            }
        }
        [HttpPost, Route(template:"[action]")]
        public async Task<ActionResult<ResponceWithData<string>>> Withdraw(ModelViews.NextPay.WithdrawDto request)
        {
            ResponceWithData<string> responce = new();
            HttpClient _httpClient = new()
            {
                Timeout = new TimeSpan(0, 40, 0)
            };
            try
            {
                var person = _repository.PersonalInformationRepository.Get(request.PersonId);
                if (person == null)
                    return BadRequest(NotFound());
                var wallet = await GetWallet(request.PersonId);
                if (long.Parse(wallet.Amount) < long.Parse(request.Amount))
                {
                    responce.Errors.Add("مبلغ درخواستی شما از موجودی حساب تان بیشتر است");
                    return BadRequest(responce);
                }
                //string _baseUrl = "https://nextpay.org/nx/gateway/checkout_on_time";
                string _baseUrl = "https://nextpay.org/nx/gateway/checkout";
                var orderId = RandomString(10);
                //string type = "sheba";
                //string str = $"wid=14893&auth=b9789095916f8b436522247b6d73f76bc8f26f22&amount={request.Amount}&type={type}&sheba={request.Sheba}&name={person.FirstName + ' ' + person.LastName}&tracker={orderId}";
                string str = $"wid=14893&auth=b9789095916f8b436522247b6d73f76bc8f26f22&amount={request.Amount}&sheba={request.Sheba}&name={person.FirstName + ' ' + person.LastName}&tracker={orderId}";
                var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
                var bankResult = await _httpClient.PostAsync(_baseUrl, content);
                if (bankResult.IsSuccessStatusCode)
                {
                    var bankResponse = await bankResult.Content.ReadAsStringAsync();
                    var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var r = JsonSerializer.Deserialize<ModelViews.NextPay.WithdrawResponceDto>(bankResponse,jsonOptions);
                    await _repository.WalletHistoryRepository.AddAsync(
                            new WalletHistoryDto
                            {
                                Transaction = false,//send
                                TransactionId = orderId,
                                TransactionStatus = r?.Code==200?"0":r?.Message,
                                WalletId = wallet.Id,
                                DtoRequest = bankResponse
                            });
                    if (r.Code == 200)
                    {
                        wallet.Amount = (long.Parse(wallet.Amount) - long.Parse(request.Amount)).ToString();
                        await _repository.WalletRepository.UpdateAsync(wallet);
                        responce.Success = true;
                    }
                    await _repository.SaveAsync();
                    responce.Data = bankResponse;
                    return Ok(responce);
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
                _httpClient.Dispose();
                _repository.Dispose();
            }
        }
        [HttpPost, Route(template:"[action]/{fromId}/{toPersonalityId}")]
        public async Task<ActionResult<ResponceMV>> Transfer([FromBody] TransferRequestModelView request, [FromRoute] Guid fromId, [FromRoute] string toPersonalityId)
        {
            var toId = _repository.UserRepository.GetByPersonalityId(toPersonalityId).PersonId;
            return Ok(await Transferto(request, fromId, toId));
        }
        [HttpPost, Route(template:"[action]/{fromId}/{toId}")]
        public async Task<ActionResult<ResponceMV>> Transferto([FromBody] TransferRequestModelView request, [FromRoute]Guid fromId, [FromRoute]Guid toId)
        {
            ResponceMV responce = new();
            try
            {
                if (request.Amount>50000)
                {
                    var person = _repository.PersonalInformationRepository.Get(fromId);
                    var user = _repository.UserRepository.GetByPersonId(person.Id);
                    if(request.Pass!=user.Pass)
                    {
                        responce.Errors.Add("رمز نادرست");
                        return BadRequest(responce);
                    }
                }
                var fromWallet = _repository.WalletRepository.GetByPersonId(fromId);
                if (fromWallet == null)
                {
                    responce.Errors.Add("مشکلی پیش آمد");
                    return BadRequest(responce);
                }
                if (int.Parse(fromWallet.Amount) < request.Amount)
                {
                    responce.Errors.Add("موجودی کافی نیست");
                    return BadRequest(responce);
                }
                var toWallet = _repository.WalletRepository.GetByPersonId(toId);
                if (fromWallet == null)
                {
                    responce.Errors.Add("مشکلی پیش آمد");
                    return BadRequest(responce);
                }
                fromWallet.Amount = (int.Parse(fromWallet.Amount) - request.Amount).ToString();
                await _repository.WalletRepository.UpdateAsync(fromWallet);
                string orderId = RandomString(10);
                await _repository.WalletHistoryRepository.AddAsync(
                    new WalletHistoryDto
                    {
                        DtoRequest = "{\"code\":0,\"amount\":\"" + request.Amount + "\",\"order_id\":\"" + orderId + "\",\"card_holder\":\"خرید خدمات\"}",
                        Transaction = false,
                        TransactionId = orderId,
                        TransactionStatus = "200",
                        WalletId = fromWallet.Id
                    });
                toWallet.Amount = (int.Parse(toWallet.Amount) + request.Amount).ToString();
                await _repository.WalletRepository.UpdateAsync(toWallet);
                await _repository.WalletHistoryRepository.AddAsync(
                    new WalletHistoryDto
                    {
                        DtoRequest = "{\"code\":0,\"amount\":\"" + request.Amount + "\",\"order_id\":\"" + orderId + "\",\"card_holder\":\"فروش خدمات\"}",
                        Transaction = true,
                        TransactionId = orderId,
                        TransactionStatus = "0",
                        WalletId = toWallet.Id
                    });
                await _repository.SaveAsync();
                responce.Success = true;
                responce.Warnings.Add(toWallet.Amount);
                responce.Warnings.Add(fromWallet.Amount);
                return Ok(responce);
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
        [HttpGet, Route(template: "[action]/{personId}/{trans_id}/{order_id}/{amount}/{np_status}")]
        public async Task<ActionResult<ResponceMV>> DepositCallBack([FromRoute] Guid personId, [FromRoute] string trans_id, [FromRoute] string order_id, [FromRoute] long amount, [FromRoute] string np_status)
        {
            ResponceMV responce = new();
            try
            {
                var person = _repository.PersonalInformationRepository.Get(personId);
                if (person == null)
                {
                    responce.Errors.Add("خطا: کاربر یافت نشد");
                    return BadRequest(responce);
                }
                var wallet = await GetWallet(personId);
                var walletHistory = _repository.WalletHistoryRepository.GetByWalletAndTransferId(wallet.Id, order_id);
                if (walletHistory == null)
                {
                    responce.Errors.Add("خطا: مشکلی پیش آمد");
                    return BadRequest(responce);
                }
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var dtoreq = JsonSerializer.Deserialize<ModelViews.NextPay.GenerateTokenResponceDto>(walletHistory?.DtoRequest, jsonOptions);
                if (dtoreq.Trans_id == trans_id &&
                    long.Parse(dtoreq?.Amount) == amount)
                {
                    walletHistory.TransactionStatus = np_status;
                    //await _repository.WalletHistoryRepository.UpdateAsync(walletHistory);
                }
                if (np_status == "Unsuccessful")
                {
                    responce.Errors.Add("عملیات ناموفق بود");
                    return Ok();
                }
                string _baseUrl = "https://nextpay.org/nx/gateway/verify";
                HttpClient _httpClient = new()
                {
                    Timeout = new TimeSpan(0, 40, 0)
                };
                var orderId = RandomString(10);
                string str = $"api_key=20219a10-e7f8-4a25-b38c-01fdd5ebdb42&trans_id={trans_id}&amount={amount}";
                var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
                var bankResult = await _httpClient.PostAsync(_baseUrl, content);
                if (bankResult.IsSuccessStatusCode)
                {
                    var bankResponse = await bankResult.Content.ReadAsStringAsync();
                    var r = JsonSerializer.Deserialize<ModelViews.NextPay.VerifyTransactionResponceDto>(bankResponse, jsonOptions);
                    if (r.Code == 0)
                    {
                        walletHistory.TransactionStatus = r.Code.ToString();
                        walletHistory.DtoRequest = bankResponse;
                        await _repository.WalletHistoryRepository.UpdateAsync(walletHistory);
                        long sum = long.Parse(wallet.Amount) + amount;
                        wallet.Amount = sum.ToString();
                        await _repository.WalletRepository.UpdateAsync(wallet);
                        await _repository.SaveAsync();
                        responce.Success = true;
                        return Ok(responce);
                    }
                }
                responce.Success = true;
                return Ok(responce);
            }
            catch
            {
                responce.Success = true;
                return Ok(responce);
            }
            finally
            {
                _repository.Dispose();
            }
        }
    }
}
