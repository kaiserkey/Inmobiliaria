using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private MySqlDatabase db { get; set; }
    public InmuebleController([FromServices]MySqlDatabase mySqlDatabase)
    {
        this.db = mySqlDatabase;
    }

    public IActionResult Index()
    {
        RepositorioInmueble inmuebles = new RepositorioInmueble();
        List<Inmueble> lista = inmuebles.GetInmuebles(db);
        return View(lista);
    }
}