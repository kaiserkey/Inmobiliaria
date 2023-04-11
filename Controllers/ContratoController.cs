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
    public class ContratoController : Controller
    {
        private MySqlDatabase con { get; set; }
        private readonly RepositorioInmueble RepoInmueble;
        private readonly RepositorioContrato RepoContrato;
        private readonly RepositorioInquilino RepoInquilino;
        public ContratoController()
        {
            con = new MySqlDatabase();
            RepoInmueble = new RepositorioInmueble();
            RepoContrato = new RepositorioContrato();
            RepoInquilino = new RepositorioInquilino();
        }
        // GET: Contrato
        public ActionResult Index()
        {
            var listaContratos = RepoContrato.GetContratos(con);
            ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"];
            }
            return View(listaContratos);
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int id)
        {
            Contrato contrato = RepoContrato.GetContrato(con, id);

            return View(contrato);
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            try
            {
                RepoContrato.CreateContrato(con, contrato);
                TempData["Id"] = contrato.IdContrato;
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
                idInquilino = i.IdInquilino,
                nombre = i.Nombre,
                apellido = i.Apellido,
                telefono = i.Telefono,
                email = i.Email,
            });

            return Json(resultados);
        }

        //obtener Inmuebles por JQuery
        public IActionResult BuscarInmuebles(string busqueda, string opcion)
        {
            var inmuebles = new List<Inmueble>();
            
            inmuebles = RepoInmueble.BuscarInmueble(con, busqueda, opcion);

            var resultados = inmuebles.Select(i => new
            {
                idInmueble = i.IdInmueble,
                tipo = i.Tipo,
                coordenadas = i.Coordenadas,
                precio = i.Precio,
                ambientes = i.Ambientes,
                uso = i.Uso,
            });

            return Json(resultados);
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            Contrato contrato = RepoContrato.GetContrato(con, id);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato updateContrato)
        {
            try
            {
                int res = RepoContrato.UpdateContrato(con, updateContrato);
                TempData["Mensaje"] = "La entidad se actualizo correctamente ID:" + id;
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
            Contrato contrato = RepoContrato.GetContrato(con, id);
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato deleteContrato)
        {
            try
            {
                int res = RepoContrato.DeleteContrato(con, id);
                TempData["Mensaje"] = "La entidad se ha elimino correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}