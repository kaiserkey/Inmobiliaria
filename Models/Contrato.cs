using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contrato
{
    [Display( Name = "Contrato" )]
    public int IdContrato { get; set; }

    [Display( Name = "Inmueble" ) ]
    public int IdInmueble { get; set; }

    [ForeignKey( nameof( IdInmueble ) ) ]
    public Inmueble Inmueble { get; set; }

    [Display( Name = "Propietario" ) ]
    public int IdPropietario{ get; set; }

    [ForeignKey( nameof( IdPropietario ) ) ]
    public Propietario Propietario{ get; set; }

    [Display( Name = "Fecha de Inicio")]
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

}