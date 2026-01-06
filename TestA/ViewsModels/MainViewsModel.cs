using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestA.Commands;
using System.Threading.Tasks;
using TestA.Services;

namespace TestA.ViewsModels
{
    class MainViewsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _code;
        private string _selectLanguage;
        private string _status;
        private string _output;

        private int _timeLimit;
        private int _meomoryLimit;

        private bool _isRunning;

        private readonly HackerEarthService _hackerEarthService = new();

        private bool _isBusy;
        public bool CanRun => !IsBusy;


        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRun));
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<string> Languages { get; } =
            new ObservableCollection<string>
            {
                "C#",
                "Python",
                "JavaScript"
            };
        public string Code
        {
            get => _code;
            set { _code = value; OnPropertyChanged(); }
        }

        public string SelectedLanguage
        {
            get => _selectLanguage;
            set { _selectLanguage = value; OnPropertyChanged(); }
        }
        public int TimeLimit
        {
            get => _timeLimit;
            set { _timeLimit = value; OnPropertyChanged(); }
        }

        public int MemoryLimit
        {
            get => _meomoryLimit;
            set { _meomoryLimit = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public string Output
        {
            get => _output;
            set { _output = value; OnPropertyChanged(); }
        }

        public bool isRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
                RunCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand RunCommand { get; }

        public MainViewsModel()
        {
            SelectedLanguage = "C#";
            TimeLimit = 2;
            MemoryLimit = 64;
            Status = "Idle";
            Code = "// Write your code here";

            RunCommand = new RelayCommand(
                execute: RunCode,
                canExecute: () => !isRunning

                );
        }

        private async void RunCode()
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                Output = "Error: Code is empty.";
                return;
            }

            try
            {
                var result = await _hackerEarthService.ExecuteCodeAsync(
                    Code,
                    SelectedLanguage,
                    TimeLimit,
                    MemoryLimit);

                Output = result;
                Status = "Done";
            }
            catch (System.Exception ex)
            {
                Output = "Error: " + ex.Message;
                Status = "Error";
            }
            finally
            {
                isRunning = false;
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
