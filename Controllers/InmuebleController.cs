using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private MySqlDatabase db { get; set; }
    public InmuebleController(MySqlDatabase mySqlDatabase)
    {
        this.db = mySqlDatabase(new MySqlDatabase("server=localhost; database=Inmobiliaria; uid=root; pwd=1234;"));
    }

    public IActionResult Index()
    {
        RepositorioInmueble inmuebles = new RepositorioInmueble();
        List<Inmueble> lista = inmuebles.GetInmuebles(db);
        return View(lista);
    }
}