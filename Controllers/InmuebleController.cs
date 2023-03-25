using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Collections.Generic;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private MySqlDatabase MySqlDatabase { get; set; }
    public InmuebleController(MySqlDatabase mySqlDatabase)
    {
        this.MySqlDatabase = mySqlDatabase;
    }
}