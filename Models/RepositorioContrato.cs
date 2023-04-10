using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioContrato
{
    public RepositorioContrato()
    {
    }

    public List<Contrato> GetContratos(MySqlDatabase mySqlDatabase)
    {
        var inmuebles = new List<Contrato>();
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT IdInmueble, Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, i.IdPropietario,
                                p.Nombre, p.Apellido
                                FROM Contrato i INNER JOIN Propietario p ON i.IdPropietario = p.IdPropietario";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var Contrato = new Contrato
                    {
                        IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                        Tipo = reader.GetString(nameof(Contrato.Tipo)),
                        Coordenadas = reader.GetString(nameof(Contrato.Coordenadas)),
                        Precio = reader.GetDecimal(nameof(Contrato.Precio)),
                        Ambientes = reader.GetInt32(nameof(Contrato.Ambientes)),
                        Uso = reader.GetString(nameof(Contrato.Uso)),
                        Activo = reader.GetBoolean(nameof(Contrato.Activo)),
                        IdPropietario = reader.GetInt32(nameof(Contrato.IdPropietario)),
                        Propietario = new Propietario{
                            IdPropietario = reader.GetInt32(nameof(Contrato.IdPropietario)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        }
                    };
                    inmuebles.Add(Contrato);
                }

            }
        }
        return inmuebles;
    }

    public Contrato GetContrato(MySqlDatabase mySqlDatabase, int id)
    {
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT IdInmueble, Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, i.IdPropietario, 
                                p.Nombre, p.Apellido
                                FROM Contrato i INNER JOIN Propietario p ON i.IdPropietario = p.IdPropietario
                                WHERE IdInmueble = @IdInmueble";
            cmd.Parameters.AddWithValue("@IdInmueble", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var Contrato = new Contrato
                    {
                        IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                        Tipo = reader.GetString(nameof(Contrato.Tipo)),
                        Coordenadas = reader.GetString(nameof(Contrato.Coordenadas)),
                        Precio = reader.GetDecimal(nameof(Contrato.Precio)),
                        Ambientes = reader.GetInt32(nameof(Contrato.Ambientes)),
                        Uso = reader.GetString(nameof(Contrato.Uso)),
                        Activo = reader.GetBoolean(nameof(Contrato.Activo)),
                        IdPropietario = reader.GetInt32(nameof(Contrato.IdPropietario)),
                        Propietario = new Propietario{
                            IdPropietario = reader.GetInt32(nameof(Contrato.IdPropietario)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        }
                    };
                    mySqlDatabase.Dispose();
                    return Contrato;
                }
            }
        }
        return null;
    }

    public int CreateContrato(MySqlDatabase mySqlDatabase, Contrato CreateContrato)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            
            cmd.CommandText = @"INSERT INTO Contrato (Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario) 
                                VALUES (@Tipo, @Coordenadas, @Precio, @Ambientes, @Uso, @Activo, @IdPropietario);
                                SELECT LAST_INSERT_ID();";

            cmd.Parameters.AddWithValue("@Tipo", CreateContrato.Tipo);
            cmd.Parameters.AddWithValue("@Coordenadas", CreateContrato.Coordenadas);
            cmd.Parameters.AddWithValue("@Precio", CreateContrato.Precio);
            cmd.Parameters.AddWithValue("@Ambientes", CreateContrato.Ambientes);
            cmd.Parameters.AddWithValue("@Uso", CreateContrato.Uso);
            cmd.Parameters.AddWithValue("@Activo", CreateContrato.Activo);
            cmd.Parameters.AddWithValue("@IdPropietario", CreateContrato.IdPropietario);

            res = Convert.ToInt32(cmd.ExecuteScalar());
            CreateContrato.IdInmueble = res;
        }
        return res;
    }

    public int UpdateInmueble(MySqlDatabase mySqlDatabase, Contrato Contrato)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            
            cmd.CommandText = @"UPDATE Contrato SET Tipo = @Tipo, Coordenadas = @Coordenadas, Precio = @Precio, Ambientes = @Ambientes, Uso = @Uso, Activo = @Activo, IdPropietario = @IdPropietario
                                WHERE IdInmueble = @IdInmueble;";

            cmd.Parameters.AddWithValue("@IdInmueble", Contrato.IdInmueble);
            cmd.Parameters.AddWithValue("@Tipo", Contrato.Tipo);
            cmd.Parameters.AddWithValue("@Coordenadas", Contrato.Coordenadas);
            cmd.Parameters.AddWithValue("@Precio", Contrato.Precio);
            cmd.Parameters.AddWithValue("@Ambientes", Contrato.Ambientes);
            cmd.Parameters.AddWithValue("@Uso", Contrato.Uso);
            cmd.Parameters.AddWithValue("@Activo", Contrato.Activo);
            cmd.Parameters.AddWithValue("@IdPropietario", Contrato.IdPropietario);

            res = Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        return res;
    }

    public int DeleteInmueble( MySqlDatabase mySqlDatabase, int id)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"DELETE FROM Contrato WHERE IdInmueble = @IdInmueble";
            cmd.Parameters.AddWithValue("@IdInmueble", id);

            res = Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        return res;
    }

}