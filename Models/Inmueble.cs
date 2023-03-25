using System;
namespace Inmobiliaria.Models;

public class Inmueble
{
    public int idInmueble{ get; set; }
    public string tipo{ get; set; }
    public string coordenadas{ get; set; }
    public decimal precio{ get; set; }
    public int ambientes{ get; set; }
    public string uso{ get; set; }
    public Boolean activo{ get; set; }
    public int idPropietario{ get; set; }


}