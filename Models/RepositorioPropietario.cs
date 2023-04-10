using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPropietario
{
    public RepositorioPropietario()
    {
    }

    public List<Propietario> GetPropietarios(MySqlDatabase mySqlDatabase)
    {
        var propietarios = new List<Propietario>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email FROM Propietario";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                    Direccion = reader.GetString(nameof(Propietario.Direccion)),
                    Telefono = reader.GetString(nameof(Propietario.Telefono)),
                    Dni = reader.GetString(nameof(Propietario.Dni)),
                    Email = reader.GetString(nameof(Propietario.Email))
                };
                propietarios.Add(propietario);
            }

        }
        mySqlDatabase.Dispose();
        return propietarios;
    }

    public Propietario GetPropietario(MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email 
                            FROM Propietario WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                    Direccion = reader.GetString(nameof(Propietario.Direccion)),
                    Telefono = reader.GetString(nameof(Propietario.Telefono)),
                    Dni = reader.GetString(nameof(Propietario.Dni)),
                    Email = reader.GetString(nameof(Propietario.Email))
                };
                mySqlDatabase.Dispose();
                return propietario;
            }
        }
        mySqlDatabase.Dispose();
        return null;
    }

    public int CreatePropietario(MySqlDatabase mySqlDatabase, Propietario createPropietario)
{
    int res = -1;
    using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
    {
        cmd.CommandText = @"INSERT INTO Propietario (Nombre, Apellido, Direccion, Telefono, Dni, Email) 
                            VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Dni, @Email);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Nombre", createPropietario.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", createPropietario.Apellido);
        cmd.Parameters.AddWithValue("@Direccion", createPropietario.Direccion);
        cmd.Parameters.AddWithValue("@Telefono", createPropietario.Telefono);
        cmd.Parameters.AddWithValue("@Dni", createPropietario.Dni);
        cmd.Parameters.AddWithValue("@Email", createPropietario.Email);
        
        res = Convert.ToInt32(cmd.ExecuteScalar());
        createPropietario.IdPropietario = res;
    }
    
    return res;
}
En este caso, estamos envolviendo el objeto MySqlCommand dentro de un bloque using, lo que garantiza que el objeto será eliminado correctamente y sus recursos liberados una vez que salgamos del bloque. También eliminamos la llamada mySqlDatabase.Dispose(), ya que el objeto MySqlConnection también se liberará automáticamente al finalizar el bloque using.

Además, he cambiado el nombre del parámetro CreatePropietario por createPropietario, para seguir las convenciones de nomenclatura de C#. Es una práctica común nombrar los parámetros con minúsculas en el inicio de cada palabra, en lugar de usar mayúsculas.




Daniel González
var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand; que funcion cumple la clausula as en este codigo
La cláusula as en este código es una conversión explícita de tipo, que permite convertir el objeto retornado por el método CreateCommand() en un objeto del tipo MySqlCommand.

La razón por la cual se utiliza la cláusula as en lugar de una conversión normal, como por ejemplo (MySqlCommand)mySqlDatabase.Connection.CreateCommand(), es para evitar una excepción de tipo InvalidCastException en caso de que el objeto retornado no sea del tipo MySqlCommand.

Si la conversión normal es utilizada y el objeto retornado no es del tipo correcto, una excepción será lanzada y detendrá la ejecución del programa. En cambio, si se utiliza la cláusula as y el objeto no es del tipo correcto, la variable cmd será nula y se puede manejar esta situación adecuadamente en el código.







    public int UpdatePropietario(MySqlDatabase mySqlDatabase, Propietario Propietario)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        
        cmd.CommandText = @"UPDATE Propietario SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion, Telefono = @Telefono, Dni = @Dni, Email = @Email
                            WHERE IdPropietario = @IdPropietario;";

        cmd.Parameters.AddWithValue("@IdPropietario", Propietario.IdPropietario);
        cmd.Parameters.AddWithValue("@Nombre", Propietario.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", Propietario.Apellido);
        cmd.Parameters.AddWithValue("@Direccion", Propietario.Direccion);
        cmd.Parameters.AddWithValue("@Telefono", Propietario.Telefono);
        cmd.Parameters.AddWithValue("@Dni", Propietario.Dni);
        cmd.Parameters.AddWithValue("@Email", Propietario.Email);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public int DeletePropietario( MySqlDatabase mySqlDatabase, int id)
    {
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM Propietario WHERE IdPropietario = @IdPropietario";
        cmd.Parameters.AddWithValue("@IdPropietario", id);

        var res = Convert.ToInt32(cmd.ExecuteNonQuery());

        mySqlDatabase.Dispose();
        
        return res;
    }

    public List<Propietario> BuscarPropietario(MySqlDatabase mySqlDatabase, string nombreCompleto){
        var propietarios = new List<Propietario>();
        var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT IdPropietario, Nombre, Apellido, Direccion, Telefono, Dni, Email 
                            FROM Propietario
                            WHERE CONCAT(Nombre, ' ', Apellido) LIKE @nombreCompleto";
        cmd.Parameters.AddWithValue("@nombreCompleto", "%" + nombreCompleto + "%");
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var propietario = new Propietario
                {
                    IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                    Direccion = reader.GetString(nameof(Propietario.Direccion)),
                    Telefono = reader.GetString(nameof(Propietario.Telefono)),
                    Dni = reader.GetString(nameof(Propietario.Dni)),
                    Email = reader.GetString(nameof(Propietario.Email))
                };
                propietarios.Add(propietario);
            }

        }
        mySqlDatabase.Dispose();
        return propietarios;
    }
}