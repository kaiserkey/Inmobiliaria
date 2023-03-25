using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInmueble
{
    private MySqlDatabase MySqlDatabase { get; set; }
    public RepositorioInmueble(MySqlDatabase mySqlDatabase)
    {
        this.MySqlDatabase = mySqlDatabase;
    }

    public List<Inmueble> GetInmuebles()
    {
        var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT idInmueble, tipo, coordenadas, precio, ambientes, uso, activo, idPropietario FROM Inmueble";

        using (var reader = cmd.ExecuteReader())
        {
            var inmuebles = new List<Inmueble>();
            while (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    idInmueble = reader.GetInt32(nameof(Inmueble.idInmueble)),
                    tipo = reader.GetString(nameof(Inmueble.tipo)),
                    coordenadas = reader.GetString(nameof(Inmueble.)),
                    precio = reader.GetDecimal(nameof(Inmueble.)),
                    ambientes = reader.GetInt32(nameof(Inmueble.)),
                    uso = reader.GetString(nameof(Inmueble.)),
                    activo = reader.GetBoolean(nameof(Inmueble.)),
                    idPropietario = reader.GetInt32(nameof(Inmueble.))
                };
                inmuebles.Add(inmueble);
            }
            return inmuebles;
        }
    }
}