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
    class db_work
    {
        public struct user
        {
            public string login;
            public int lvl;
            public string surname;
            public string name;
            public string patr;
            public string phone;
        }

        MySqlConnection Connection;

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


        //db_work con = new db_work("127.0.0.1", "root", "", "biblioteka");

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
                            login = reader.GetString(1),
                            lvl = reader.GetInt32(3),
                            surname = reader.GetString(4),
                            name = reader.GetString(5),
                            patr = reader.GetString(6),
                            phone = reader.GetString(7),
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
        public int log_is_sys1(string log, string pass)
        {
            string login;
            string password;
            int lvl = 0;
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT `login`,`password`,`lvl` FROM user_info WHERE login=@login and password=@password";
            command.Parameters.AddWithValue("@login", log);
            command.Parameters.AddWithValue("@password", pass);
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        login = reader.GetString(0);
                        password = reader.GetString(1);
                        lvl = Convert.ToInt32(reader.GetString(2));
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
            return lvl;
        }
        public int reg_in_sys(string login, string password,string surname, string name, string priv, string phone)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO user_info(login, password, surname, name, patronymic, phone, lvl) VALUES(?login, ?password, ?surname, ?name, ?patronymic, ?phone, ?lvl)"; //INSERT INTO `biblioteka`.`user_info` (`login`, `password`, `surname`, `name`, `patronymic`, `phone`, `lvl`) VALUES ('A1caida', 'ch3bur', 'Ахмедханов', 'Рамис', 'Нурутдинович', '79347542389', '3');
            command.Parameters.Add("?login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("?password", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("?lvl", MySqlDbType.VarChar).Value = 1;
            command.Parameters.Add("?surname", MySqlDbType.VarChar).Value = surname;
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?patronymic", MySqlDbType.VarChar).Value = priv;
            command.Parameters.Add("?phone", MySqlDbType.VarChar).Value = phone;

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
