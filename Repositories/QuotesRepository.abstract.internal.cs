using Entities;
using Mediation;
using Repositories.Core;
using Servers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories
{
    public abstract partial class AbstractQuotesRepository
    {
        protected List<Quote> _quotes = new List<Quote>();
        protected SQLiteConnection _databaseConnection = null;
        protected IDatabase _database = null;

        protected void InitializeDatabase()
        {
            Subscribe(Messages.REQUEST_QUOTES_DATABASE_RESPONSE, obj => _database = obj as IDatabase);
            Publish(Messages.REQUEST_QUOTES_DATABASE);

            _database.Initialize();
        }

        void OnRequestQuotes(object obj) => _database.Read();

        void OnRequestQuote(object obj) => _database.Read(obj as string);

        void OnSaveQuote(object obj) => Publish(Messages.REQUEST_SAVE_QUOTE_RESPONSE, Save(obj));

        bool Save(object entity)
        {
            var quote = entity as Quote;

            Quote quoteToProcess = null;

            var existingQuote = _quotes.FirstOrDefault(s => s == quote);

            if (existingQuote == null)
            {
                quote.UserId = new ProfileServer().GetProfile().Id;
                quote.Id = quote.Id ?? Guid.NewGuid().ToString();

                _quotes.Add(quote);
                quoteToProcess = quote;
            }
            else
            {
                existingQuote.Address = quote.Address;
                existingQuote.CreatedOn = quote.CreatedOn;
                existingQuote.CustomerId = quote.CustomerId;
                existingQuote.LaborCost = quote.LaborCost;
                existingQuote.Description = quote.Description;
                existingQuote.MaterialsCost = quote.MaterialsCost;
                existingQuote.Profile = quote.Profile;
                existingQuote.Services = quote.Services;
                existingQuote.StatementNumber = quote.StatementNumber;
                existingQuote.Subtotal = quote.Subtotal;
                existingQuote.Tax = quote.Tax;
                existingQuote.Title = quote.Title;
                existingQuote.Total = quote.Total;
                existingQuote.TypeName = quote.TypeName;
                existingQuote.UserId = quote.UserId;

                quoteToProcess = existingQuote;
            }

            SaveData(quoteToProcess);

            return true;
        }
    }
}