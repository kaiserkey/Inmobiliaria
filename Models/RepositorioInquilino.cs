using Internal;
using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInquilino
{
    public RepositorioInquilino()
    {
    }

    public List<Inquilino> GetInquilinos(MySqlDatabase mySqlDatabase)
    {
        var inquilinos = new List<Inquilino>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdInquilino, Nombre, Apellido, Telefono, Dni, Email, FechaNacimiento FROM Inquilino";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var inquilino = new Inquilino
                {
                    IdInquilino = reader.GetInt32(nameof(Inquilino.IdInquilino)),
                    Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                    Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                    Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                    Dni = reader.GetString(nameof(Inquilino.Dni)),
                    Email = reader.GetString(nameof(Inquilino.Email)),
                    FechaNacimiento = reader.GetDateTime(nameof(Inquilino.FechaNacimiento))
                };
                inquilinos.Add(inquilino);
            }

        }
        mySqlDatabase.Dispose();
        return inquilinos;
    }

    public Inquilino GetInquilino(MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdInquilino, Nombre, Apellido, Telefono, Dni, Email, FechaNacimiento 
                            FROM Inquilino WHERE IdInquilino = @IdInquilino";
        cmd.Parameters.AddWithValue("@IdInquilino", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var inquilino = new Inquilino
                {
                    IdInquilino = reader.GetInt32(nameof(Inquilino.IdInquilino)),
                    Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                    Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                    Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                    Dni = reader.GetString(nameof(Inquilino.Dni)),
                    Email = reader.GetString(nameof(Inquilino.Email)),
                    FechaNacimiento = reader.GetDateTime(nameof(Inquilino.FechaNacimiento))
                };
                mySqlDatabase.Dispose();
                return inquilino;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int CreateInquilino(MySqlDatabase mySqlDatabase, Inquilino CreateInquilino)
    {
        var fechaFormat = CreateInquilino..ToString("yyyy-MM-dd HH:mm:ss")
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO Inquilino (Nombre, Apellido, Telefono, Dni, Email, FechaNacimiento) 
                            VALUES (@Nombre, @Apellido, @Telefono, @Dni, @Email);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Nombre", CreateInquilino.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", CreateInquilino.Apellido);
        cmd.Parameters.AddWithValue("@Telefono", CreateInquilino.Telefono);
        cmd.Parameters.AddWithValue("@Dni", CreateInquilino.Dni);
        cmd.Parameters.AddWithValue("@Email", CreateInquilino.Email);
        cmd.Parameters.AddWithValue("@FechaNacimiento", CreateInquilino.FechaNacimiento);
        
        var res = Convert.ToInt32(cmd.ExecuteScalar());
        
        mySqlDatabase.Dispose();
        
        return res;
    }

    public int UpdateInquilino(MySqlDatabase mySqlDatabase, Inquilino Inquilino)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Inquilino SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Dni = @Dni, Email = @Email, FechaNacimiento = @FechaNacimiento
                            WHERE IdInquilino = @IdInquilino;";

        cmd.Parameters.AddWithValue("@IdInquilino", Inquilino.IdInquilino);
        cmd.Parameters.AddWithValue("@Nombre", Inquilino.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", Inquilino.Apellido);
        cmd.Parameters.AddWithValue("@Telefono", Inquilino.Telefono);
        cmd.Parameters.AddWithValue("@Dni", Inquilino.Dni);
        cmd.Parameters.AddWithValue("@Email", Inquilino.Email);
        cmd.Parameters.AddWithValue("@FechaNacimiento", Inquilino.FechaNacimiento);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int DeleteInquilino( MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM Inquilino WHERE IdInquilino = @IdInquilino";
        cmd.Parameters.AddWithValue("@IdInquilino", id);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }
}