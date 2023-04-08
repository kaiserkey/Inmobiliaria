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
        cmd.CommandText = @"SELECT IdInmueble, tipo, coordenadas, precio, ambientes, uso, activo, idPropietario FROM Inmueble";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                    tipo = reader.GetString(nameof(Inmueble.Tipo)),
                    coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                    precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                    ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                    uso = reader.GetString(nameof(Inmueble.Uso)),
                    activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                    idPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario))
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
        cmd.CommandText = @"SELECT IdInmueble, tipo, coordenadas, precio, ambientes, uso, activo, idPropietario 
                            FROM Inmueble WHERE IdInmueble = @IdInmueble";
        cmd.Parameters.AddWithValue("@IdInmueble", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                    tipo = reader.GetString(nameof(Inmueble.Tipo)),
                    coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                    precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                    ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                    uso = reader.GetString(nameof(Inmueble.Uso)),
                    activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                    idPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario))
                };
                mySqlDatabase.Dispose();
                return inmueble;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int CreateInmueble(MySqlDatabase mySqlDatabase, Inmueble inmueble)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"INSERT INTO Inmueble (tipo, coordenadas, precio, ambientes, uso, activo, idPropietario) 
                            VALUES (@tipo, @coordenadas, @precio, @ambientes, @uso, @activo, @idPropietario);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@tipo", inmueble.Tipo);
        cmd.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);
        cmd.Parameters.AddWithValue("@precio", inmueble.Precio);
        cmd.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
        cmd.Parameters.AddWithValue("@uso", inmueble.Uso);
        cmd.Parameters.AddWithValue("@activo", inmueble.Activo);
        cmd.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);

        var recs = Convert.ToInt32(cmd.ExecuteScalar());

        mySqlDatabase.Dispose();
        
        return recs;
    }

    public int UpdateInmueble(MySqlDatabase mySqlDatabase, Inmueble inmueble)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Inmueble SET tipo = @tipo, coordenadas = @coordenadas, precio = @precio, ambientes = @ambientes, uso = @uso, activo = @activo, idPropietario = @idPropietario
                            WHERE IdInmueble = @IdInmueble;";

        cmd.Parameters.AddWithValue("@IdInmueble", inmueble.IdInmueble);
        cmd.Parameters.AddWithValue("@tipo", inmueble.Tipo);
        cmd.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);
        cmd.Parameters.AddWithValue("@precio", inmueble.Precio);
        cmd.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
        cmd.Parameters.AddWithValue("@uso", inmueble.Uso);
        cmd.Parameters.AddWithValue("@activo", inmueble.Activo);
        cmd.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);

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