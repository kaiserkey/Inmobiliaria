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
        private readonly RepositorioPropietario RepoPropietario;
        public InmuebleController() { 
            con = new MySqlDatabase(); 
            RepoInmueble = new RepositorioInmueble();
            RepoPropietario = new RepositorioPropietario();
        }

        // GET: Inmueble
        public ActionResult Index()
        {
            var listaInmuebles = RepoInmueble.GetInmuebles(con);
            ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje")){
                ViewBag.Mensaje = TempData["Mensaje"];
            }
            return View(listaInmuebles);
        }

        public ActionResult Create()
        {
            return View();
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
                TempData["Id"] = inmueble.IdInmueble;
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

        //obtener propietarios por JQuery
        public IActionResult BuscarPropietarios(string busqueda)
        {
            var propietarios = new List<Propietario>();
            propietarios = RepoPropietario.BuscarPropietario(con, busqueda);

            var resultados = propietarios.Select(p => new
            {
                idP = p.IdPropietario,
                nombre = p.Nombre,
                apellido = p.Apellido,
                telefono = p.Telefono,
                email = p.Email,
            });

            return Json(resultados);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble UpdateInmueble)
        {
            try
            {
                int res = RepoInmueble.UpdateInmueble(con, UpdateInmueble);
                TempData["Mensaje"] = "La entidad se actualizo correctamente ID:" + id;
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
        public ActionResult Delete(int id, Inmueble inmueble)
        {
            try
            {
                int res = RepoInmueble.DeleteInmueble(con, id);
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