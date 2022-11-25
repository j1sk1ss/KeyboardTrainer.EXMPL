using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using KeyboardTrainer.Objects;

namespace KeyboardTrainer.Windows {
    public partial class TrainerWindow {
        public TrainerWindow(Trainer trainer, User user) {
            InitializeComponent();
            Time  = new DateTime();
            Lines = trainer.Quest.Split("\n").ToList() ?? new List<string>();
            User  = user;
            
            _timer.Tick     += TimeIncrease;
            _interface.Tick += UpdateInfo;
            
            _timer.IsEnabled     = true;
            _interface.IsEnabled = true;
        }
        private User User { get; set; }
        private DateTime Time { get; set; }
        private List<string> Lines { get; }

        private int _level;
        private int _mistakes;
        private int _position;
        
        private readonly DispatcherTimer _timer = new () {
            Interval = new TimeSpan(0,0,0,1)
        };
        private readonly DispatcherTimer _interface = new () {
            Interval = new TimeSpan(100)
        };
        private void CloseTrainer(object sender, EventArgs e) {
            new MainWindow().Show();
        }
        private void TimeIncrease(object sender, EventArgs eventArgs) {
            var addSeconds = Time.AddSeconds(1);
            Time = addSeconds;
        }
        private void UpdateInfo(object sender, EventArgs eventArgs) {
            Info.Content = $"Время: {Time:mm:ss}\n" +
                           $"Ошибки: {_mistakes}\n" +
                           $"Скорость набора: {0} сим/мин\n" +
                           $"\nВведите строку:\n{Lines[_level] ?? "NULL"}";
            ExtendedInfo.Content = $"Уровень: {User.Difficulty.ToString()}\n" +
                                   $"Строка: {_level}/{Lines.Count}/{_position}";
        }
        private void EnterChar(object sender, KeyEventArgs e) {

            DuringAnswer.Content += e.Key.ToString().ToLower() == "space" ? " " : e.Key.ToString().ToLower();
            if (DuringAnswer.Content.ToString()![_position] != Lines[_level][_position]) _mistakes++;
            if (_position++ < Lines[_level].Length - 2) return;
            DuringAnswer.Content = "";
            _position            = 0;
            _level++;
        }
    }
}