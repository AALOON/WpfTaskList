using System;
using TaskList.Wpf.ViewModel;
using TaskList.Wpf.ViewModel.Abstract;

namespace TaskList.Wpf.Model
{
    /// <summary>
    /// Сервис для заполнения данными в Десайнере
    /// </summary>
    public class DesignDataService : IDataService
    {
        public void GetData(Action<ITask> removeAction, Action<ITask[], Exception> callback)
        {
            var i = 0;
            var tasks = new ITask[]
            {
                new TaskViewModel(removeAction, $"Task {++i}",25),
                new TaskViewModel(removeAction, $"Task {++i}",5),
                new TaskViewModel(removeAction, $"Task {++i}",20),
                new TaskViewModel(removeAction, $"Task {++i}",10),
                new TaskViewModel(removeAction, $"Task {++i}",28),
                new TaskViewModel(removeAction, $"Task {++i}",14),
                new TaskViewModel(removeAction, $"Task {++i}",12),
                new TaskViewModel(removeAction, $"Task {++i}",41)
            };
            callback(tasks, null);
        }
    }
}