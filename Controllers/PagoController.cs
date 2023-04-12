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

        public IActionResult BuscarContrato(string busqueda, string buscarPor)
        {
            var contrato = new List<Inquilino>();
            contrato = RepoContrato.BuscarContrato(con, busqueda, buscarPor);

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