using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Runtime.CompilerServices;


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

        public MainViewsModel()
        {
            SelectedLanguage = "C#";
            TimeLimit = 2;
            MemoryLimit = 64;
            Status = "Idle";
            Code = "Write your code here /n";
        }
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
