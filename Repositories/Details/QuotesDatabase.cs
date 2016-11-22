using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using SQLite;
using Servers;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories.Details
{
    public class QuotesDatabase : AbstractQuotesDatabase
    {
        SQLiteConnection _databaseConnection = null;

        public override void Initialize()
        {
            _databaseConnection = DependencyService.Get<IDatabaseConnection>().Connect();

            var tableExists = DependencyService.Get<IDatabaseConnection>().TableExists(_databaseConnection, "Quote");

            if (!tableExists)
            {
                _databaseConnection.CreateTable<Quote>();
            }
        }

        public override void OnSave(object entity)
        {
            var quote = entity as Quote;
            var existingQuote = _databaseConnection.Table<Quote>().FirstOrDefault(q => q.Id == quote.Id);

            if (existingQuote != null)
            {
                _databaseConnection.Update(existingQuote);
            }
            else
            {
                quote.Id = quote.Id ?? Guid.NewGuid().ToString();
                quote.UserId = new ProfileServer().GetProfile().Id;

                _databaseConnection.Insert(quote);

                Publish(Messages.QUOTE_ADDED, quote);
            }
        }

        public override void Read()
        {
            var profile = new ProfileServer().GetProfile();
            var quotes = ExecuteQueryStrategy(profile.Id);

            Publish(Messages.REQUEST_QUOTES_RESPONSE, quotes);
        }

        public override void Read(string id)
        {
            var existingQuote = _databaseConnection.Table<Quote>().FirstOrDefault(q => q.CustomerId == id);
            Publish(Messages.REQUEST_QUOTE_RESPONSE, existingQuote);
        }

        protected override IEnumerable<Quote> ExecuteQueryStrategy(string profileId) =>
            _databaseConnection.Table<Quote>().Where(s => s.UserId == profileId);
    }
}