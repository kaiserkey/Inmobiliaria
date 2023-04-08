using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmueble
{
    [Display( Name = "Código" )]
    public int IdInmueble{ get; set; }
    [Display( Name = "Tipo" )]
    public string Tipo{ get; set; }
    [Display( Name = "Coordenadas" )]
    public string Coordenadas{ get; set; }
    [Display( Name = "Precio" )]
    public decimal Precio{ get; set; }
    []
    public int Ambientes{ get; set; }
    public string Uso{ get; set; }
    public Boolean Activo{ get; set; }

    [Display( Name = "Propietario" ) ]
    public int IdPropietario{ get; set; }

    /* [ForeignKey( nameof( IdPropietario ) ) ]

    public Propietario Propietario{ get; set; } */
}