using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Collections.Generic;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    public InmuebleController()
    {
    }

    public IActionResult Index()
    {
        
        return View();
    }
}