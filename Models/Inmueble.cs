using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmueble
{
    [Display( Name = "Código" )]
    public int IdInmueble{ get; set; }
    public string Tipo{ get; set; }
    public string Coordenadas{ get; set; }
    public decimal Precio{ get; set; }
    public int Ambientes{ get; set; }
    public string Uso{ get; set; }
    [Display( Name = "Activo" )]
    public Boolean Activo{ get; set; }

    [Display( Name = "Propietario" ) ]
    public int IdPropietario{ get; set; }

    /* [ForeignKey( nameof( IdPropietario ) ) ]

    public Propietario Propietario{ get; set; } */
}