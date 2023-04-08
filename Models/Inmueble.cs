using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;

public class Inmueble
{
    public int IdInmueble{ get; set; }
    public string Tipo{ get; set; }
    public string Coordenadas{ get; set; }
    public decimal Precio{ get; set; }
    public int Ambientes{ get; set; }
    public string Uso{ get; set; }
    public Boolean Activo{ get; set; }

    [Display( Name = "Propietario" ) ]
    public int IdPropietario{ get; set; }

    [ForeignKey( nameof( Propietario ) ) ]


}