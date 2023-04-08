using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Propietario
{
    [Display (Name = "Código")]
    public int IdPropietario{ get; set; }
    [Display (Name = "Nombre")]
    public string Nombre{ get; set; }
    [Display (Name = "Apellido")]
    public string Apellido{ get; set; }
    []
    public string Direccion{ get; set; }
    public string Telefono{ get; set; }
    public string Dni{ get; set; }
    public string Email{ get; set; }
}