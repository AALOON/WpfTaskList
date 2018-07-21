using GalaSoft.MvvmLight.Command;

namespace TaskList.Wpf.ViewModel.Abstract
{
    /// <summary>
    /// Интерфес Задания
    /// </summary>
    public interface ITask
    {
        string Name { get; set; }
        int Progress { get; }
        bool IsDone { get; }

        RelayCommand RemoveCommand { get; }
    }
}