using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago
{
    public int IdPago { get; set; }
    public int IdContrato { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Importe { get; set; }
    public string NumeroPago { get; set; }
    public string Estado { get; set; }
    public string Observaciones { get; set; }
    public Contrato Contrato { get; set; }
}