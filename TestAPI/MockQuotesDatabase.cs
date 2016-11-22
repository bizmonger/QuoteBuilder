using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Entities;
using Mediation;
using Repositories.Core;
using static Bizmonger.Patterns.MessageBus;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockQuotesDatabase : AbstractQuotesDatabase
    {
        public List<Quote> Quotes { get; set; } = new List<Quote>();

        void MakePromises() =>
            Subscribe(Messages.REQUEST_SAVE_QUOTE, OnSave);

        void BreakPromises() =>
            Unsubscribe(Messages.REQUEST_SAVE_QUOTE, OnSave);

        public override void Read()
        {
            Publish(Messages.REQUEST_QUOTES_RESPONSE, Quotes);
        }

        public override void Read(string id)
        {
            var service = Quotes.FirstOrDefault(m => m.Id == id);
            Publish(Messages.REQUEST_QUOTE_RESPONSE, service);
        }

        public override void OnSave(object entity)
        {
            var quote = entity as Quote;
            Quotes.Add(quote);
            Publish(Messages.QUOTE_ADDED, quote);
        }

        public override void Initialize() { }

        protected override IEnumerable<Quote> ExecuteQueryStrategy(string profileId) =>
            Quotes.Where(s => s.UserId == profileId).ToList();
    }
}