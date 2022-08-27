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
        public async Task<ActionResult<ResponceWithData<string>>> Register(RegisterModelView request)
        {
            ResponceWithData<string> responce = new();
            try
            {
                var country = await _repository.CountryRepository.AddAsync(request?.Country);
                string token = country.Continent + request?.Gender + request?.NationalCode;
                var person = await _repository.PersonalInformationRepository.AddAsync(
                    new PersonalInformationDto
                    {
                        FirstName = request?.FirstName,
                        LastName = request?.LastName,
                        NationalCode = request?.NationalCode,
                        Mobile = request?.Mobile,
                        Gender = request?.Gender,
                        CountryId = country.Id,
                        PersonalIdentity = token
                    });
                Random random = new();
                string key = random.NextInt64(10000000000000, 99999999999999).ToString();
                await _repository.UserRepository.AddAsync(
                    new UserDto
                    {
                        PersonId = person.Id,
                        Key = key,
                        Token = Cryptor.Encrypt(token+"00", key),
                        UserName = request?.NationalCode,
                        Pass = request?.Pass
                    });
                await _repository.WalletRepository.AddAsync(
                    new WalletDto
                    {
                        PersonId = person.Id
                    });
                await _repository.SaveAsync();
                string text = $"{person?.FirstName} {person?.LastName} عزیز؛ شناسه شما:\n{token}\nماسا بانک";
                string _baseUrl = $"http://www.0098sms.com/sendsmslink.aspx?FROM=50002203053&TO=0{person?.Mobile}&TEXT={text}&USERNAME=dsms9223&PASSWORD=54562149&DOMAIN=0098";
                HttpClient _httpClient = new();
                var smsResult = await _httpClient.GetAsync(_baseUrl);
                responce.Data = token;
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
        public ActionResult<ResponceWithData<UserInfoModelView>> Login(LoginModelView request)
        {
            ResponceWithData<UserInfoModelView> responce = new();
            try
            {
                var result = _repository.UserRepository.Login(request);
                if (result == null)
                {
                    responce.Errors.Add("نام کاربری یا گذرواژه نادرست است.");
                    return NotFound(responce);
                }
                result.PersonalInformation = _repository.PersonalInformationRepository.Get(result.PersonId);
                responce.Success = true;
                responce.Data = new UserInfoModelView
                {
                    FirstName = result?.PersonalInformation?.FirstName!,
                    LastName = result?.PersonalInformation?.LastName,
                    PersonId = result.PersonalInformation.Id,
                    PersonalIdentity = result.PersonalInformation.PersonalIdentity,
                    Token = result?.Token,
                    UserId = result.Id,
                    Type = result.Type
                };
                return Ok(responce);
            }
            catch (Exception ex)
            {
                responce.Add(ex);
                return BadRequest(responce);
            }
            finally { _repository.Dispose(); }
        }
        [HttpPost,Route(template:"[action]/{personId}/{userId}")]
        public async Task<ActionResult<ResponceMV>> EditPersonInfo([FromBody]RegisterModelView request, [FromRoute]Guid personId, [FromRoute]Guid userId)
        {
            ResponceMV responce = new();
            try
            {
                var person = _repository.PersonalInformationRepository.Get(personId);
                if (person == null)
                {
                    responce.Errors.Add("شخصی با این مشخصات یافت نشد");
                    return BadRequest(responce);
                }
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Gender = request.Gender;
                person.Mobile = request.Mobile;
                person.NationalCode = request.NationalCode;
                await _repository.PersonalInformationRepository.UpdateAsync(person);
                var user = _repository.UserRepository.GetUser(userId);
                if (user == null)
                {
                    responce.Errors.Add("کاربر یافت نشد");
                    return BadRequest(responce);
                }
                user.Pass = request.NewPass;
                await _repository.UserRepository.UpdateAsync(user);
                responce.Success = true;
                await _repository.SaveAsync();
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
        [HttpGet,Route(template:"[action]/{personId}")]
        public ActionResult<ResponceWithData<RegisterModelView>> GetPersonalInformations([FromRoute]Guid personId)
        {
            var result = new ResponceWithData<RegisterModelView>();
            try
            {
                var res = _repository.PersonalInformationRepository.Get(personId);
                if (res == null)
                {
                    result.Errors.Add("کاربر یافت نشد.");
                    return BadRequest(result);
                }
                res.Country = _repository.CountryRepository.Get((Guid)res.CountryId);
                result.Data = new RegisterModelView
                {
                    Country = res?.Country??new CountryDto(),
                    FirstName = res?.FirstName,
                    LastName = res?.LastName,
                    Gender =res?.Gender??0,
                    Mobile = res?.Mobile??"",
                    NationalCode = res?.NationalCode
                };
                result.Success = true;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Add(ex);
                return BadRequest(result);
            }
            finally
            {
                _repository.Dispose();
            }
        }
        [HttpPost, Route(template:"[action]/{userId}")]
        public async Task<ActionResult<ResponceMV>> BusinessAccountRequest([FromRoute]Guid userId, [FromBody]string file)
        {
            var responce = new ResponceMV();
            try
            {
                var user = _repository.UserRepository.GetUser(userId);
                user.Type = 10;
                user.Files = file;
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
        
        [HttpPost, Route(template:"[action]")]
        public ActionResult<ResponceWithData<PersonalInformationDto>> GetPersonalInfoByUserToken(TokenGetter request) 
        {
            var result = new ResponceWithData<PersonalInformationDto>();
            try
            {
                var user = _repository.UserRepository.GetByToken(request.Token);
                var person = _repository.PersonalInformationRepository.Get(user.PersonId);
                result.Data = person;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Add(ex);
                return BadRequest(result);
            }
            finally
            {
                _repository.Dispose();
            }
        }
    }
}
/*
string text = "کاربر گرامی ماسا بانک؛ رمز ورود شما: " + request.Pass + "این رمز یک ساعت دیگر منقضی خواهد شد.";
string _baseUrl = "http://www.0098sms.com/sendsmslink.aspx?FROM=50002203053&TO=" + person.Mobile + "&TEXT=" + text + "&USERNAME=dsms9223&PASSWORD=54562149&DOMAIN=0098";
HttpClient _httpClient = new();
var smsResult = await _httpClient.GetAsync(_baseUrl);
var smsResponce = await smsResult.Content.ReadAsStringAsync();
                if (smsResult.IsSuccessStatusCode)

*/