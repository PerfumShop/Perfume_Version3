using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace S3Train.IdentityManager
{
    public class SmsService : IIdentityMessageService
    {
        public SmsService()
        {
        }

        public async Task SendAsync(IdentityMessage message)
        {
            if (message == null)
                return;
            await Task.Factory.StartNew(() => { }); await Task.Factory.StartNew(() => { sendMail(message); });
        }

        void sendMail(IdentityMessage message)
        {
           
        }
    }
}
