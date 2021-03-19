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
using System.Data;


namespace kyrsovaya_2k
{
    /// <summary>
    /// Логика взаимодействия для auth_ad.xaml
    /// </summary>
    public partial class take_books : Window
    {
        db_work a = new db_work("127.0.0.1", "root", "", "biblioteka");
        public take_books()
        {
            InitializeComponent();
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0");//проверь запрос
        }
        private void sear(object sender, RoutedEventArgs e)//проверь
        {
            string sel = search.Text;
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0 and surname LIKE '%" + sel + "%' OR author_info.name LIKE '%" + sel + "%' OR patronymic LIKE '%" + sel + "%' OR born LIKE '%" + sel + "%'");
        }

        private void give_to_man(object sender, RoutedEventArgs e)//проверь
        {
            DataRowView row = users.SelectedItem as DataRowView;



        }
    }
}
