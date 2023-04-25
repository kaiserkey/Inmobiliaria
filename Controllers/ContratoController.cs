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

namespace Inmobiliaria.Controllers
{
    [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult Details(int id)
        {
            Contrato contrato = RepoContrato.GetContrato(con, id);

            return View(contrato);
        }

        // GET: Contrato/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

        /* buscar contratos por fecha jquery */
        public IActionResult BuscarContratosPorFecha(string fechaDesde, string fechaHasta)
        {
            var contrato = new List<Contrato>();
            
            contrato = RepoContrato.BuscarContratosPorFecha(con, fechaDesde, fechaHasta);
            
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

        /* buscar contratos por fecha jquery */
        public IActionResult BuscarContratosPorCodigo(int codigo)
        {
            var contrato = new List<Contrato>();
            
            contrato = RepoContrato.BuscarContratosPorCodigo(con, codigo);
            
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

        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            Contrato contrato = RepoContrato.GetContrato(con, id);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Contrato updateContrato)
        {
            try
            {
                int res = RepoContrato.UpdateContrato(con, updateContrato);
                if(res > 0){
                    TempData["Mensaje"] = "La entidad se actualizo correctamente ID:" + id;
                }else{
                    TempData["Mensaje"] = "Existe superposicion de fechas con otra " + id;
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            Contrato contrato = RepoContrato.GetContrato(con, id);
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
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