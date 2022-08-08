using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using masa_backend.Repositories;
using masa_backend.ModelViews;

namespace masa_backend.Controllers.payment
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpPost, Route(template: "[action]")]
        public async Task<ActionResult<ResponceMV>> Register(PersonalInformationDto request)
        {
            ResponceMV responce = new();
            try
            {
                var country = await _repository.CountryRepository.AddAsync(request?.Country);
                var person = await _repository.PersonalInformationRepository.AddAsync(
                    new PersonalInformationDto
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        NationalCode = request.NationalCode,
                        Mobile = request.Mobile,
                        CountryId = country.Id
                    });
                await _repository.UserRepository.AddAsync(
                    new UserDto
                    {
                        PersonId = person.Id,
                        UserName = request.NationalCode
                    });
                await _repository.WalletRepository.AddAsync(
                    new WalletDto
                    {
                        PersonId = person.Id
                    });
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
        [HttpPost, Route(template: "[action]")]
        public async Task<ActionResult<ResponceWithData<UserDto>>> Login(LoginModelView request)
        {
            ResponceWithData<UserDto> responce = new();
            try
            {
                var result = _repository.UserRepository.Login(request);
                if (result == null)
                {
                    responce.Errors.Add("not found!");
                    return NotFound(responce);
                }
                var person = _repository.PersonalInformationRepository.Get(result.PersonId);
                string text = "کاربر گرامی ماسا بانک؛ رمز ورود شما: " + request.Pass + "این رمز یک ساعت دیگر منقضی خواهد شد.";
                string _baseUrl = "http://www.0098sms.com/sendsmslink.aspx?FROM=50002203053&TO=" + person.Mobile + "&TEXT=" + text + "&USERNAME=dsms9223&PASSWORD=54562149&DOMAIN=0098";
                HttpClient _httpClient = new();
                var smsResult = await _httpClient.GetAsync(_baseUrl);
                var smsResponce = await smsResult.Content.ReadAsStringAsync();
                if (smsResult.IsSuccessStatusCode)
                {
                    responce.Success = true;
                    responce.Data = result;
                    return Ok(responce);
                }
                responce.Errors.Add(smsResponce);
                return BadRequest(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
            finally { _repository.Dispose(); }
        }
    }
}
