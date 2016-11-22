using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Payloads;

namespace Connectivity
{
    public class Email
    {
        public async Task SendEmail(IEmailClient client, EmailDependencies email)
        {
            var connected = await Connection.IsOnline();
            if (!connected) return;

            try
            {
                client.Send(email);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}