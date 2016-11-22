using Payloads;

namespace Connectivity
{
    public interface IEmailClient
    {
        void Send(EmailDependencies email);
    }
}