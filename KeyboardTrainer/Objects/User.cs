using System;

namespace KeyboardTrainer.Objects {
    public class User {
        public User(string name, string difficulty) {
            Name = name;
            Difficulty = GetDifficulty(difficulty);
        }
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int HighResult { get; set; }
        private Difficulty GetDifficulty(string name) => name switch {
            "Низкий" => Difficulty.Low,
            "Средний" => Difficulty.Middle,
            "Высокий" => Difficulty.High,
            _ => Difficulty.Low
        };
    }
    public enum Difficulty {
        Low,
        Middle,
        High
    }
}