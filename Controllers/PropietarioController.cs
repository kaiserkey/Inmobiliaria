using Internal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        private MySqlDatabase con { get; set; }
        private readonly RepositorioPropietario RepoPropietario;
        public PropietarioController()
        {
            con = new MySqlDatabase();
            RepoPropietario = new RepositorioPropietario();
        }

        // GET: Propietario
        [Authorize]
        public ActionResult Index()
        {
            var listaPropietarios = RepoPropietario.GetPropietarios(con);
            ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje")){
                ViewBag.Mensaje = TempData["Mensaje"];
            }
            return View(listaPropietarios);
        }

        // GET: Propietario/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var propietario = RepoPropietario.GetPropietario(con, id);
            return View(propietario);
        }

        // GET: Propietario/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                var existEmail = RepoPropietario.GetPropietarioPorEmail(con, propietario.Email);
                if(existEmail != null){
                    ViewBag.Roles = Usuario.ObtenerRoles();
                    ViewBag.Error = "El email ya se encuentra registrado.";
                    return View();
                }
                RepoPropietario.CreatePropietario(con, propietario);
                TempData["Id"] = propietario.IdPropietario;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Propietario propietario = RepoPropietario.GetPropietario(con, id);
            return View(propietario);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Propietario UpdatePropietario)
        {
            try
            {
                
                var res = RepoPropietario.UpdatePropietario(con, UpdatePropietario);
                TempData["Mensaje"] = "La entidad se actualizo correctamente ID:" + id;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            Propietario propietario = RepoPropietario.GetPropietario(con, id);
            
            return View(propietario);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Propietario propietario)
        {
            try
            {
                var res = RepoPropietario.DeletePropietario(con, id);
                TempData["Mensaje"] = "La entidad se ha eliminado corectamente.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}