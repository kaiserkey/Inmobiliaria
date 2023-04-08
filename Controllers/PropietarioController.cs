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
            return View();
        }

        // GET: Propietario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Propietario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}