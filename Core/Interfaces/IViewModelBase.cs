using System.ComponentModel;

namespace Interfaces.Core
{
    public interface IViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        bool IsBusy { get; set; }
    }
}