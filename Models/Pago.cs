using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago
{
    [Display (Name = "Código")]
    public int IdPago { get; set; }

    [Display (Name = "Fecha")]
    public DateTime Fecha { get; set; }

    [Display (Name = "Importe")]
    public decimal Importe { get; set; }

    [Display (Name = "Número de Pago")]
    public string NumeroPago { get; set; }

    [Display (Name = "Contrato")]
    public int IdContrato { get; set; }
    [ForeignKey("IdContrato")]
    public Contrato Contrato { get; set; }
}