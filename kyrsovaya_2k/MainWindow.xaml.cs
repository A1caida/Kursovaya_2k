using System.Windows;
using System.Collections.Generic;

namespace kyrsovaya_2k
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        db_work a = new db_work("127.0.0.1", "root", "", "biblioteka");
        private void login_sys(object sender, RoutedEventArgs e)
        {
            List<db_work.user> Kurisu = a.log_is_sys(log.Text, pass.Password);

            if (Kurisu.Count == 1)
            {
                MessageBox.Show("Неправильное имя пользователя или пароль.");
            }
            else if (Kurisu[1].ban == "0")
            {
                MessageBox.Show("Добро пожаловать, " + Kurisu[1].name + " " + Kurisu[1].patr + "!", "Вы успешно вошли!");
                work_win work = new work_win(Kurisu);
                work.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Вы забанены!", "Ошибка", 0, MessageBoxImage.Error);
            }
        }

        private void reg_sys(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для регистрации обратитесь к работнику библиотеки.", "Регистрация", 0, MessageBoxImage.Asterisk);

        }
    }
}