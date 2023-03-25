using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private MySqlDatabase con { get; set; }
    public InmuebleController()
    {
        this.con = new MySqlDatabase("server=localhost; database=Inmobiliaria; uid=root; pwd=1234;");
    }

    public IActionResult Index()
    {
        RepositorioInmueble inmuebles = new RepositorioInmueble();
        List<Inmueble> lista = inmuebles.GetInmuebles(con);
        return View(lista);
    }

    public IActionResult Create()
    {
        return View();
    }
    
}