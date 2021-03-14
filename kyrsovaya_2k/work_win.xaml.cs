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
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void search_auth(object sender, RoutedEventArgs e)
        {
            int sel = authors.SelectedIndex;//фикс плз
            authors.DataContext = a.getTableInfoo("SELECT id AS 'Номер', name AS 'Название', books.year AS 'Год',available AS 'Наличие' FROM books WHERE autthor_id = " + sel+1);//AS 'Номер'
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
                authors.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', author_info.name AS 'Имя', patronymic AS 'Отчество',born AS 'Год рождения' FROM author_info");
            }
            else
            {
                MessageBox.Show("ты кек.");
            }

        }

    }
}
