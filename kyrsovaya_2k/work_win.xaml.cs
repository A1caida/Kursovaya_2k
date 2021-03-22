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
    /// Interaction logic for work_win.xaml
    /// </summary>
    public partial class work_win : Window
    {
        db_work a = new db_work("127.0.0.1", "root", "", "biblioteka");
        public work_win(List<db_work.user> Kurisu)
        {
            InitializeComponent();
            name.Text = " " + Kurisu[1].surname + " " + Kurisu[1].name + " " + Kurisu[1].patr;
            authors.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info");//SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info
            borrowed.DataContext = a.getTableInfoo("SELECT name AS 'Название', date_end AS 'Конец аренды??' FROM borrowed_books JOIN books on book_id = books.id WHERE date_back IS NULL and login_id = " + Kurisu[1].id);
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0");
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
            if (e.Handled == true)
                MessageBox.Show("Можно писать только цифры!");//сделай попап
        }

        private void LettersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Char.IsDigit(e.Text, 0);
            if (e.Handled == true)
                MessageBox.Show("Можно писать только буквы!");
        }
        private void search_auth(object sender, RoutedEventArgs e)
        {          
            string sel = sear.Text;
            authors.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info WHERE surname LIKE '%" + sel + "%' OR author_info.name LIKE '%" + sel + "%' OR patronymic LIKE '%" + sel + "%' OR born LIKE '%" + sel + "%'");//AS 'Номер'
        }

        private void bookss_auth(object sender, RoutedEventArgs e)
        {
            DataRowView row = authors.SelectedItem as DataRowView;
            books.DataContext = a.getTableInfoo("SELECT id AS 'Номер', books.name AS 'Название',  year AS 'Год', available AS 'Наличие' FROM books WHERE available > 0 AND autthor_id = " + row.Row.ItemArray[0].ToString());
        }

        private void borrow_bookss(object sender, RoutedEventArgs e)//чек на выбранную книгу
        {
            DataRowView row = books.SelectedItem as DataRowView;
            take_books win = new take_books(row.Row.ItemArray[0].ToString());
            win.Show();
        }

        private void add_authh(object sender, RoutedEventArgs e)
        {
            string sur = authsur.Text;
            string name = authname.Text;
            string patr = authpatr.Text;
            string year = authyear.Text;

            if (a.add_authors(sur, name, patr, year) == 0)
            {
                MessageBox.Show("ok");
                authors.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM autthor_info");
            }
            else
            {
                MessageBox.Show("ты кек.");
            }

        }

        private void search_users(object sender, RoutedEventArgs e)
        {
            string sel = sear.Text;
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE surname LIKE '%" + sel + "%' OR author_info.name LIKE '%" + sel + "%' OR patronymic LIKE '%" + sel + "%' ");//AS 'Номер'
        }

        private void bookss_users(object sender, RoutedEventArgs e)
        {
            DataRowView row = users.SelectedItem as DataRowView;
            user_books.DataContext = a.getTableInfoo("SELECT borrowed_books.id AS 'Номер', name AS 'Название' FROM borrowed_books JOIN books on book_id = books.id WHERE date_back IS NULL and login_id = " + row.Row.ItemArray[0].ToString());
        }

        private void take_book(object sender, RoutedEventArgs e)
        {        
            DataRowView row = user_books.SelectedItem as DataRowView;
            //a.back(Convert.ToInt32(row.Row.ItemArray[0].ToString()));
            //a.available(row.Row.ItemArray[1].ToString());//потом сделай бокс

            if((a.back(Convert.ToInt32(row.Row.ItemArray[0].ToString())) == 0) && (a.available(row.Row.ItemArray[1].ToString())==0))
            {
                MessageBox.Show("ok");//апдейт датагрида
            }
            else
            {
                MessageBox.Show("ты кек.");
            }

        }
    }
}
