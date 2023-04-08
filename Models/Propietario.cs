using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Propietario
{
    public int IdPropietario{ get; set; }
    public string Nombre{ get; set; }
    public string Apellido{ get; set; }
    public string Dni{ get; set; }
    public string Telefono{ get; set; }
    public string Email{ get; set; }
    public string Clave{ get; set; }
}