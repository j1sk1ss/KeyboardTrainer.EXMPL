using System.Windows;
using System.Windows.Documents;
using KeyboardTrainer.Objects;

namespace KeyboardTrainer.Windows {
    public partial class EditQuest {
        public EditQuest(Trainer trainer) {
            InitializeComponent();
            Trainer = trainer;
        }
        private Trainer Trainer { get; }
        private void SaveQuest(object sender, RoutedEventArgs e) {
            Trainer.Quest = new TextRange(Quest.Document.ContentStart, Quest.Document.ContentEnd).Text;
            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}