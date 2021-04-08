using System;
using System.Data;
using System.Windows;

namespace kyrsovaya_2k
{
    /// <summary>
    /// Логика взаимодействия для auth_ad.xaml
    /// </summary>
    public partial class take_books : Window
    {
        db_work a = new db_work("127.0.0.1", "root", "", "biblioteka");

        int numberbook = 0;
        public take_books(string book)
        {
            InitializeComponent();
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0");//проверь запрос
            numberbook = Convert.ToInt32(book);
        }

        private void seear(object sender, RoutedEventArgs e)//проверь
        {
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0 and surname LIKE '%" + search.Text + "%' OR author_info.name LIKE '%" + search.Text + "%' OR patronymic LIKE '%" + search.Text + "%' OR born LIKE '%" + search.Text + "%'");
        }

        private void give_to_man(object sender, RoutedEventArgs e)//проверь
        {
            DataRowView row = users.SelectedItem as DataRowView;
            if ((a.give_book(Convert.ToInt32(row.Row.ItemArray[0].ToString()), numberbook) == 0) && (a.available(numberbook) == 0))
            {
                MessageBox.Show("Книга выдана!", "Успешно!", 0, MessageBoxImage.Asterisk);
                Close();
            }
            else
            {
                MessageBox.Show("Книга не выдана!", "Ошибка!", 0, MessageBoxImage.Error);
            }
        }
    }
}