using System.ComponentModel;

namespace Interfaces.Core
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        bool IsBusy { get; set; }
    }
}