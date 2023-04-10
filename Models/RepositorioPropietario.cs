using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPropietario
{
    public RepositorioPropietario()
    {
    }

    public List<Propietario> GetPropietarios(MySqlDatabase mySqlDatabase)
    {
        var propietarios = new List<Propietario>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email FROM Propietario";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                    Direccion = reader.GetString(nameof(Propietario.Direccion)),
                    Telefono = reader.GetString(nameof(Propietario.Telefono)),
                    Dni = reader.GetString(nameof(Propietario.Dni)),
                    Email = reader.GetString(nameof(Propietario.Email))
                };
                propietarios.Add(propietario);
            }

        }
        mySqlDatabase.Dispose();
        return propietarios;
    }

    public Propietario GetPropietario(MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email 
                            FROM Propietario WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                    Direccion = reader.GetString(nameof(Propietario.Direccion)),
                    Telefono = reader.GetString(nameof(Propietario.Telefono)),
                    Dni = reader.GetString(nameof(Propietario.Dni)),
                    Email = reader.GetString(nameof(Propietario.Email))
                };
                mySqlDatabase.Dispose();
                return propietario;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int CreatePropietario(MySqlDatabase mySqlDatabase, Propietario Propietario)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO Propietario (Nombre, Apellido, Direccion, Telefono, Dni, Email) 
                            VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Dni, @Email);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Nombre", Propietario.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", Propietario.Apellido);
        cmd.Parameters.AddWithValue("@Direccion", Propietario.Direccion);
        cmd.Parameters.AddWithValue("@Telefono", Propietario.Telefono);
        cmd.Parameters.AddWithValue("@Dni", Propietario.Dni);
        cmd.Parameters.AddWithValue("@Email", Propietario.Email);
        
        var res = Convert.ToInt32(cmd.ExecuteScalar());
        
        mySqlDatabase.Dispose();
        
        return res;
    }

    public int UpdatePropietario(MySqlDatabase mySqlDatabase, Propietario Propietario)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Propietario SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion, Telefono = @Telefono, Dni = @Dni, Email = @Email
                            WHERE IdPropietario = @IdPropietario;";

        cmd.Parameters.AddWithValue("@IdPropietario", Propietario.IdPropietario);
        cmd.Parameters.AddWithValue("@Nombre", Propietario.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", Propietario.Apellido);
        cmd.Parameters.AddWithValue("@Direccion", Propietario.Direccion);
        cmd.Parameters.AddWithValue("@Telefono", Propietario.Telefono);
        cmd.Parameters.AddWithValue("@Dni", Propietario.Dni);
        cmd.Parameters.AddWithValue("@Email", Propietario.Email);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int DeletePropietario( MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM Propietario WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public List<P> BuscarInmuebles(MySqlDatabase mySqlDatabase, string nombre)
}