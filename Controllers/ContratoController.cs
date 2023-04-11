using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers
{
    public class ContratoController : Controller
    {
        private MySqlDatabase con { get; set; }
        private readonly RepositorioInmueble RepoInmueble;
        private readonly RepositorioContrato RepoContrato;
        private readonly RepositorioInquilino RepoInquilino;
        public ContratoController() { 
            con = new MySqlDatabase(); 
            RepoInmueble = new RepositorioInmueble();
            RepoContrato = new RepositorioContrato();
            RepoInquilino = new RepositorioInquilino();
        }
        // GET: Contrato
        public ActionResult Index()
        {
            var listaContratos = RepoContrato.GetContratos(con);
            return View(listaContratos);
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(COntrato )
        {
            try
            {
                RepoContrato.CreateContrato(con, collection);
                TempData["Id"] = 

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //obtener Inquilinos por JQuery
        public IActionResult BuscarInquilinos(string busqueda)
        {
            var inquilinos = new List<Inquilino>();
            inquilinos = RepoInquilino.BuscarInquilino(con, busqueda);

            var resultados = inquilinos.Select(i => new
            {
                id = i.IdInquilino,
                text = i.Nombre + " " + i.Apellido
            });

            return Json(resultados);
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contrato/Edit/5
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

        // GET: Contrato/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contrato/Delete/5
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