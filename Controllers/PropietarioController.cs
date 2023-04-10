using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers
{
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
        public ActionResult Index()
        {
            var listaPropietarios = RepoPropietario.GetPropietarios(con);
            return View(listaPropietarios);
        }

        // GET: Propietario/Details/5
        public ActionResult Details(int id)
        {
            var propietario = RepoPropietario.GetPropietario(con, id);
            return View(propietario);
        }

        // GET: Propietario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                var id = RepoPropietario.CreatePropietario(con, propietario);
                TempData["Id"] = 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Edit/5
        public ActionResult Edit(int id)
        {
            Propietario propietario = RepoPropietario.GetPropietario(con, id);
            return View(propietario);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario UpdatePropietario)
        {
            try
            {
                
                var res = RepoPropietario.UpdatePropietario(con, UpdatePropietario);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Delete/5
        public ActionResult Delete(int id)
        {
            Propietario propietario = RepoPropietario.GetPropietario(con, id);
            return View(propietario);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario propietario)
        {
            try
            {
                var res = RepoPropietario.DeletePropietario(con, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}