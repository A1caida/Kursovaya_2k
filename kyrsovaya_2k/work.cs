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
    class work
    {
        MySqlConnection Connection;
        db_work con = new db_work("127.0.0.1", "root", "", "biblioteka");

        public int log_is_sys(string log, string pass)
        {
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

                        string login = reader.GetString(1);
                        string password = reader.GetString(2);
                        int lvl = Convert.ToInt32(reader.GetString(3));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
    }
}
