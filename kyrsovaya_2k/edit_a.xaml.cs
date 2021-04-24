using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для edit_a.xaml
    /// </summary>

    public partial class edit_a : Window
    {

        int lvl = 0;

        private void up_to_date(int lvl)
        {
            authorss.DataContext = a.getTableInfoo("SELECT id AS '#', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info");
            authorsid.ItemsSource = a.getTableInfoo("SELECT id, surname FROM author_info").AsDataView();
            books.DataContext = a.getTableInfoo("SELECT id AS '#',autthor_id AS 'Номер автора', books.name AS 'Название',  year AS 'Год', available AS 'Наличие' FROM books");
            if(lvl == 3)
                users.DataContext = a.getTableInfoo("SELECT id AS '#', login AS 'Логин',password AS 'Пароль', surname AS 'Фамилия', NAME AS 'Имя', patronymic AS 'Отчество', phone AS 'Номер телефона', ban AS 'Блокировка' , lvl as 'Уровень доступа' FROM user_info ");
            else
                users.DataContext = a.getTableInfoo("SELECT id AS '#', login AS 'Логин',password AS 'Пароль', surname AS 'Фамилия', NAME AS 'Имя', patronymic AS 'Отчество', phone AS 'Номер телефона', ban AS 'Блокировка' FROM user_info WHERE lvl != 3");
        }
        public edit_a(int Kurisu, int edit)
        {
            InitializeComponent();
            lvl = Kurisu;
            up_to_date(lvl);
            if (lvl != 3)
            {
                userlvl.Visibility = Visibility.Collapsed;
                texboxlvl.Visibility = Visibility.Collapsed;
            }
            user.Visibility = Visibility.Collapsed;
            book.Visibility = Visibility.Collapsed;
            authors.Visibility = Visibility.Collapsed;
            switch (edit)
            {
                case 1:
                    authors.Visibility = Visibility.Visible;
                    authors.IsSelected = true;
                    break;
                case 2:
                    book.Visibility = Visibility.Visible;
                    book.IsSelected = true;
                    break;
                default:
                    user.Visibility = Visibility.Visible;
                    user.IsSelected = true;
                    break;

            }
               
        }

        db_work a = new db_work("95.104.192.212", "A1caida", "REvisE9023800", "A1caida");

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

        private void Row_DoubleClick_auth(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = authorss.SelectedItem as DataRowView;
            authsur.Text = row.Row.ItemArray[1].ToString();
            authname.Text = row.Row.ItemArray[2].ToString();
            authpatr.Text = row.Row.ItemArray[3].ToString();
            authyear.Text = row.Row.ItemArray[4].ToString();
        }

        private void Row_DoubleClick_book(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = books.SelectedItem as DataRowView;
            authorsid.SelectedValue = row.Row.ItemArray[1];
            book_name.Text = row.Row.ItemArray[2].ToString();
            book_year.Text = row.Row.ItemArray[3].ToString();
            book_ava.Text = row.Row.ItemArray[4].ToString();

        }

        private void Row_DoubleClick_users(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = users.SelectedItem as DataRowView;
            password.Text = row.Row.ItemArray[2].ToString();
            userfam.Text = row.Row.ItemArray[3].ToString();
            username.Text = row.Row.ItemArray[4].ToString();
            userpatr.Text = row.Row.ItemArray[5].ToString();
            userphone.Text = row.Row.ItemArray[6].ToString();
            ban.Text = row.Row.ItemArray[7].ToString();
            if(lvl == 3)
                userlvl.Text = row.Row.ItemArray[8].ToString();
        }

        private void add_users(object sender, RoutedEventArgs e)
        {
            DataRowView row = users.SelectedItem as DataRowView;
            if (a.edit(Convert.ToInt32(row.Row.ItemArray[0].ToString()), userfam.Text, username.Text, userpatr.Text, password.Text, userphone.Text, Convert.ToInt32(ban.Text), Convert.ToInt32(userlvl.Text), 2) == 0)
            {
                MessageBox.Show("Пользователь успешно изменен!", "Успешно!", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка изменения пользователя", "Ошибка!", 0, MessageBoxImage.Error);
            }
            up_to_date(lvl);
        }

        private void add_authh(object sender, RoutedEventArgs e)
        {
            DataRowView row = authorss.SelectedItem as DataRowView;
            if (a.edit(Convert.ToInt32(row.Row.ItemArray[0].ToString()), authsur.Text, authname.Text, authpatr.Text, authyear.Text, "", 0, 0, 0) == 0)
            {
                MessageBox.Show("Автор успешно изменен!", "Успешно!", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка изменения автора", "Ошибка!", 0, MessageBoxImage.Error);
            }
            up_to_date(lvl);
        }

        private void add_books(object sender, RoutedEventArgs e)
        {
            DataRowView row = books.SelectedItem as DataRowView;
            if (a.edit(Convert.ToInt32(row.Row.ItemArray[0].ToString()), "", book_name.Text, "", book_year.Text, "", Convert.ToInt32(book_ava.Text), Convert.ToInt32(authorsid.SelectedValue), 1) == 0)
            {
                MessageBox.Show("Книга успешно изменена!", "Успешно!", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка изменения книги", "Ошибка!", 0, MessageBoxImage.Error);
            }
            up_to_date(lvl);
        }


        private void sear_TextChanged(object sender, TextChangedEventArgs e)
        {
            authorss.DataContext = a.getTableInfoo("SELECT id AS '#', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info WHERE surname LIKE '%" + sear.Text + "%' OR author_info.name LIKE '%" + sear.Text + "%' OR patronymic LIKE '%" + sear.Text + "%' OR born LIKE '%" + sear.Text + "%'");
        }

        private void sear_b_TextChanged(object sender, TextChangedEventArgs e)
        {
            books.DataContext = a.getTableInfoo("SELECT id AS '#',autthor_id AS 'Номер автора', books.name AS 'Название',  year AS 'Год', available AS 'Наличие' FROM books WHERE name LIKE '%" + sear_b.Text + "%'");
        }

        private void sear_u_TextChanged(object sender, TextChangedEventArgs e)
        {
            users.DataContext = a.getTableInfoo("SELECT id AS '#', login AS 'Логин',password AS 'Пароль', surname AS 'Фамилия', NAME AS 'Имя', patronymic AS 'Отчество', phone AS 'Номер телефона', ban AS 'Блокировка' FROM user_info WHERE surname LIKE '%" + sear_u.Text + "%' OR author_info.name LIKE '%" + sear_u.Text + "%' OR patronymic LIKE '%" + sear_u.Text + "%' AND lvl !=3");
        }
    }
}
