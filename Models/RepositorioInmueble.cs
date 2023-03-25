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
                    idInmueble = reader.GetInt32(nameof),
                    tipo = reader.GetString(nameof),
                    coordenadas = reader.GetString(nameof),
                    precio = reader.GetDecimal(nameof),
                    ambientes = reader.GetInt32(nameof),
                    uso = reader.GetString(nameof),
                    activo = reader.GetBoolean(nameof),
                    idPropietario = reader.GetInt32(nameof)
                };
                inmuebles.Add(inmueble);
            }
            return inmuebles;
        }
    }
}