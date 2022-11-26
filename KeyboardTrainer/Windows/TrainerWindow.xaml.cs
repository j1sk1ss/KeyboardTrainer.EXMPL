using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
        private float _speed;
        private int _tempSpeed;
        
        private readonly DispatcherTimer _timer = new () {
            Interval = new TimeSpan(0,0,0,1)
        };
        private readonly DispatcherTimer _interface = new () {
            Interval = new TimeSpan(100)
        };
        private void TimeIncrease(object sender, EventArgs eventArgs) {
            var addSeconds = Time.AddSeconds(1);

            _speed = _tempSpeed;
            _tempSpeed = 0;
            
            Time = addSeconds;
        }
        private void UpdateInfo(object sender, EventArgs eventArgs) {
            Info.Content = $"Время: {Time:mm:ss}\n" +
                           $"Ошибки: {_mistakes}\n" +
                           $"Скорость: {_speed} сим/мин\n" +
                           $"\nВведите строку:\n{Lines[_level] ?? "NULL"}";
            ExtendedInfo.Content = $"Уровень: {User.Difficulty.ToString()}\n" +
                                   $"Строка: {_level}/{Lines.Count}\n" +
                                   $"Позиция: {_position}";
            if (User.HighResult < _speed) User.HighResult = _speed;
        }
        private void EnterChar(object sender, KeyEventArgs e) {
            var temp = GetString(e.Key.ToString().ToLower());
            if (temp == "") return;
            _tempSpeed++;
            DuringAnswer.Content += temp;
            
            if (DuringAnswer.Content.ToString()![_position] != Lines[_level][_position]) _mistakes++;
            
            if (_position++ < Lines[_level].Length - 2) return;
            DuringAnswer.Content = "";
            _position = 0;
            if (++_level < Lines.Count - 1) return;

            MessageBox.Show($"Вы прошли!\nМаксимальная скорость: {User.HighResult}\n" +
                            $"Колличество ошибок: {_mistakes}", "Резулитат.");
            _timer.IsEnabled = false;
            new MainWindow().Show();
            this.Close();
        }

        private readonly List<string> _disableCharacters = new() {
            "space", "leftctrl", "alt", "back", "rightctrl", "leftshift", "return", "system"
        };
        private string GetString(string symbol) => _disableCharacters.Contains(symbol) ? " " : symbol;
    }
}