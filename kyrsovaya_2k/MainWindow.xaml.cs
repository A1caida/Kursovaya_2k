using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using CsvHelper;

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

        db_work a = new db_work("95.104.192.212", "A1caida", "REvisE9023800", "A1caida");
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