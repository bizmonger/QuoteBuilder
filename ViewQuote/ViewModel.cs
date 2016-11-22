using Entities;
using System.Diagnostics;
using UILogic;
using Xamarin.Forms;

namespace ViewQuote
{
    [DebuggerNonUserCode]
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        HtmlWebViewSource _file = null;
        public HtmlWebViewSource File
        {
            get { return _file; }
            set
            {
                if (_file != value)
                {
                    _file = value;
                    OnPropertyChanged();
                }
            }
        }

        string _state = null;
        public string State
        {
            get { return _state; }
            set
            {
                if (_state != value?.Trim())
                {
                    _state = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        bool _isSent = false;
        public bool IsSent
        {
            get { return _isSent; }
            set
            {
                if (_isSent != value)
                {
                    _isSent = value;
                    OnPropertyChanged();
                }
            }
        }

        public Quote Quote { get; set; }
    }
}