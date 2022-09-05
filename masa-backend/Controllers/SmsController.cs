using AmootSMS;
using masa_backend.ModelViews;
using masa_backend.ModelViews.AmootSms;
using Microsoft.AspNetCore.Mvc;
namespace masa_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        [HttpGet, Route("[action]/{name}/{number}/{code}")]
        public async Task<ActionResult<ResponceWithData<SendResult>>> SendOtp([FromRoute] string name, [FromRoute] string number, [FromRoute] string code)
        {
            try
            {
                var _amootSMS = new AmootSms();
                var response = new ResponceWithData<SendResult>();
                var text = $"{name} عزیز، کد یک بار مصرف شما:\n{code}\nماسا بانک";
                var result = await _amootSMS.SendMessageAsync(text, number);
                response.Data = result;
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                var responce = new ResponceWithData<SendResult>
                {
                    Success = false
                };
                responce.Add(ex);
                return BadRequest(responce);
            }
            
        }
    }
}
