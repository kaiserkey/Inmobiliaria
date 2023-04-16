using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Inmobiliaria.Controllers
{   
    [Authorize]
    public class UsuarioController : Controller
    {
        private MySqlDatabase con { get; set; }
        private readonly RepositorioUsuario RepoUsuario;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        
        public UsuarioController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            con = new MySqlDatabase();
            RepoUsuario = new RepositorioUsuario();
            this.configuration = configuration;
            this.environment = environment;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Usuario/Salir
        [Route("Salir", Name = "Logout")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // POST: Usuario/Login/
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login){
            try
            {
                if (ModelState.IsValid)
				{
					string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Clave,
						salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 1000,
						numBytesRequested: 256 / 8));

					var Usuario = RepoUsuario.ObtenerPorEmail(con, login.Usuario);
					if (Usuario == null || Usuario.Clave != hashed)
					{
                        ModelState.AddModelError("","Usuario o contraseña incorrecto");
                        return View();
                    }

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, Usuario.Email),
						new Claim("FullName", Usuario.Nombre + " " + Usuario.Apellido),
						new Claim(ClaimTypes.Role, Usuario.RolNombre),
					};

					var claimsIdentity = new ClaimsIdentity(
							claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
							CookieAuthenticationDefaults.AuthenticationScheme,
							new ClaimsPrincipal(claimsIdentity));

					
					return Redirect("/Home");
				}

				return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
        }

        // GET: Usuario
        [Authorize(Policy ="Administrador")]
        public ActionResult Index()
        {
            var usuarios = RepoUsuario.GetUsuarios(con);
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        [Authorize(Policy ="Administrador")]
        public ActionResult Details(int id)
        {
            Usuario usuario = RepoUsuario.GetUsuario(con, id);
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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