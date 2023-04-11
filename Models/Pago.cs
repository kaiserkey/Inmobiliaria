using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago
{
    public int IdPago { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Importe { get; set; }
    public string NumeroPago { get; set; }
    
    [ForeignKey("IdContrato")]
    public Contrato Contrato { get; set; }
}