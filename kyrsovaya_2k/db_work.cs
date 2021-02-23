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

        public int log_is_sys(string log, string pass)
        {
            string login;
            string password;
            int lvl=0;
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM user WHERE login=@login and password=@password";
            command.Parameters.AddWithValue("@login", log);
            command.Parameters.AddWithValue("@password", pass);
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        login = reader.GetString(1);
                        password = reader.GetString(2);
                        lvl = Convert.ToInt32(reader.GetString(3));
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            return lvl;
        }
        public int reg_in_sys(string login, string password,string surname, string name, string priv, string phone)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO user(login, password, lvl) VALUES(?login, ?password, ?lvl)"; //INSERT INTO `biblioteka`.`user` (`login`, `password`, `lvl`) VALUES('A1caida', 'ch3bur', '3');
            command.Parameters.Add("?login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("?password", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("?lvl", MySqlDbType.VarChar).Value = 1;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                reg_more(login, surname, name, priv, phone);
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

        public int reg_more(string login, string surname, string name, string priv, string phone)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO user_info (login_user, surname, name, patronymic, phone) VALUES(?login_user, ?surname, ?name, ?patronymic, ?phone)"; 
            command.Parameters.Add("?login_user", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("?surname", MySqlDbType.VarChar).Value = surname;
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?patronymic", MySqlDbType.VarChar).Value = priv;
            command.Parameters.Add("?phone", MySqlDbType.VarChar).Value = phone;
            try
            {
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
