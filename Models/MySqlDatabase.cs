using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class MySqlDatabase : IDisposable
    {
        public MySqlConnection Connection;
        public string connectionString = "server=localhost;port=3306;database=Inmobiliaria;uid=root;password=1234"

        public MySqlDatabase()
        {
            Connection = new MySqlConnection(this.connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}