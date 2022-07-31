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
        public async Task<IActionResult> Register(RegisterModelView request)
        {
            try
            {
                await _repository.PersonalInformationRepository.AddAsync(
                    new PersonalInformationDto
                    {
                        LastName = request.LastName,
                        NationalCode = request.NationalCode,
                        Mobile = request.Mobile
                    });
                await _repository.UserRepository.AddAsync(
                    new UserDto
                    {
                        NationalCode = request.NationalCode
                    });
                await _repository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
            }
        }
    }
}
