using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contrato
{
    []
    public int IdContrato { get; set; }

    [Display( Name = "Inquilino" ) ]
    public int IdInmueble { get; set; }
    []

    [Display( Name = "Propietario" ) ]
    public int IdPropietario{ get; set; }

    [ForeignKey( nameof( IdPropietario ) ) ]

    public Propietario Propietario{ get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

}