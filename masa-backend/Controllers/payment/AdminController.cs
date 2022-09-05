using masa_backend.ModelViews;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using masa_backend.Repositories;

namespace masa_backend.Controllers.payment
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static Random random = new();
        private readonly IRepositoryWrapper _repository;
        public AdminController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet, Route("[action]")]
        public async Task<ActionResult<ResponceWithData<AdminTotalReport>>> GetBalance()
        {
            var responce = new ResponceWithData<AdminTotalReport>
            {
                Data = new AdminTotalReport()
            };
            HttpClient _httpClient = new()
            {
                Timeout = new TimeSpan(0, 40, 0)
            };
            try
            {
                string _baseUrl = "https://nextpay.org/nx/gateway/get_balance";
                string str = $"wid=14893&auth=b9789095916f8b436522247b6d73f76bc8f26f22";
                var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
                var bankResult = await _httpClient.PostAsync(_baseUrl, content);
                if (bankResult.IsSuccessStatusCode)
                {
                    var bankResponse = await bankResult.Content.ReadAsStringAsync();
                    var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var r = JsonSerializer.Deserialize<ModelViews.NextPay.GetBalanceResponceModelView>(bankResponse, jsonOptions);
                    if (r.Code == 200)
                        responce.Data.Balance = r;
                }
                responce.Data.SumUsers = _repository.UserRepository.Count();
                responce.Data.Transactions = await _repository.WalletHistoryRepository.ListAsync();
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
                _httpClient.Dispose();
                _repository.Dispose();
            }
        }
        [HttpGet, Route("[action]/{page}/{pageSize}")]
        public async Task<ActionResult<ResponceWithData<UserPagination>>> ListUsersPagination([FromRoute]int page, [FromRoute]int pageSize)
        {
            var response = new ResponceWithData<UserPagination>
            {
                Data = new UserPagination()
            };
            try
            {

                List<UserDto> result;
                result = await _repository.UserRepository.GetPaginationAsync(page, pageSize, null);
                var count = _repository.UserRepository.Count();
                response.Success = true;
                response.Data.Data = result;
                response.Data.Count = count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Add(ex);
                return BadRequest(response);
            }
        }
        [HttpGet, Route("[action]/{page}/{pageSize}/{userType}")]
        public async Task<ActionResult<ResponceWithData<UserPagination>>> ListUsersPagination([FromRoute] int page, [FromRoute] int pageSize, [FromRoute]int userType)
        {
            var response = new ResponceWithData<UserPagination>
            {
                Data = new UserPagination()
            };
            try
            {
                List<UserDto> result;
                result = await _repository.UserRepository.GetPaginationAsync(page, pageSize, userType);
                var count = _repository.UserRepository.Count();
                response.Success = true;
                response.Data.Data = result;
                response.Data.Count = count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Add(ex);
                return BadRequest(response);
            }
        }
        [HttpGet, Route("[action]/{personId}")]
        public async Task<ActionResult<ResponceMV>> UpdateUserPass([FromRoute]Guid personId)
        {
            var response = new ResponceMV();
            try
            {
                var user = _repository.UserRepository.GetByPersonId(personId);
                user.Pass = "123456";
                await _repository.UserRepository.UpdateAsync(user);
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Add(ex);
                return BadRequest(response);
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
                    return _repository.WalletRepository.GetByPersonId(personId) ??
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
        [HttpGet, Route("[action]/{adminId}/{personId}/{amount}")]
        public async Task<ActionResult<ResponceMV>> IncreaseWalletAmount([FromRoute]Guid adminId, [FromRoute] Guid personId, [FromRoute] long amount)
        {
            ResponceWithData<string> responce = new();
            try
            {
                var person = _repository.PersonalInformationRepository.Get(personId);
                if (person == null)
                    return BadRequest(NotFound());
                var orderId = RandomString(10);
                var wallet = await GetWallet(adminId);
                await _repository.WalletHistoryRepository.AddAsync(
                    new WalletHistoryDto
                    {
                        Transaction = false,//send
                        TransactionId = orderId,
                        TransactionStatus = "200",
                        WalletId = wallet.Id,
                        DtoRequest = "{\"code\":0,\"amount\":\"" + amount + "\",\"order_id\":\"" + orderId + "\",\"card_holder\":\"افزایش اعتبار مشتری\"}"
                    });
                wallet.Amount = (long.Parse(wallet.Amount) - amount).ToString();
                await _repository.WalletRepository.UpdateAsync(wallet);
                var toWallet = await GetWallet(personId);
                await _repository.WalletHistoryRepository.AddAsync(
                    new WalletHistoryDto
                    {
                        Transaction = true,//send
                        TransactionId = orderId,
                        TransactionStatus = "0",
                        WalletId = toWallet.Id,
                        DtoRequest = "{\"code\":0,\"amount\":\"" + amount + "\",\"order_id\":\"" + orderId + "\",\"card_holder\":\"افزایش اعتبار توسط مدیر\"}"
                    });
                toWallet.Amount = (long.Parse(toWallet.Amount) + amount).ToString();
                await _repository.WalletRepository.UpdateAsync(toWallet);
                await _repository.SaveAsync();
                responce.Data = "{\"code\":0,\"amount\":\"" + amount + "\",\"order_id\":\"" + orderId + "\",\"card_holder\":\"افزایش اعتبار توسط مدیر\"}";
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
        [HttpGet, Route("[action]/{personId}")]
        public async Task<ActionResult<ResponceMV>> UpgradeUserBusiness([FromRoute] Guid personId)
        {
            ResponceMV responce = new();
            try
            {
                var user = _repository.UserRepository.GetByPersonId(personId);
                if (user == null)
                    return BadRequest(NotFound());
                user.Type = 1;
                await _repository.UserRepository.UpdateAsync(user);
                await _repository.SaveAsync();
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
    }
}
