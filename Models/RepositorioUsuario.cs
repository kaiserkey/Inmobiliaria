using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Inmobiliaria.Models;

public class RepositorioUsuario
{
    public RepositorioUsuario()
    {
    }

    public List<Usuario> GetUsuarios(MySqlDatabase mySqlDatabase)
    {
        var usuarios = new List<Usuario>();
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT IdUsuario, Nombre, Apellido, Avatar, Email, Rol, Clave FROM Usuario";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var usuario = new Usuario
                    {
                        IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Clave = reader.GetString(nameof(Usuario.Clave)),
                        Avatar = reader.GetString(nameof(Usuario.Avatar)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Rol = reader.GetString(nameof(Usuario.Rol))
                    };
                    usuarios.Add(usuario);
                }

            }
        }
        return usuarios;
    }

    public Usuario GetUsuario(MySqlDatabase mySqlDatabase, int id)
    {
        Usuario? usuario = null;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT IdUsuario, Nombre, Apellido, Clave, Avatar, Email, Rol FROM Usuario WHERE IdUsuario = @id";
            cmd.Parameters.AddWithValue("@id", id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Clave = reader.GetString(nameof(Usuario.Clave)),
                        Avatar = reader.GetString(nameof(Usuario.Avatar)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Rol = reader.GetString(nameof(Usuario.Rol))
                    };
                }
            }
        }
        return usuario;
    }

    public int CreateUsuario(MySqlDatabase mySqlDatabase, Usuario usuario)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"INSERT INTO Usuario (Nombre, Apellido, Clave, Avatar, Email, Rol) VALUES (@Nombre, @Apellido, @Clave, @Avatar, @Email, @Rol)";
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
            cmd.Parameters.AddWithValue("@Avatar", usuario.Avatar);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
            res = cmd.ExecuteNonQuery();
        }
        return res;
    }

    public int UpdateUsuario(MySqlDatabase mySqlDatabase, Usuario usuario)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"UPDATE Usuario SET Nombre=@Nombre, Apellido=@Apellido, Clave=@Clave, Avatar=@Avatar, Email=@Email, Rol=@Rol WHERE IdUsuario = @id";
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
            cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
            cmd.Parameters.AddWithValue("@Avatar", usuario.Avatar);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@I", usuario.IdUsuario);
            res = cmd.ExecuteNonQuery();
        }
        return res;
    }

    public int DeleteUsuario(MySqlDatabase mySqlDatabase, int id)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"DELETE FROM Usuario WHERE IdUsuario = @id";
            cmd.Parameters.AddWithValue("@id", id);
            res = cmd.ExecuteNonQuery();
        }
        return res;
    }

    
}
