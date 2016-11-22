using System;
using System.Windows.Input;
using Bizmonger.Patterns;
using Mediation;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;

namespace ViewMenu
{
    public partial class ViewModel
    {
        public ICommand NewQuote { get; private set; }
        public ICommand ViewCustomers { get; private set; }
        public ICommand ViewServices { get; private set; }
        public ICommand ViewMaterials { get; private set; }
        public ICommand ViewProfile { get; private set; }
        public ICommand PrepareEmail { get; private set; }
        public ICommand ViewBusinessPage { get; private set; }

        void ActivateCommands()
        {
            NewQuote = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_NEW_QUOTE));
            ViewCustomers = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_CUSTOMERS));
            ViewServices = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_SERVICES));
            ViewMaterials = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_MATERIALS));
            ViewProfile = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_PROFILE));
            PrepareEmail = new DelegateCommand(obj => Device.OpenUri(new Uri("mailto:support@bizmonger.net")));
            ViewBusinessPage = new DelegateCommand(obj => Device.OpenUri(new Uri(@"http://bizmonger.net")));
        }
    }
}