using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UILogic
{
    [DebuggerNonUserCode]
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Disable() => BreakPromises();
        protected abstract void MakePromises();
        protected abstract void BreakPromises();

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}