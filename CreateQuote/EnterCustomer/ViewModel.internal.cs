using Entities;
using IO;
using Mediation;
using Mediation.Validation;
using OperationDependencies;
using Servers;
using System;
using System.Collections.Generic;
using static Bizmonger.Patterns.MessageBus;

namespace CreateQuote.EnterCustomer
{
    public partial class ViewModel
    {
        const string TEMPLATES_DIRECTORY = @"QuoteBuilder\Templates";
        const string STATEMENT_TYPE_QUOTE = "Quote";

        Customer _customer = null;
        IRead _fileReader = null;
        Quote _quote = null;

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_FILE_READER_RESPONSE, OnRequestFileReaderResponse);
            Subscribe(Messages.REQUEST_QUOTE_DEPENDENCIES, OnGetQuoteDependenciesResponse);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_FILE_READER_RESPONSE, OnRequestFileReaderResponse);
            Unsubscribe(Messages.REQUEST_QUOTE_DEPENDENCIES, OnGetQuoteDependenciesResponse);
        }

        void SendRequests()
        {
            Publish(Messages.REQUEST_SERVICES);
            Publish(Messages.REQUEST_FILE_READER);
        }

        void OnGetQuoteDependenciesResponse(object obj) =>
            Publish(Messages.REQUEST_QUOTE_DEPENDENCIES_RESPONSE, AssignQuoteDependencies());


        ViewQuoteDependencies AssignQuoteDependencies() => new ViewQuoteDependencies()
        {
            Customer = _customer,
            FileReader = _fileReader,
            Quote = GetQuote(),
            Logo = "HeaderLogo_Quote.jpg"
        };

        Quote GetQuote()
        {
            var registry = new CalculateTransaction.ViewModel();

            foreach (var sevice in SelectedServices)
            {
                registry.AddService.Execute(sevice);
            }

            var profile = new ProfileServer().GetProfile();

            _quote = new Quote()
            {
                Id = Guid.NewGuid().ToString(),
                TypeName = STATEMENT_TYPE_QUOTE,
                Title = Title,
                UserId = profile.Id,
                CloudSynced = false,
                CreatedOn = DateTime.Now,
                StatementNumber = GetStatementNumber(),
                Profile = profile,
                Services = new List<Service>(SelectedServices),
                Address = new Address(),
                Total = registry.Total,
                Subtotal = registry.Subtotal,
                Tax = registry.Tax
            };

            Subtotal = _quote.Subtotal;
            Tax = _quote.Tax;
            Total = _quote.Total;

            return _quote;
        }

        int GetStatementNumber()
        {
            int statementNumber = 0;
            SubscribeFirstPublication(Messages.REQUEST_NEW_QUOTE_STATEMENT_NUMBER_RESPONSE, obj => statementNumber = (int)obj);
            Publish(Messages.REQUEST_NEW_QUOTE_STATEMENT_NUMBER);

            return statementNumber;
        }

        void OnRequestFileReaderResponse(object obj) => _fileReader = obj as IRead;

        void OnGenerate(object obj)
        {
            var profile = new ProfileServer().GetProfile();
            var canEmailQuote = !string.IsNullOrEmpty(profile.Email);

            if (!canEmailQuote)
            {
                Publish(Messages.REQUEST_VIEW_PROFILE);
            }
            else
            {
                InitializeCustomer();
                Publish(Messages.REQUEST_VIEW_QUOTE, _customer);
            }
        }

        void InitializeCustomer() =>
            _customer = new Customer()
            {
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Email = Email
            };

        bool OnCanGenerate(object obj)
        {
            InitializeCustomer();
            return new CustomerValidator().Validate(_customer);
        }
    }
}