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
using System.IO;



namespace kyrsovaya_2k
{
    /// <summary>
    /// Interaction logic for work_win.xaml
    /// </summary>
    /// 

    public partial class work_win : Window
    {
        db_work a = new db_work("127.0.0.1", "root", "", "biblioteka");

        private void up_to_date()
        {
            authors.DataContext = a.getTableInfoo("SELECT id AS '#', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info");//SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info
            users.DataContext = a.getTableInfoo("SELECT id AS '#', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0");
            boook.DataContext = a.getTableInfoo("SELECT id AS '#', books.name AS 'Название',  year AS 'Год', available AS 'Наличие' FROM books WHERE available>0");
            listofusers.DataContext = a.getTableInfoo("SELECT id AS '#', login AS 'Логин', surname AS 'Фамилия', NAME AS 'Имя', patronymic AS 'Отчество' FROM user_info");
            oper_id.ItemsSource = a.getTableInfoo("SELECT id, name FROM operation").AsDataView();
            magazin.DataContext = a.getTableInfoo("SELECT id AS'#', NAME AS 'Название', DATE AS 'Число' FROM magazine");
            ychet_data.DataContext = a.getTableInfoo("SELECT logs_oper.id AS '#', NAME AS 'Операция', costFO AS 'Цена за шт', how_many AS 'Кол-во', cost AS 'Цена', date_when AS 'Дата' FROM logs_oper JOIN operation ON oper_id = operation.id");
            post.DataContext = a.getTableInfoo("SELECT id AS'#', comp AS 'Название', phone AS 'Номер телефона' FROM postavshik");
            authorsid.ItemsSource = a.getTableInfoo("SELECT id, surname FROM author_info").AsDataView();

        }
        int lvl = 0;
        public work_win(List<db_work.user> Kurisu)
        {
            InitializeComponent();
            name.Text = " " + Kurisu[1].surname + " " + Kurisu[1].name + " " + Kurisu[1].patr;
            borrowed.DataContext = a.getTableInfoo("SELECT name AS 'Название', date_end AS 'Конец аренды' FROM borrowed_books JOIN books on book_id = books.id WHERE date_back IS NULL and login_id = " + Kurisu[1].id);
            recomendation.DataContext = a.getTableInfoo("SELECT id AS '#', name as 'Название' FROM books where autthor_id = (select autthor_id FROM books where id = (SELECT DISTINCT book_id FROM borrowed_books WHERE login_id = " + Kurisu[1].id+ " LIMIT 1)) ");//ПЕРЕДЕЛАЙ!!!!!

            up_to_date();

            lvl = Kurisu[1].lvl;
            switch (lvl)
            {
                case 1:
                    take.Visibility = Visibility.Collapsed; add.Visibility = Visibility.Collapsed; user_list.Visibility = Visibility.Collapsed; ychet.Visibility = Visibility.Collapsed; import_export.Visibility = Visibility.Collapsed; postavshik.Visibility = Visibility.Collapsed;//tabs
                    borroww.Visibility = Visibility.Collapsed;//buttons                   
                    break;
                case 2:
                    user_list.Visibility = Visibility.Collapsed; postavshik.Visibility = Visibility.Collapsed; //tabs
                    break;
                default:
                    break;
            }

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
            authors.DataContext = a.getTableInfoo("SELECT id AS '#', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info WHERE surname LIKE '%" + sear.Text + "%' OR author_info.name LIKE '%" + sear.Text + "%' OR patronymic LIKE '%" + sear.Text + "%' OR born LIKE '%" + sear.Text + "%'");
        }

        private void borrowww(object sender, RoutedEventArgs e)//чек на выбранную книгу
        {
            DataRowView row = boook.SelectedItem as DataRowView;
            take_books win = new take_books(row.Row.ItemArray[0].ToString());
            win.Show();
            up_to_date();
        }


