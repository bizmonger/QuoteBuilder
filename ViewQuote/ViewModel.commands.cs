using Bizmonger.Patterns;

namespace ViewQuote
{
    public partial class ViewModel
    {
        public DelegateCommand Send { get; private set; }
        public DelegateCommand Home { get; private set; }

        void ActivateCommands()
        {
            State = "Send";
            Send = new DelegateCommand(OnSend, obj => State == "Send");
            Home = new DelegateCommand(OnHome);
        }
    }
}