using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
    
    public string Clave { get; set; }
    
    public string Avatar { get; set; }   

    public int Rol { get; set; }
}