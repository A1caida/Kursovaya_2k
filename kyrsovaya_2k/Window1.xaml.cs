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
            string login = log.Text;
            string password = pas.Text;
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