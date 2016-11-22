using System.Collections.Generic;
using Bizmonger.Patterns;
using Entities;
using static Bizmonger.Patterns.MessageBus;
using Mediation;

namespace CreateQuote.EnterCustomer
{
    public partial class ViewModel
    {
        public DelegateCommand Previous { get; private set; }
        public DelegateCommand Generate { get; private set; }
        public DelegateCommand Load { get; private set; }

        void ActivateCommands()
        {
            Previous = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
            Generate = new DelegateCommand(OnGenerate, OnCanGenerate);
            Load = new DelegateCommand(obj => GetQuote());
        }
    }
}