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
using System.Windows.Shapes;

namespace kyrsovaya_2k
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        db_work a = new db_work("127.0.0.1", "root", "", "biblioteka");

        private void register_sys(object sender, RoutedEventArgs e)
        {
            string login = log.Text;
            string password = pas.Text;
            string sur = a.registr_letters(surname.Text);
            string nam = a.registr_letters(name.Text);
            string pat = a.registr_letters(priv.Text);
            string pho = phone.Text;
            int Kurisu = a.reg_in_sys(login, password,sur,nam,pat, pho);
            if (Kurisu == 0)
            {
                MessageBox.Show("ok");
                Close();
            }
            else
            {
                MessageBox.Show("ты кек.");
            }
        }
    }
}
