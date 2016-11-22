using Entities;
using Mediation;
using Repositories.Core;
using System.Collections.Generic;
using System.Linq;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories
{
    public abstract partial class AbstractQuotesRepository : AbstractPromise
    {
        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_NEW_QUOTE_STATEMENT_NUMBER, OnNewStateMentNumber);
            Subscribe(Messages.REQUEST_SAVE_QUOTE, OnSaveQuote);
            Subscribe(Messages.REQUEST_QUOTE, OnRequestQuote);
            Subscribe(Messages.REQUEST_QUOTES, OnRequestQuotes);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SAVE_QUOTE, OnSaveQuote);
            Unsubscribe(Messages.REQUEST_QUOTE, OnRequestQuote);
            Unsubscribe(Messages.REQUEST_QUOTES, OnRequestQuotes);
        }

        void OnNewStateMentNumber(object obj)
        {
            IEnumerable<Quote> quotes = null;
            SubscribeFirstPublication(Messages.REQUEST_QUOTES_RESPONSE, payload => quotes = payload as IEnumerable<Quote>);
            _database.Read();

            Publish(Messages.REQUEST_NEW_QUOTE_STATEMENT_NUMBER_RESPONSE, quotes.Count() + 1);
        }

        protected void SendRequests() => Read();

        protected abstract void Read();

        protected abstract void SaveData(Quote quote);
    }
}