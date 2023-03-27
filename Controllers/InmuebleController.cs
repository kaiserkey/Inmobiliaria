using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers
{
    public class InmuebleController : Controller
    {
        private MySqlDatabase con { get; set; }
        public InmuebleController() { 
            con = new MySqlDatabase(); }

        private readonly RepositorioInmueble RepoInmueble;

        // GET: Inmueble
        public ActionResult Index()
        {
            var listaInmuebles = RepoInmueble.GetInmuebles(con);
            return View(listaInmuebles);
        }

        

        // GET: Inmueble/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inmueble/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inmueble/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                RepoInmueble.CreateInmueble(con, inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inmueble/Edit/5
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

        // GET: Inmueble/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inmueble/Delete/5
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