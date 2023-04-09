using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Propietario
{
    public Propietario(){

    }
    
    [Display (Name = "Código")]
    public int IdPropietario{ get; set; }
    [Display (Name = "Nombre")]
    public string Nombre{ get; set; }
    [Display (Name = "Apellido")]
    public string Apellido{ get; set; }
    [Display (Name = "Dirección")]
    public string Direccion{ get; set; }
    [Display (Name = "Teléfono")]
    public string Telefono{ get; set; }
    [Display (Name = "DNI")]
    public string Dni{ get; set; }
    [Display (Name = "Email")]
    public string Email{ get; set; }
}