        private void add_authh(object sender, RoutedEventArgs e)
        {
            string sur = a.registr_letters(authsur.Text);
            string name = a.registr_letters(authname.Text);
            string patr = a.registr_letters(authpatr.Text);
            string year = authyear.Text;

            if (a.add_authors(sur, name, patr, year) == 0)
            {
                MessageBox.Show("Авторы успешно добавлены", "Успешно", 0, MessageBoxImage.Asterisk);
                authors.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info");
            }
            else
            {
                MessageBox.Show("Ошибка добавления авторов", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }

        private void book_yea(object sender, RoutedEventArgs e)
        {
            boook.DataContext = a.getTableInfoo("SELECT id AS '#', books.name AS 'Название',  year AS 'Год', available AS 'Наличие' FROM books WHERE YEAR ='" + yea.Text + "'");
            up_to_date();
        }

        private void search_users(object sender, RoutedEventArgs e)
        {
            string sel = sear.Text;
            users.DataContext = a.getTableInfoo("SELECT id AS '#', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE surname LIKE '%" + sel + "%' OR author_info.name LIKE '%" + sel + "%' OR patronymic LIKE '%" + sel + "%' ");
        }

        private void bookss_users(object sender, RoutedEventArgs e)
        {
            DataRowView row = users.SelectedItem as DataRowView;
            user_books.DataContext = a.getTableInfoo("SELECT borrowed_books.id AS '#', name AS 'Название' FROM borrowed_books JOIN books on book_id = books.id WHERE date_back IS NULL and login_id = " + row.Row.ItemArray[0].ToString());
            up_to_date();
        }

        private void take_book(object sender, RoutedEventArgs e)
        {
            DataRowView row = user_books.SelectedItem as DataRowView;

            if ((a.back(Convert.ToInt32(row.Row.ItemArray[0].ToString())) == 0) && (a.available(row.Row.ItemArray[1].ToString()) == 0))
            {
                MessageBox.Show("Книга под номером " + row.Row.ItemArray[0] + "выдана успешно!", "Успешно", 0, MessageBoxImage.Asterisk);  //апдейт датагрида
            }
            else
            {
                MessageBox.Show("Ошибка выдачи книги", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }
        private void ban_ham(object sender, RoutedEventArgs e)
        {
            DataRowView row = listofusers.SelectedItem as DataRowView;
            if (a.ban(row.Row.ItemArray[0].ToString()) == 0)
            {
                MessageBox.Show("Пользователь успешно заблокирован!", "Успешно", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка блокировки", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }
        private void sear_us(object sender, RoutedEventArgs e)
        {

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = listofusers.SelectedItem as DataRowView;
            logsofbooks.DataContext = a.getTableInfoo("SELECT NAME AS 'Название', DATE AS 'дата', date_back,date_end FROM borrowed_books JOIN books ON book_id = books.id WHERE login_id =" + row.Row.ItemArray[0].ToString());
            up_to_date();
        }
        private void Row_DoubleClick_auth(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = authors.SelectedItem as DataRowView;
            books.DataContext = a.getTableInfoo("SELECT id AS '#', books.name AS 'Название',  year AS 'Год', available AS 'Наличие' FROM books WHERE available > 0 AND autthor_id = " + row.Row.ItemArray[0].ToString());
            up_to_date();
        }

        private void Row_DoubleClick_book(object sender, MouseButtonEventArgs e)
        {
            if (lvl != 1)
            {
                DataRowView row = books.SelectedItem as DataRowView;
                take_books win = new take_books(row.Row.ItemArray[0].ToString());
                win.Show();
                up_to_date();
            }
        }

        private void imp_auth(object sender, RoutedEventArgs e)
        {
            if (a.import() == 0)
            {
                MessageBox.Show("Авторы успешно импортированы", "Успешно", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка импортирования авторов", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }

        private void imp_book(object sender, RoutedEventArgs e)
        {
            if (a.import_books() == 0)
            {
                MessageBox.Show("Книги успешно импортированы", "Успешно", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                //  MessageBox.Show("ты кек.");
                MessageBox.Show("Ошибка экспортирования учебников", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }

        private void exp_auth(object sender, RoutedEventArgs e)
        {
            if (exp_name.Text == "")
            {
                MessageBox.Show("Название файла не может быть пустым!", "Ошибка!", 0, MessageBoxImage.Error);
            }
            else
            {
                if (a.export(exp_name.Text) == 0)
                {
                    MessageBox.Show("Авторы успешно экспортированы", "Успешно", 0, MessageBoxImage.Asterisk);
                }
                else
                {
                    // MessageBox.Show("ты кек.");
                    MessageBox.Show("Ошибка экспортирования авторов", "Ошибка", 0, MessageBoxImage.Error);
                }
            }
            up_to_date();
        }
        private void money_ych(object sender, RoutedEventArgs e)
        {
            int cost = Convert.ToInt32(money.Text) * Convert.ToInt32(lists.Text);

            if (a.moneygobrr(Convert.ToInt32(oper_id.SelectedValue), money.Text, lists.Text, cost) == 0)
            {
                MessageBox.Show("Успешно", "Успешно", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                // MessageBox.Show("ты кек.");
                MessageBox.Show("Ошибка. Обратитесь к администратору", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }

        private void search_magaz(object sender, RoutedEventArgs e)
        {
            magazin.DataContext = a.getTableInfoo("SELECT id AS'#', NAME AS 'Название', DATE AS 'Число' FROM magazine WHERE NAME LIKE '%" + sear_mag.Text + "%'");
            //up_to_date();
        }

        private void post_sear(object sender, RoutedEventArgs e)
        {
            post.DataContext = a.getTableInfoo("SELECT id AS'#', comp AS 'Название', phone AS 'Номер телефона' FROM postavshik");//тут поменяй запрос
        }

        private void add_book(object sender, RoutedEventArgs e)
        {

            if (a.add_books(Convert.ToInt32(authorsid.SelectedValue), bookname.Text, Convert.ToInt32(availbook.Text), yearbook.Text) == 0)
            {
                MessageBox.Show("Книга успешно добавлена", "Успешно", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка добавления книг", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();

        }
        private void add_magazine(object sender, RoutedEventArgs e)
        {
            if (a.add_magazine(magazname.Text, datepicker.SelectedDate) == 0)
            {
                MessageBox.Show("Журнал успешно добавлен", "Успешно", 0, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка добавления журнала", "Ошибка", 0, MessageBoxImage.Error);
            }
            up_to_date();
        }
        private void add_user(object sender, RoutedEventArgs e)
        {
            Window1 reg = new Window1();
            reg.Show();
        }

        private void log_off(object sender, RoutedEventArgs e)
        {
            MainWindow reg = new MainWindow();
            reg.Show();
            Close();
        }
    }
}
