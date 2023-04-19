using Internal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
            ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje")){
                ViewBag.Mensaje = TempData["Mensaje"];
            }
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
                RepoInquilino.CreateInquilino(con, inquilino);
                TempData["Id"] = inquilino.IdInquilino;
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
                int res = RepoInquilino.UpdateInquilino(con, inquilino);
                TempData["Mensaje"] = "La entidad se actualizo correctamente ID:" + id;
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