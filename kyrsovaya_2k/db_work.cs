using CsvHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Windows;

namespace kyrsovaya_2k
{
    public class Foo
    {
        public string surn { get; set; }
        public string nam { get; set; }
        public string patr { get; set; }
        public string name { get; set; }
        public string year { get; set; }
        public int ava { get; set; }

    }

    public class Aut
    {
        public string surn { get; set; }
        public string name { get; set; }
        public string patr { get; set; }
        public string born { get; set; }

    }

    public class db_work
    {
        public struct user
        {
            public int id;
            public string login;
            public int lvl;
            public string surname;
            public string name;
            public string patr;
            public string phone;
            public string ban;
        }

        public struct author
        {
            public int id;
            public string surname;
            public string name;
            public string patronymic;
            public string born;
        }

        public struct books
        {
            public int id;
            public string surname;
            public string name;
            public string patronymic;
            public string book;
            public string year;
            public int ava;
        }

        MySqlConnection Connection;

        public string registr_letters(string owo)
        {
            for (int i = 0; i < owo.Length; i++)
            {
                string uwu = owo.Substring(i, 1).ToLower();
                owo = owo.Remove(i, 1);
                owo = owo.Insert(i, uwu);
            }
            owo = owo.Substring(0, 1).ToUpper() + owo.Remove(0, 1);
            return owo;
        }

        public db_work(string server, string user, string pass, string database)
        {
            MySqlConnectionStringBuilder Connect = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = user,
                Password = pass,
                Port = 3306,
                Database = database,
                CharacterSet = "utf8"
            };
            Connection = new MySqlConnection(Connect.ConnectionString);
        }

        public DataTable getTableInfoo(string query)
        {
            MySqlCommand queryExecute = new MySqlCommand(query, Connection);
            DataTable ass = new DataTable();
            Connection.Open();
            ass.Load(queryExecute.ExecuteReader());
            Connection.Close();
            return ass;
        }

