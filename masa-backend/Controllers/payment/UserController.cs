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
        public async Task<ActionResult<ResponceMV>> Register(RegisterModelView request)
        {
            ResponceMV responce = new();
            try
            {
                var person = await _repository.PersonalInformationRepository.AddAsync(
                    new PersonalInformationDto
                    {
                        LastName = request.LastName,
                        NationalCode = request.NationalCode,
                        Mobile = request.Mobile
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
        public ActionResult<ResponceWithData<UserDto>> Login(LoginModelView request)
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
                responce.Success = true;
                responce.Data = result;
                return Ok(responce);
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
