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
    [Display( Name = "Ambientes" )]
    public int Ambientes{ get; set; }
    [Display( Name = "Uso" )]
    public string Uso{ get; set; }
    [Display( Name = "Activo" )]
    public Boolean Activo{ get; set; }

    [Display( Name = "Propietario" ) ]
    public int IdPropietario{ get; set; }

    /* [ForeignKey( nameof( IdPropietario ) ) ]

    public Propietario Propietario{ get; set; } */
}