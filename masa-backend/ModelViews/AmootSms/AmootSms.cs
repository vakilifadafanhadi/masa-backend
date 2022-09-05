using AmootSMS;

namespace masa_backend.ModelViews.AmootSms
{
    public class AmootSms
    {
        private readonly AmootSMSWebService2SoapClient _client;
        private readonly string UserName = "09156905836";
        private readonly string Password = "Hv@0890329370";
        private readonly string LineNumber = "public";
        private readonly short CodeLength = 6;
        public AmootSms()
        {
            _client = new AmootSMSWebService2SoapClient(
                AmootSMSWebService2SoapClient.EndpointConfiguration.AmootSMSWebService2Soap12,
                "https://portal.amootsms.com/webservice2.asmx");
        }
        public async Task<SendResult> SendMessageAsync(string text,string number)
        {

            var result = await _client.SendSimpleAsync(
                UserName,
                Password,
                DateTime.Now,
                text,
                LineNumber,
                new string[]
                {
                    number
                });
            return result;
            //if (result.Status == AmootSMS.Status.Success)
            //{
            //    //خروجی
            //}
            //return
            //    await 
            //    _client
            //    .SendOTPAsync(
            //        UserName, 
            //        Password, 
            //        DateTime.Now, 
            //        name + "عزیز. رمز عبور شما در ماسا بانک", 
            //        LineNumber, 
            //        number, 
            //        CodeLength);
        }
    }
}
