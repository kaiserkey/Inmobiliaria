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
        cmd.CommandText = @"SELECT idInmueble, tipo, coordenadas, precio, ambientes, uso, activo, idPropietario FROM Inmueble";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    idInmueble = reader.GetInt32(nameof(Inmueble.idInmueble)),
                    tipo = reader.GetString(nameof(Inmueble.tipo)),
                    coordenadas = reader.GetString(nameof(Inmueble.coordenadas)),
                    precio = reader.GetDecimal(nameof(Inmueble.precio)),
                    ambientes = reader.GetInt32(nameof(Inmueble.ambientes)),
                    uso = reader.GetString(nameof(Inmueble.uso)),
                    activo = reader.GetBoolean(nameof(Inmueble.activo)),
                    idPropietario = reader.GetInt32(nameof(Inmueble.idPropietario))
                };
                inmuebles.Add(inmueble);
            }

        }
        mySqlDatabase.Dispose();
        return inmuebles;
    }

    public int CreateInmueble(MySqlDatabase mySqlDatabase, Inmueble inmueble)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"INSERT INTO Inmueble (tipo, coordenadas, precio, ambientes, uso, activo, idPropietario) 
                            VALUES (@tipo, @coordenadas, @precio, @ambientes, @uso, @activo, @idPropietario);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@tipo", inmueble.tipo);
        cmd.Parameters.AddWithValue("@coordenadas", inmueble.coordenadas);
        cmd.Parameters.AddWithValue("@precio", inmueble.precio);
        cmd.Parameters.AddWithValue("@ambientes", inmueble.ambientes);
        cmd.Parameters.AddWithValue("@uso", inmueble.uso);
        cmd.Parameters.AddWithValue("@activo", inmueble.activo);
        cmd.Parameters.AddWithValue("@idPropietario", inmueble.idPropietario);

        var recs = Convert.ToInt32(cmd.ExecuteScalar());

        mySqlDatabase.Dispose();
        
        return recs;
    }

    public 
}