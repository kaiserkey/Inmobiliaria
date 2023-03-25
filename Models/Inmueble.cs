using System;
namespace Inmobiliaria.Models;

public class Inmueble
{
    private int idInmueble{ get; set; }
    private string coordenadas{ get; set; }
    private decimal precio{ get; set; }
    private int ambientes{ get; set; }
    private string uso{ get; set; }
    private Boolean activo{ get; set; }
    private int idPropietario{ get; set; }

}