using System;
using System.Diagnostics.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TaskList.Wpf.ViewModel.Abstract;

namespace TaskList.Wpf.ViewModel
{
    /// <summary>
    /// Служит для конфигурации простого задания
    /// </summary>
    public class CreationTaskViewModel : ViewModelBase, ICreationTask
    {
        private readonly Action<ITask> _addAction;
        private readonly Action<ITask> _removeAction;
        private string _name;
        private int? _duration;

        public CreationTaskViewModel(Action<ITask> addAction, Action<ITask> removeAction)
        {
            _addAction = addAction;
            _removeAction = removeAction;
            _name = string.Empty;
            _duration = null;

            AddCommand = new RelayCommand(CreateTaskCommand);
        }

        private void CreateTaskCommand()
        {
            if(!CanExecute)
                throw new ArgumentException(nameof(Duration));

            Contract.Assert(_duration != null);

            _addAction(new TaskViewModel(_removeAction, _name, _duration.Value));
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int? Duration
        {
            get => _duration;
            set
            {
                Set(ref _duration, value);
                RaisePropertyChanged(nameof(CanExecute));
            }
        }

        public RelayCommand AddCommand { get; }

        public bool CanExecute => _duration != null && _duration > 0;
    }
}