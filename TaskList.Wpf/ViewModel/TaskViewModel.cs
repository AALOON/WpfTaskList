using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TaskList.Wpf.ViewModel.Abstract;

namespace TaskList.Wpf.ViewModel
{
    /// <summary>
    /// Самая простая реализация задания
    /// </summary>
    public class TaskViewModel : ViewModelBase, ITask, IDisposable
    {
        private int _progress;
        private readonly int _seconds;
        private string _name;

        private readonly Action<ITask> _removeAction;

        private readonly BackgroundWorker _worker;
        private bool _isDone;


        public TaskViewModel(Action<ITask> removeAction, string name, int seconds = 1)
        {
            Contract.Assert(seconds > 0);

            _removeAction = removeAction;
            _name = name;
            _seconds = seconds;

            _worker = new BackgroundWorker();
            _worker.DoWork += DoWork;
            _worker.ProgressChanged += ProgressChanged;

            _worker.RunWorkerAsync();

            RemoveCommand = new RelayCommand(RemoveTaskCommand);
        }

        private void RemoveTaskCommand()
        {
            _removeAction(this);
        }


        private async void DoWork(object sender, DoWorkEventArgs e)
        {
            var seconds = _seconds  * 1000; //To milliseconds
            var delayStep = seconds / 100;

            for (int i = 0; i < 101; i++)
            {
                //Imitation of the work
                await Task.Delay(delayStep);

                Progress = i;
            }
            IsDone = true;
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }


        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int Progress
        {
            get => _progress;
            private set => Set(ref _progress, value);
        }

        public bool IsDone
        {
            get => _isDone;
            set => Set(ref _isDone, value);
        }

        public RelayCommand RemoveCommand { get; }


        public void Dispose()
        {
            _worker?.Dispose();
        }
    }
}