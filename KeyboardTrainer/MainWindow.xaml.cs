using System.Windows;
using KeyboardTrainer.Objects;
using KeyboardTrainer.Windows;

namespace KeyboardTrainer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Trainer = new Trainer();
        }
        private Trainer Trainer { get; }
        private void StartTrainer(object sender, RoutedEventArgs e) {
            if (UserName.Text == "") {
                MessageBox.Show("Введите имя!");
                return;
            }
            if (Trainer.Quest == null) {
                MessageBox.Show("Введите задание!");
                return;
            }
            new TrainerWindow(Trainer, new User(UserName.Text, Difficult.Text)).Show();
            this.Close();
        }
        private void ChangeQuest(object sender, RoutedEventArgs e) {
            new EditQuest(Trainer).Show();
        }
        private void GetInfo(object sender, RoutedEventArgs e) {
            new Info().Show();
        }
    }
}