using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInquilino
{
    public RepositorioInquilino()
    {
    }

    public List<Inquilino> GetPropietarios(MySqlDatabase mySqlDatabase)
    {
        var propietarios = new List<Inquilino>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email FROM Inquilino";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var Inquilino = new Inquilino
                {
                    IdPropietario = reader.GetInt32(nameof(Inquilino.IdPropietario)),
                    Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                    Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                    Direccion = reader.GetString(nameof(Inquilino.Direccion)),
                    Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                    Dni = reader.GetString(nameof(Inquilino.Dni)),
                    Email = reader.GetString(nameof(Inquilino.Email))
                };
                propietarios.Add(Inquilino);
            }

        }
        mySqlDatabase.Dispose();
        return propietarios;
    }

    public Inquilino GetPropietario(MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email 
                            FROM Inquilino WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var Inquilino = new Inquilino
                {
                    IdPropietario = reader.GetInt32(nameof(Inquilino.IdPropietario)),
                    Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                    Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                    Direccion = reader.GetString(nameof(Inquilino.Direccion)),
                    Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                    Dni = reader.GetString(nameof(Inquilino.Dni)),
                    Email = reader.GetString(nameof(Inquilino.Email))
                };
                mySqlDatabase.Dispose();
                return Inquilino;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int CreatePropietario(MySqlDatabase mySqlDatabase, Inquilino Inquilino)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO Inquilino (Nombre, Apellido, Direccion, Telefono, Dni, Email) 
                            VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Dni, @Email);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Nombre", Inquilino.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", Inquilino.Apellido);
        cmd.Parameters.AddWithValue("@Direccion", Inquilino.Direccion);
        cmd.Parameters.AddWithValue("@Telefono", Inquilino.Telefono);
        cmd.Parameters.AddWithValue("@Dni", Inquilino.Dni);
        cmd.Parameters.AddWithValue("@Email", Inquilino.Email);
        
        var res = Convert.ToInt32(cmd.ExecuteScalar());
        
        mySqlDatabase.Dispose();
        
        return res;
    }

    public int UpdatePropietario(MySqlDatabase mySqlDatabase, Inquilino Inquilino)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Inquilino SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion, Telefono = @Telefono, Dni = @Dni, Email = @Email
                            WHERE IdPropietario = @IdPropietario;";

        cmd.Parameters.AddWithValue("@IdPropietario", Inquilino.IdPropietario);
        cmd.Parameters.AddWithValue("@Nombre", Inquilino.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", Inquilino.Apellido);
        cmd.Parameters.AddWithValue("@Direccion", Inquilino.Direccion);
        cmd.Parameters.AddWithValue("@Telefono", Inquilino.Telefono);
        cmd.Parameters.AddWithValue("@Dni", Inquilino.Dni);
        cmd.Parameters.AddWithValue("@Email", Inquilino.Email);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int DeletePropietario( MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM Inquilino WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }
}