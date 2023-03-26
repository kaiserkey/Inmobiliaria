using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class MySqlDatabase : IDisposable
    {
        public MySqlConnection Connection;
        public 

        public MySqlDatabase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}