using System;
using TaskList.Wpf.ViewModel.Abstract;

namespace TaskList.Wpf.Model
{
    public interface IDataService
    {
        void GetData(Action<ITask> removeAction, Action<ITask[], Exception> callback);
    }
}