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
            cmd.CommandText = @"SELECT IdUsuario, Nombre, Apellido, Direccion, Telefono, Dni, Email, Rol FROM Usuario";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var usuario = new Usuario
                    {
                        IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Direccion = reader.GetString(nameof(Usuario.Direccion)),
                        Telefono = reader.GetString(nameof(Usuario.Telefono)),
                        Dni = reader.GetString(nameof(Usuario.Dni)),
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
            cmd.CommandText = @"SELECT IdUsuario, Nombre, Apellido, Direccion, Telefono, Dni, Email, Rol FROM Usuario WHERE IdUsuario = @id";
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
                        Direccion = reader.GetString(nameof(Usuario.Direccion)),
                        Telefono = reader.GetString(nameof(Usuario.Telefono)),
                        Dni = reader.GetString(nameof(Usuario.Dni)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Rol = reader.GetString(nameof(Usuario.Rol))
                    };
                }
            }
        }
        return usuario;
    }
}
