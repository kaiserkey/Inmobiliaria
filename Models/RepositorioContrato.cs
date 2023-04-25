using Internal;
using System;
using MySql.Data.MySqlClient;
using System.Globalization;

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
        var fechaInicioFormat = CreateContrato.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
        var fechaFinFormat = CreateContrato.FechaFin.ToString("yyyy-MM-dd HH:mm:ss");
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {

            cmd.CommandText = @"INSERT INTO Contrato (IdInquilino, IdInmueble, FechaInicio, FechaFin) 
                            VALUES (@IdInquilino, @IdInmueble, @FechaInicio, @FechaFin);
                            SELECT LAST_INSERT_ID();";

            cmd.Parameters.AddWithValue("@IdInquilino", CreateContrato.IdInquilino);
            cmd.Parameters.AddWithValue("@IdInmueble", CreateContrato.IdInmueble);
            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicioFormat);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFinFormat);

            res = Convert.ToInt32(cmd.ExecuteScalar());
            CreateContrato.IdContrato = res;
        }
        return res;
    }

    public int UpdateContrato(MySqlDatabase mySqlDatabase, Contrato contrato)
    {
        var fechaInicioFormat = contrato.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
        var fechaFinFormat = contrato.FechaFin.ToString("yyyy-MM-dd HH:mm:ss");
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {

            cmd.CommandText = @"UPDATE Contrato SET IdInquilino = @IdInquilino, IdInmueble = @IdInmueble, FechaInicio = @FechaInicio, FechaFin = @FechaFin
                            WHERE IdContrato = @IdContrato;";

            cmd.Parameters.AddWithValue("@IdContrato", contrato.IdContrato);
            cmd.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
            cmd.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicioFormat);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFinFormat);

            res = Convert.ToInt32(cmd.ExecuteNonQuery());

        }
        return res;
    }

    public int DeleteContrato(MySqlDatabase mySqlDatabase, int id)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"DELETE FROM Contrato WHERE IdContrato = @IdContrato";
            cmd.Parameters.AddWithValue("@IdContrato", id);

            res = Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        return res;
    }

    public List<Contrato> BuscarContrato(MySqlDatabase mySqlDatabase, string busqueda, string buscarPor)
    {

        var contratos = new List<Contrato>();
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {

            var query = "";

            if (buscarPor.Equals("Inquilino"))
            {
                query = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
                            i.Nombre, i.Apellido, i.Dni
                            FROM Contrato c
                            INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino
                            WHERE CONCAT(i.Nombre, ' ', i.Apellido) LIKE @busqueda LIMIT 10";
            }
            else if (buscarPor.Equals("FechaInicio") || buscarPor.Equals("FechaFin"))
            {
                query = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
                            i.Nombre, i.Apellido, i.Dni
                            FROM Contrato c
                            INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino 
                            WHERE " + buscarPor + " LIKE @busqueda LIMIT 10";
            }
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
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
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                        }
                    };
                    contratos.Add(contrato);
                }
            }
        }
        return contratos;
    }

    public List<Contrato> BuscarContratosPorFecha(MySqlDatabase mySqlDatabase, string fechaDesde, string fechaHasta)
    {
        var contratos = new List<Contrato>();

        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
            i.Nombre, i.Apellido, i.Dni
            FROM Contrato c
            INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino
            WHERE c.FechaInicio <= @fechaHasta AND c.FechaFin >= @fechaDesde
            LIMIT 10";

            cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde);
            cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta);

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
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                        }
                    };
                    contratos.Add(contrato);
                }
            }
        }
        return contratos;
    }

    public List<Contrato> BuscarContratosPorCodigo(MySqlDatabase mySqlDatabase, int codigo, string opcion)
    {
        var contratos = new List<Contrato>();

        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            var query = "";

            if (opcion.Equals("Inmueble"))
            {
                query = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
                        i.Nombre, i.Apellido, i.Dni
                        FROM Contrato c
                        INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino
                        WHERE c.IdInmueble = @codigo
                        LIMIT 10";
            }
            if(opcion.Equals("Fecha"))

            if (opcion.Equals("Pagos"))
            {
                query = @"SELECT c.IdContrato, c.IdInquilino, c.IdInmueble, c.FechaInicio, c.FechaFin,
                        i.Nombre, i.Apellido, i.Dni
                        FROM Contrato c
                        INNER JOIN Inquilino i ON c.IdInquilino = i.IdInquilino
                        INNER JOIN Pago p ON c.IdContrato = p.IdContrato
                        WHERE c.IdContrato = @codigo";
            }

            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@codigo", codigo);

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
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                        }
                    };
                    contratos.Add(contrato);
                }
            }
        }
        return contratos;
    }

}