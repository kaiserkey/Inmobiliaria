using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInmueble
{

    public List<Inmueble> GetInmuebles(MySqlDatabase mySqlDatabase)
    {
        var inmuebles = new List<Inmueble>();
        var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
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
        this.MySqlDatabase.Connection.Close();
        return inmuebles;
    }
}