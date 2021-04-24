using System;
using System.Windows;

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

        db_work a = new db_work("95.104.192.212", "A1caida", "REvisE9023800", "A1caida");

        private void register_sys(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(log.Text) == true)
            {
                MessageBox.Show("Поле не может быть пустым!", "Ошибка", 0, MessageBoxImage.Error);
                return;
            }
            else if (String.IsNullOrEmpty(pass.Text) == true)
            {
                MessageBox.Show("Поле не может быть пустым!", "Ошибка", 0, MessageBoxImage.Error);
                return;
            }
            else if (String.IsNullOrEmpty(surname.Text) == true)
            {
                MessageBox.Show("Поле не может быть пустым!", "Ошибка", 0, MessageBoxImage.Error);
                return;
            }
            else if (String.IsNullOrEmpty(name.Text) == true)
            {
                MessageBox.Show("Поле не может быть пустым!", "Ошибка", 0, MessageBoxImage.Error);
                return;
            }
            else if (String.IsNullOrEmpty(phone.Text) == true)
            {
                MessageBox.Show("Поле не может быть пустым!", "Ошибка", 0, MessageBoxImage.Error);
                return;
            }
            string login = log.Text;
            string password = pass.Text;
            string sur = a.registr_letters(surname.Text);
            string nam = a.registr_letters(name.Text);
            string pat = a.registr_letters(priv.Text);
            string pho = phone.Text;



            if (a.reg_in_sys(login, password, sur, nam, pat, pho) == 0)
            {
                MessageBox.Show(nam + " " + sur + " " + pat + " " + ",вы успешно зарегестрировались!", "Успешно", 0, MessageBoxImage.Asterisk);
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации!", "Ошибка", 0, MessageBoxImage.Error);
            }
        }

    }
}