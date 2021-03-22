using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Windows;

namespace kyrsovaya_2k
{
   
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
        

        MySqlConnection Connection;
       
       public string registr_letters(string owo)
        {
            owo.ToLower();
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
        public int reg_in_sys(string login, string password,string surname, string name, string priv, string phone)
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
            command.CommandText = "UPDATE `biblioteka`.`books` SET `available`='0' WHERE  `id`=" + book;     
           
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
        public int available(string book)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE `biblioteka`.`books` SET `available`='1' WHERE `name`='" + book+ "'";


            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            command.CommandText = "UPDATE `biblioteka`.`borrowed_books` SET `date_back`= ?date_back WHERE `id`=" + id;
            command.Parameters.Add("?date_back", MySqlDbType.Timestamp).Value = DateTime.Now;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }
    }
}
