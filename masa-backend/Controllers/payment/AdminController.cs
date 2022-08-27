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
        [HttpGet, Route("[action]")]
        public async Task<ActionResult<ResponceWithData<List<UserDto>>>> ListUsers()
        {
            var response = new ResponceWithData<List<UserDto>>();
            try
            {
                var result = await _repository.UserRepository.GetAllAsync();
                response.Success = true;
                response.Data = result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                response.Add(ex);
                return BadRequest(response);
            }
        }
    }
}
