using System;
using TaskList.Wpf.ViewModel.Abstract;

namespace TaskList.Wpf.Model
{
    /// <summary>
    /// Сервис без данных
    /// </summary>
    public class EmptyDataService : IDataService
    {
        public void GetData(Action<ITask> removeAction, Action<ITask[], Exception> callback)
        {
            callback(null, null);
        }
    }
}