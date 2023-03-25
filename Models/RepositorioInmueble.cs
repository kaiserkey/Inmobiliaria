using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInmueble
{
    private MySqlDatabase MySqlDatabase { get; set; }
    public RepositorioInmueble(MySqlDatabase mySqlDatabase)
    {
        this.MySqlDatabase = mySqlDatabase;
    }

    public List<Inmueble> GetInmuebles(){
        
    }
}