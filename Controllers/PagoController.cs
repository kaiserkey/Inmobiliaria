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
    public class PagoController : Controller
    {
        private MySqlDatabase con { get; set; }
        private readonly RepositorioPago RepoPago;
        private readonly RepositorioContrato RepoContrato;
        public PagoController()
        {
            con = new MySqlDatabase();
            RepoPago = new RepositorioPago();
            RepoContrato = new RepositorioContrato();
        }
        // GET: Pago
        public ActionResult Index()
        {
            var listaPagos = RepoPago.GetPagos(con);
            ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"];
            }
            return View(listaPagos);
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
            try
            {
                var res = RepoPago.CreatePago(con, pago);
                TempData["Id"] = pago.IdPago;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /* buscar contratos por jquery */
        public IActionResult BuscarContratos(string busqueda, string opcion)
        {
            var contrato = new List<Contrato>();
            
            contrato = RepoContrato.BuscarContrato(con, busqueda, opcion);
            
            var resultados = contrato.Select(c => new
            {
                idContrato = c.IdContrato,
                idInmueble = c.IdInmueble,
                idInquilino = c.IdInquilino,
                nombre = c.Inquilino.Nombre,
                apellido = c.Inquilino.Apellido,
                dni = c.Inquilino.Dni,
                fechaFin = c.FechaFin,
                fechaInicio = c.FechaInicio,
            });

            return Json(resultados);
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pago/Edit/5
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

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pago/Delete/5
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