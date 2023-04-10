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
        var contratos = new List<Contrato>();
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
                            i.Nombre, i.Apellido,
                            inm.Tipo, inm.Coordenadas, inm.Precio, inm.Ambientes, inm.Uso, inm.Activo,
                            p.Nombre, p.Apellido
                            FROM Contrato c
                            INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino
                            INNER JOIN Inmueble inm ON c.IdInmueble = inm.IdInmueble
                            INNER JOIN Propietario p ON inm.IdPropietario = p.IdPropietario";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var contrato = new Contrato
                    {
                        IdContrato = reader.GetInt32(nameof(Contrato.IdContrato)),
                        IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                        IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                        FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                        FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                        Inquilino = new Inquilino
                        {
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                        },
                        Inmueble = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                            Tipo = reader.GetString(nameof(Inmueble.Tipo)),
                            Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                            Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                            Ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                            Uso = reader.GetString(nameof(Inmueble.Uso)),
                            Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                            Propietario = new Propietario
                            {
                                Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            }
                        }
                    };
                    contratos.Add(contrato);
                }
            }
        }
        return contratos;
    }

    public Contrato GetContrato(MySqlDatabase mySqlDatabase, int id)
    {
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
                            i.Nombre, i.Apellido,
                            inm.Tipo, inm.Coordenadas, inm.Precio, inm.Ambientes, inm.Uso, inm.Activo,
                            p.Nombre, p.Apellido
                            FROM Contrato c
                            INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino
                            INNER JOIN Inmueble inm ON c.IdInmueble = inm.IdInmueble
                            INNER JOIN Propietario p ON inm.IdPropietario = p.IdPropietario
                            WHERE IdContrato = @IdContrato";
            cmd.Parameters.AddWithValue("@IdContrato", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var contrato = new Contrato
                    {
                        IdContrato = reader.GetInt32(nameof(Contrato.IdContrato)),
                        IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                        IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                        FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                        FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                        Inquilino = new Inquilino
                        {
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                        },
                        Inmueble = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                            Tipo = reader.GetString(nameof(Inmueble.Tipo)),
                            Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                            Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                            Ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                            Uso = reader.GetString(nameof(Inmueble.Uso)),
                            Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                            Propietario = new Propietario
                            {
                                Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                Apellido = reader.GetString(nameof(Propietario.Apellido))
                            }
                        }
                    };
                    return contrato;
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

    public int UpdateContrato(MySqlDatabase mySqlDatabase, Contrato Contrato)
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

    public int DeleteContrato(MySqlDatabase mySqlDatabase, int id)
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