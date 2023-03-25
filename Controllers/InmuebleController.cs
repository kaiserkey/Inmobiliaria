using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private cmd { get; set; }

    private MySqlDatabase MySqlDatabase { get; set; }
    public InmuebleController(MySqlDatabase mySqlDatabase)
    {
        this.MySqlDatabase = mySqlDatabase;
    }
}