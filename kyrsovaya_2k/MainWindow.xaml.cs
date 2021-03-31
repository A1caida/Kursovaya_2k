using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string login = log.Text;
            string password = pass.Password;
            List<db_work.user> Kurisu = a.log_is_sys(login, password);

            if (Kurisu.Count == 1)
            {
                MessageBox.Show("Неправильное имя пользователя или пароль.");
            }
            else if (Kurisu[1].ban == "0")
            {
                MessageBox.Show("Добро пожаловать, " + Kurisu[1].name + " " + Kurisu[1].patr + "!","Вы успешно вошли!");
                work_win work= new work_win(Kurisu);
                work.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Вам бан");
            }
        }

        private void reg_sys(object sender, RoutedEventArgs e)
        {
            Window1 reg = new Window1();
            reg.Show();
        }
    }
}
