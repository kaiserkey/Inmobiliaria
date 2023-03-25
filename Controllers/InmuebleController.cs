using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private MySqlDatabase MySqlDatabase { get; set; }
    public InmuebleController(MySqlDatabase mySqlDatabase)
    {
        this.MySqlDatabase = mySqlDatabase;
    }

    public IActionResult Index()
    {
        RepositorioInmueble inmuebles = new RepositorioInmueble();
        List<Inmueble> lista = inmuebles.GetInmuebles(MySqlDatabase);
        return View(lista);
    }
}