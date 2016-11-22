using System.Diagnostics;
using Connectivity;
using Payloads;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockEmailClient : IEmailClient
    {
        public void Send(EmailDependencies email)
        {
            Debug.WriteLine("MockEmailClient::Send()");
        }
    }
}