        public int import_books()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return -1;
            }

            var records = new List<Foo>();
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {

                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Foo
                    {
                        surn = csv.GetField("Фамилия"),
                        nam = csv.GetField("Имя"),
                        patr = csv.GetField("Отчество"),
                        name = csv.GetField("Название"),
                        year = csv.GetField("Год"),
                        ava = csv.GetField<int>("Кол-во")
                    };
                    records.Add(record);
                }
            }

            foreach (var i in records)
            {
                MySqlCommand command = Connection.CreateCommand();
                command.CommandText = "SELECT id FROM author_info WHERE surname LIKE '%" + i.surn + "%' and author_info.name LIKE '%" + i.nam + "%' and patronymic LIKE '%" + i.patr + "%'";

                int auth_id = 0;
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        auth_id = reader.GetInt32(0);

                Connection.Close();

                int book_id = 0;
                command = Connection.CreateCommand();
                command.CommandText = "SELECT id FROM books where books.name ='" + i.name + "'";
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        book_id = reader.GetInt32(0);

                Connection.Close();

                command = Connection.CreateCommand();
                switch (book_id)
                {
                    case 0:
                        command.CommandText = "INSERT INTO books(autthor_id, name, year,available) VALUES(?autthor_id, ?name, ?year, ?available)";
                        command.Parameters.Add("?autthor_id", MySqlDbType.Int32).Value = auth_id;
                        command.Parameters.Add("?name", MySqlDbType.VarChar).Value = i.name;
                        command.Parameters.Add("?year", MySqlDbType.VarChar).Value = i.year;
                        command.Parameters.Add("?available", MySqlDbType.Int32).Value = i.ava;
                        Connection.Open();
                        command.ExecuteNonQuery();
                        Connection.Close();
                        break;
                    default:
                        command.CommandText = "UPDATE `books` SET `available`= available+ " + i.ava + " WHERE `id`=" + book_id + ";";
                        break;
                }
            }
            return 0;
        }



        public List<user> log_is_sys(string log, string pass)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM user_info WHERE login=@login and password=@password";
            command.Parameters.AddWithValue("@login", log);
            command.Parameters.AddWithValue("@password", pass);
            List<user> bd = new List<user>();
            bd.Clear();
            user databd1 = new user
            {
                login = "0",
                lvl = 0,
                surname = "0",
                name = "0",
                patr = "0",
                phone = "0",
                ban = "0"
            };
            bd.Add(databd1);
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user databd = new user
                        {
                            id = reader.GetInt32(0),
                            login = reader.GetString(1),
                            lvl = reader.GetInt32(3),
                            surname = reader.GetString(4),
                            name = reader.GetString(5),
                            patr = reader.GetString(6),
                            phone = reader.GetString(7),
                            ban = reader.GetString(8),
                        };
                        bd.Add(databd);
                    }
                }
                Connection.Close();
                return bd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            Connection.Close();
            return null;
        }
        public int add_authors(string surname, string name, string patr, string year)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO author_info(surname, name, patronymic,born) VALUES(?surname, ?name, ?patronymic, ?born)";
            command.Parameters.Add("?surname", MySqlDbType.VarChar).Value = surname;
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?patronymic", MySqlDbType.VarChar).Value = patr;
            command.Parameters.Add("?born", MySqlDbType.VarChar).Value = year;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public int add_books(int id, string name, int available, string year)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO books(autthor_id, name, year,available) VALUES(?autthor_id, ?name, ?year, ?available)";
            command.Parameters.Add("?autthor_id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?year", MySqlDbType.VarChar).Value = year;
            command.Parameters.Add("?available", MySqlDbType.Int32).Value = available;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public int add_magazine(string name, DateTime? date)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO magazine(name, date) VALUES(?name, ?date)";
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?date", MySqlDbType.Date).Value = date;


            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public int reg_in_sys(string login, string password, string surname, string name, string priv, string phone)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO user_info(login, password, surname, name, patronymic, phone, lvl, ban) VALUES(?login, ?password, ?surname, ?name, ?patronymic, ?phone, ?lvl, ?ban)";
            command.Parameters.Add("?login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("?password", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("?lvl", MySqlDbType.VarChar).Value = 1;
            command.Parameters.Add("?surname", MySqlDbType.VarChar).Value = surname;
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?patronymic", MySqlDbType.VarChar).Value = priv;
            command.Parameters.Add("?phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("?ban", MySqlDbType.Binary).Value = 0;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public int give_book(int login, int book)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO borrowed_books(login_id, book_id, date, date_end) VALUES(?login_id, ?book_id, ?date, ?date_end)";
            command.Parameters.Add("?login_id", MySqlDbType.Int32).Value = login;
            command.Parameters.Add("?book_id", MySqlDbType.Int32).Value = book;
            command.Parameters.Add("?date", MySqlDbType.Timestamp).Value = DateTime.Now;
            command.Parameters.Add("?date_end", MySqlDbType.Timestamp).Value = DateTime.Now.AddDays(14);

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public int available(int book)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE `books` SET `available`='0' WHERE  `id`=" + book;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
        public int edit(int id, string surname, string name, string patr, string year, string phone, int ava, int auth_id, int ch)
        {
            MySqlCommand command = Connection.CreateCommand();
            switch (ch)
            {
                case 0:

                    command.CommandText = "UPDATE `author_info` SET `surname`='" + surname + "',`name`= '" + name + "', `patronymic`= '" + patr + "', `born` = '" + year + "'  WHERE `id` =" + id;
                    try
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                    }
                    finally
                    {
                        Connection.Close();
                    }
                    return -1;
                case 1:

                    command.CommandText = "UPDATE `books` SET `autthor_id`=" + auth_id + ",`name`= '" + name + "', `year` = '" + year + "', `available` = " + ava + "  WHERE `id` =" + id;
                    try
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                    }
                    finally
                    {
                        Connection.Close();
                    }
                    return -1;

                default:
                    command.CommandText = "UPDATE `user_info` SET `password` = '" + year + "',`surname`='" + surname + "',`name`= '" + name + "', `patronymic`= '" + patr + "', `phone` = " + phone + ", `ban` = " + ava + ",`lvl` = "+ auth_id+ "  WHERE `id` =" + id;
                    try
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                    }
                    finally
                    {
                        Connection.Close();
                    }
                    return -1;

            }
        }
        public int available(string book)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE `books` SET `available`='1' WHERE `name`='" + book + "'";


            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
        public int back(int id)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE `borrowed_books` SET `date_back`= ?date_back WHERE `id`=" + id;
            command.Parameters.Add("?date_back", MySqlDbType.Timestamp).Value = DateTime.Now;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
        public int import()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return -1;
            }
            var records = new List<Aut>();
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                //csv.Configuration.Delimiter = ";";
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Aut
                    {
                        surn = csv.GetField("Фамилия"),
                        name = csv.GetField("Имя"),
                        patr = csv.GetField("Отчество"),
                        born = csv.GetField("Год"),
                    };
                    records.Add(record);
                }
            }


            MySqlCommand command = Connection.CreateCommand();

            try
            {
                foreach (var Kurisu in records)
                {
                    Connection.Open();
                    command.CommandText = "INSERT INTO `author_info` (`surname`, `name`, `patronymic`, `born`) VALUES ('" + Kurisu.surn + "', '" + Kurisu.name + "', '" + Kurisu.patr + "', '" + Kurisu.born + "')";
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public int export(string name, int a)
        {
            string kostil = "";
            string path = "";
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                path = dialog.SelectedPath;
            }
            path = path + "\\" + name + ".csv";
            MySqlCommand command = Connection.CreateCommand();
            if (a == 0)
            {
                try
                {
                    command.CommandText = "SELECT * FROM author_info";
                    List<author> auth = new List<author>();
                    try
                    {
                        Connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                author authh = new author
                                {
                                    id = reader.GetInt32(0),
                                    surname = reader.GetString(1),
                                    name = reader.GetString(2),
                                    patronymic = reader.GetString(3),
                                    born = reader.GetString(4),
                                };
                                auth.Add(authh);
                            }
                        }
                        Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                    }
                    Connection.Close();


                    foreach (var Kurisu in auth)
                    {
                        kostil += Convert.ToString(Kurisu.id) + ";" + Kurisu.surname + ";" + Kurisu.name + ";" + Kurisu.patronymic + ";" + Kurisu.born + "\n";
                    }
                    File.WriteAllText(path, kostil);

                    return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                }
                finally
                {
                    Connection.Close();
                }
                return -1;
            }
            else
            {
                try
                {
                    command.CommandText = "SELECT * FROM full_book_info";
                    List<books> auth = new List<books>();
                    try
                    {
                        Connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                books authh = new books
                                {
                                    id = reader.GetInt32(0),
                                    surname = reader.GetString(1),
                                    name = reader.GetString(2),
                                    patronymic = reader.GetString(3),
                                    book = reader.GetString(4),
                                    year = reader.GetString(5),
                                    ava = reader.GetInt32(6),
                                };
                                auth.Add(authh);
                            }
                        }
                        Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                    }
                    Connection.Close();


                    foreach (var Kurisu in auth)
                    {
                        kostil += Convert.ToString(Kurisu.id) + ";" + Kurisu.surname + ";" + Kurisu.name + ";" + Kurisu.patronymic + ";" + Kurisu.book + ";" + Kurisu.year + ";" + Convert.ToString(Kurisu.ava) + "\n";
                    }
                    File.WriteAllText(path, kostil);

                    return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
                    return -1;
                }

            }

        }

        public int ban(string id)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE `user_info` SET `ban`= 1 WHERE `id`=" + id;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
        public int moneygobrr(int oper, string costFO, string how_many, int cost)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO `logs_oper` (`oper_id`, `costFO`, `how_many`, `cost`, `date_when`) VALUES(?oper, ?costFO, ?how_many, ?cost, ?date_when)";
            command.Parameters.Add("?oper", MySqlDbType.Int32).Value = oper;
            command.Parameters.Add("?costFO", MySqlDbType.Int32).Value = Convert.ToInt32(costFO);
            command.Parameters.Add("?how_many", MySqlDbType.Int32).Value = Convert.ToInt32(how_many);
            command.Parameters.Add("?cost", MySqlDbType.Int32).Value = cost;
            command.Parameters.Add("?date_when", MySqlDbType.Timestamp).Value = DateTime.Now;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Ошибка! Обратитесь к администратору.", "Ошибка", 0, MessageBoxImage.Error);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
    }
}