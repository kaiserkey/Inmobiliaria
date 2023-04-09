using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System.Globalization;

namespace Inmobiliaria.Controllers
{
    public class InquilinoController : Controller
    {

        private MySqlDatabase con { get; set; }
        private readonly RepositorioInquilino RepoInquilino;

        public InquilinoController()
        {
            con = new MySqlDatabase();
            RepoInquilino = new RepositorioInquilino();
        }

        // GET: Inquilino
        public ActionResult Index()
        {
            var listaInquilinos = RepoInquilino.GetInquilinos(con);
            return View(listaInquilinos);
        }

        // GET: Inquilino/Details/5
        public ActionResult Details(int id)
        {
            var inquilino = RepoInquilino.GetInquilino(con, id);
            return View(inquilino);
        }

        // GET: Inquilino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {   
                string fechaNacimientoStr = inquilino.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime fechaNacimiento = DateTime.ParseExact(fechaNacimientoStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string fechaNacimientoFormateada = fechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss");
                
                RepoInquilino.CreateInquilino(con, inquilino);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilino/Edit/5
        public ActionResult Edit(int id)
        {
            var inquilino = RepoInquilino.GetInquilino(con, id);
            return View(inquilino);
        }

        // POST: Inquilino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino inquilino)
        {
            try
            {
                RepoInquilino.UpdateInquilino(con, inquilino);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilino/Delete/5
        public ActionResult Delete(int id)
        {
            var inquilino = RepoInquilino.GetInquilino(con, id);
            return View(inquilino);
        }

        // POST: Inquilino/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino inquilino)
        {
            try
            {
                RepoInquilino.DeleteInquilino(con, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}