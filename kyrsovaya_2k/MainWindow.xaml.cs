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
            string password = pass.Text;
            int Kurisu = a.log_is_sys(login, password);
            if (Kurisu == 0)
            {
                MessageBox.Show("Неправильное имя пользователя или пароль.");
            }
            else
            {
                MessageBox.Show(Convert.ToString(Kurisu));
            }
        }

        private void reg_sys(object sender, RoutedEventArgs e)
        {
            Window1 reg = new Window1();
            reg.Show();
        }
    }
}
