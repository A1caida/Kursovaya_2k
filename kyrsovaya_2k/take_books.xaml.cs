<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> 44e6079324c99ab571c4eb2bd6dec9873afc6f13
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
<<<<<<< HEAD
        int numberbook = 0;
        public take_books(string book)
        {
            InitializeComponent();
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0");//проверь запрос
            numberbook = Convert.ToInt32(book);
        }

        private void seear(object sender, RoutedEventArgs e)//проверь
=======
        public take_books()
        {
            InitializeComponent();
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0");//проверь запрос
        }
        private void sear(object sender, RoutedEventArgs e)//проверь
>>>>>>> 44e6079324c99ab571c4eb2bd6dec9873afc6f13
        {
            string sel = search.Text;
            users.DataContext = a.getTableInfoo("SELECT id AS 'Номер', surname AS 'Фамилия', user_info.name AS 'Имя', patronymic AS 'Отчество' FROM user_info WHERE ban = 0 and surname LIKE '%" + sel + "%' OR author_info.name LIKE '%" + sel + "%' OR patronymic LIKE '%" + sel + "%' OR born LIKE '%" + sel + "%'");
        }

        private void give_to_man(object sender, RoutedEventArgs e)//проверь
        {
            DataRowView row = users.SelectedItem as DataRowView;
<<<<<<< HEAD
            int b = a.give_book(Convert.ToInt32(row.Row.ItemArray[0].ToString()), numberbook);
            int av = a.available(numberbook);
            if ((b == 0) && (av == 0))
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
=======



        }
    }
}
>>>>>>> 44e6079324c99ab571c4eb2bd6dec9873afc6f13
