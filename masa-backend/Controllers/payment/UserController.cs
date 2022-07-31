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
        private readonly RepositoryWrapper _repository;
        public UserController(RepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpPost,Route(template:"[action]")]
        public async Task<IActionResult> Register(UserDto user)
        {
            await _repository.UserRepository.AddAsync(user);
            await _repository.SaveAsync();
            return Ok();
        }
    }
}
