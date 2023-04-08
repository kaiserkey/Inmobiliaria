using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPropietario
{
    public RepositorioPropietario()
    {
    }

    public List<Propietario> GetInmuebles(MySqlDatabase mySqlDatabase)
    {
        var propietarios = new List<Propietario>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario FROM Propietario";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var Propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Tipo = reader.GetString(nameof(Propietario.Tipo)),
                    Coordenadas = reader.GetString(nameof(Propietario.Coordenadas)),
                    Precio = reader.GetDecimal(nameof(Propietario.Precio)),
                    Ambientes = reader.GetInt32(nameof(Propietario.Ambientes)),
                    Uso = reader.GetString(nameof(Propietario.Uso)),
                    Activo = reader.GetBoolean(nameof(Propietario.Activo)),
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario))
                };
                propietarios.Add(Propietario);
            }

        }
        mySqlDatabase.Dispose();
        return propietarios;
    }

    public Propietario GetInmueble(MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario 
                            FROM Propietario WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var Propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Tipo = reader.GetString(nameof(Propietario.Tipo)),
                    Coordenadas = reader.GetString(nameof(Propietario.Coordenadas)),
                    Precio = reader.GetDecimal(nameof(Propietario.Precio)),
                    Ambientes = reader.GetInt32(nameof(Propietario.Ambientes)),
                    Uso = reader.GetString(nameof(Propietario.Uso)),
                    Activo = reader.GetBoolean(nameof(Propietario.Activo)),
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario))
                };
                mySqlDatabase.Dispose();
                return Propietario;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int CreateInmueble(MySqlDatabase mySqlDatabase, Propietario Propietario)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"INSERT INTO Propietario (Tipo, Coordenadas, Precio, Ambientes, Uso, Activo, IdPropietario) 
                            VALUES (@Tipo, @Coordenadas, @Precio, @Ambientes, @Uso, @Activo, @IdPropietario);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Tipo", Propietario.Tipo);
        cmd.Parameters.AddWithValue("@Coordenadas", Propietario.Coordenadas);
        cmd.Parameters.AddWithValue("@Precio", Propietario.Precio);
        cmd.Parameters.AddWithValue("@Ambientes", Propietario.Ambientes);
        cmd.Parameters.AddWithValue("@Uso", Propietario.Uso);
        cmd.Parameters.AddWithValue("@Activo", Propietario.Activo);
        cmd.Parameters.AddWithValue("@IdPropietario", Propietario.IdPropietario);

        var recs = Convert.ToInt32(cmd.ExecuteScalar());

        mySqlDatabase.Dispose();
        
        return recs;
    }

    public int UpdateInmueble(MySqlDatabase mySqlDatabase, Propietario Propietario)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Propietario SET Tipo = @Tipo, Coordenadas = @Coordenadas, Precio = @Precio, Ambientes = @Ambientes, Uso = @Uso, Activo = @Activo, IdPropietario = @IdPropietario
                            WHERE IdPropietario = @IdPropietario;";

        cmd.Parameters.AddWithValue("@IdPropietario", Propietario.IdPropietario);
        cmd.Parameters.AddWithValue("@Tipo", Propietario.Tipo);
        cmd.Parameters.AddWithValue("@Coordenadas", Propietario.Coordenadas);
        cmd.Parameters.AddWithValue("@Precio", Propietario.Precio);
        cmd.Parameters.AddWithValue("@Ambientes", Propietario.Ambientes);
        cmd.Parameters.AddWithValue("@Uso", Propietario.Uso);
        cmd.Parameters.AddWithValue("@Activo", Propietario.Activo);
        cmd.Parameters.AddWithValue("@IdPropietario", Propietario.IdPropietario);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int DeleteInmueble( MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM Propietario WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }
}