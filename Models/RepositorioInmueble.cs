using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInmueble
{
    public RepositorioInmueble()
    {
    }

    public List<Inmueble> GetInmuebles(MySqlDatabase mySqlDatabase)
    {
        var inmuebles = new List<Inmueble>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdInmueble, Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario FROM Inmueble";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                    Tipo = reader.GetString(nameof(Inmueble.Tipo)),
                    Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                    Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                    Ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                    Uso = reader.GetString(nameof(Inmueble.Uso)),
                    Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                    IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario))
                };
                inmuebles.Add(inmueble);
            }

        }
        mySqlDatabase.Dispose();
        return inmuebles;
    }

    public Inmueble GetInmueble(MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdInmueble, Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario 
                            FROM Inmueble WHERE IdInmueble = @IdInmueble";
        cmd.Parameters.AddWithValue("@IdInmueble", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                    Tipo = reader.GetString(nameof(Inmueble.Tipo)),
                    Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                    Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                    Ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                    Uso = reader.GetString(nameof(Inmueble.Uso)),
                    Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                    IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario))
                };
                mySqlDatabase.Dispose();
                return inmueble;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int 

    public int CreateInmueble(MySqlDatabase mySqlDatabase, Inmueble inmueble)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"INSERT INTO Inmueble (Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario) 
                            VALUES (@Tipo, @Coordenadas, @Precio, @Ambientes, @Uso, @Activo, @IdPropietario);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
        cmd.Parameters.AddWithValue("@Coordenadas", inmueble.Coordenadas);
        cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
        cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
        cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
        cmd.Parameters.AddWithValue("@Activo", inmueble.Activo);
        cmd.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);

        var res = Convert.ToInt32(cmd.ExecuteScalar());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int UpdateInmueble(MySqlDatabase mySqlDatabase, Inmueble inmueble)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Inmueble SET Tipo = @Tipo, Coordenadas = @Coordenadas, Precio = @Precio, Ambientes = @Ambientes, Uso = @Uso, Activo = @Activo, IdPropietario = @IdPropietario
                            WHERE IdInmueble = @IdInmueble;";

        cmd.Parameters.AddWithValue("@IdInmueble", inmueble.IdInmueble);
        cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
        cmd.Parameters.AddWithValue("@Coordenadas", inmueble.Coordenadas);
        cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
        cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
        cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
        cmd.Parameters.AddWithValue("@Activo", inmueble.Activo);
        cmd.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int DeleteInmueble( MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM Inmueble WHERE IdInmueble = @IdInmueble";
        cmd.Parameters.AddWithValue("@IdInmueble", id);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }
}