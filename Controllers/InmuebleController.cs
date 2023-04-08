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
        private readonly RepositorioInmueble RepoInmueble;
        public InmuebleController() { 
            con = new MySqlDatabase(); 
            RepoInmueble = new RepositorioInmueble();
        }

        // GET: Inmueble
        public ActionResult Index()
        {
            var listaInmuebles = RepoInmueble.GetInmuebles(con);
            return View(listaInmuebles);
        }

        

        // GET: Inmueble/Details/5
        public ActionResult Details(int id)
        {
            var inmueble = RepoInmueble.GetInmueble(con, id);
            return View(inmueble);
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
            Inmueble inmueble = RepoInmueble.GetInmueble(con, id);
            
            return View(inmueble);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble UpdateInmueble)
        {
            try
            {
                int res = RepoInmueble.UpdateInmueble(con, UpdateInmueble);

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
            Inmueble inmueble = RepoInmueble.GetInmueble(con, id);

            return View(inmueble);
        }

        // POST: Inmueble/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                int res = RepoInmueble.DeleteInmueble(con, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}