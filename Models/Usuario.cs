using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum UsuarioRol
    {
        Administrador = 1,
        Empleado = 2,
    }
public class Usuario
{
    [Key]
    [Display(Name = "Código")]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Apellido { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Clave { get; set; }
    
    public string Avatar { get; set; }   
    [NotMapped]//Para EF
    public IFormFile AvatarFile { get; set; }

    public int Rol { get; set; }
    
}