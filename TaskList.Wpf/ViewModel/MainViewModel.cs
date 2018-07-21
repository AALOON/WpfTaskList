using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using TaskList.Wpf.Model;
using TaskList.Wpf.ViewModel.Abstract;

namespace TaskList.Wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ITask> _tasks;

        private CreationTaskViewModel _newTask;
        private readonly ICollectionView _sortedTasks;

        public MainViewModel(IDataService dataService)
        {
            InitializeCommands();

            _tasks = new ObservableCollection<ITask>();
            _newTask = CreateTaskViewModel();

            dataService.GetData(Remove,
                (items, error) =>
                {
                    if (error != null)
                        throw error;
                    if (items != null)
                        foreach (var i in items)
                            _tasks.Add(i);
                });

            var sortProperty = nameof(ITask.Progress);
            _sortedTasks = CollectionViewSource.GetDefaultView(_tasks);
            _sortedTasks.SortDescriptions.Add(new SortDescription(sortProperty, ListSortDirection.Descending));
            var liveView = (ICollectionViewLiveShaping)_sortedTasks;
            if (liveView.CanChangeLiveSorting)
            {
                liveView.LiveSortingProperties.Add(sortProperty);
                liveView.IsLiveSorting = true;
            }
        }

        private CreationTaskViewModel CreateTaskViewModel() => new CreationTaskViewModel(Add, Remove);

        private void Add(ITask task)
        {
            _tasks.Add(task);
            NewTask.Name = null;
            NewTask.Duration = null;
        }

        private void Remove(ITask task)
        {
            // If there are a lot of objects consider to remove by index
            // or use another struture
            _tasks.Remove(task);
        }

        private void InitializeCommands()
        {
        }

        public ICollectionView SortedTasks => _sortedTasks;

        public CreationTaskViewModel NewTask
        {
            get => _newTask;
            set => Set(ref _newTask, value);
        }
    }
}