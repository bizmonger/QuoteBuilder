using Connectivity;
using Documents.Generation;
using Mediation;
using OperationDependencies;
using Payloads;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;

namespace ViewQuote
{
    public partial class ViewModel
    {
        ViewQuoteDependencies _dependencies = null;

        protected override void MakePromises() =>
            Subscribe(Messages.REQUEST_QUOTE_DEPENDENCIES_RESPONSE, OnQuoteResponse);

        protected override void BreakPromises() =>
            Unsubscribe(Messages.REQUEST_QUOTE_DEPENDENCIES_RESPONSE, OnQuoteResponse);

        void SendRequests() => Publish(Messages.REQUEST_QUOTE_DEPENDENCIES);

        void OnSend(object obj)
        {
            Publish(Messages.REQUEST_QUOTE_DEPENDENCIES);

            var emailClient = GetEmailClient();
            var quote = _dependencies.Quote;
            var profile = quote.Profile;
            var customer = _dependencies.Customer;

            emailClient.Send(new EmailDependencies()
            {
                Title = quote.Title,
                FromName = profile.BusinessName,
                FromAddress = profile.Email,
                ToAddress = customer.Email,
                ToName = customer.FirstName,
                content = File.Html
            });

            IsSent = true;
            State = "Sent";

            Publish(Messages.REQUEST_SAVE_QUOTE, quote);

            Send.RaiseCanExecuteChanged();
            Home.RaiseCanExecuteChanged();
        }

        void OnQuoteResponse(object obj)
        {
            _dependencies = obj as ViewQuoteDependencies;
            File = new HtmlWebViewSource();
            File.Html = new DocumentGenerator().ExecuteAsync(_dependencies);
            Quote = _dependencies.Quote;
        }

        void OnHome(object obj)
        {
            BreakPromises();
            Publish(Messages.REQUEST_VIEW_VIEW_MENU);
        }

        IEmailClient GetEmailClient()
        {
            IEmailClient emailClient = null;
            SubscribeFirstPublication(Messages.REQUEST_EMAIL_CLIENT_RESPONSE,
                payload => emailClient = payload as IEmailClient);
            Publish(Messages.REQUEST_EMAIL_CLIENT);
            return emailClient;
        }
    }
}