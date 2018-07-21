using GalaSoft.MvvmLight.Command;

namespace TaskList.Wpf.ViewModel.Abstract
{
    /// <summary>
    /// Интерфейс создания задания
    /// </summary>
    public interface ICreationTask
    {
        string Name { get; set; }

        int? Duration { get; set; }

        RelayCommand AddCommand { get; }
    }
}