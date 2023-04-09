using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inquilino
{
    [Display (Name = "Código")]
    public int IdInquilino{ get; set; }
    [Display (Name = "Nombre")]
    public string Nombre{ get; set; }
    [Display (Name = "Apellido")]
    public string Apellido{ get; set; }
    [Display (Name = "Email")]
    public string Email{ get; set; }
    [Display (Name = "DNI")]
    public string Dni{ get; set; }
    [Display (Name = "Teléfono")]
    public string Telefono{ get; set; }
    [Display ()]
    public DateTime FechaNacimiento{ get; set; }
}