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
        public async Task<IActionResult> Login(Login login)
        {
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
                        ModelState.AddModelError("", "Usuario o contraseña incorrecto");
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
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Usuario
        [Authorize(Policy = "Administrador")]
        public ActionResult Index()
        {
            var usuarios = RepoUsuario.GetUsuarios(con);
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Details(int id)
        {
            Usuario usuario = RepoUsuario.GetUsuario(con, id);
            return View(usuario);
        }

        // GET: Usuario/Create
        [Authorize(Policy = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: usuario.Clave,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    ));
                usuario.Clave = hashed;
                //usuario.Rol = User.IsInRole("Administrador") ? usuario.Rol : (int)enRoles.Empleado ;
                var res = RepoUsuario.CreateUsuario(con, usuario);
                if (usuario.AvatarFile != null && usuario.IdUsuario > 0)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = "avatar_" + usuario.IdUsuario + Path.GetExtension(usuario.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    usuario.Avatar = Path.Combine("/Uploads", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        usuario.AvatarFile.CopyTo(stream);
                    }
                    RepoUsuario.UpdateUsuario(con, usuario);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //ViewBag.Roles = Usuario.ObtenerRoles();
                return View();
            }
        }

        [Authorize]
        public ActionResult Perfil()
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            var usuario = RepoUsuario.ObtenerPorEmail(con, User.Identity.Name);
            return View(usuario);
        }

        [Authorize]
        public ActionResult EditarPerfil()
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            var usuario = RepoUsuario.ObtenerPorEmail(con, User.Identity.Name);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Edit(int id)
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            var usuario = RepoUsuario.GetUsuario(con, id);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Edit(int id, Usuario usuarioEdit)
        {
            var usuario = RepoUsuario.GetUsuario(con, id);
            try
            {
                if (usuarioEdit.Clave == null || usuarioEdit.Clave == "")
                {
                    usuarioEdit.Clave = usuario.Clave;
                }
                else
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: usuarioEdit.Clave,
                            salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 1000,
                            numBytesRequested: 256 / 8
                        ));
                    usuarioEdit.Clave = hashed;
                }

                var res = RepoUsuario.UpdateUsuario(con, usuarioEdit);
                ViewBag.Roles = Usuario.ObtenerRoles();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Usuario/EditarPerfil/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditarPerfil(int id, Usuario usuarioEdit)
        {
            Console.WriteLine("Editar perfil");
            var usuario = RepoUsuario.GetUsuario(con, id);
            try
            {
                var usuarioActual = RepoUsuario.ObtenerPorEmail(con, User.Identity.Name);
                if (usuarioActual.IdUsuario != id)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                if (usuarioEdit.Clave == null || usuarioEdit.Clave == "")
                {
                    usuarioEdit.Clave = usuario.Clave;
                }
                else
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: usuarioEdit.Clave,
                            salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 1000,
                            numBytesRequested: 256 / 8
                        ));
                    usuarioEdit.Clave = hashed;
                }

                if (usuarioEdit.AvatarFile != null)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = "avatar_" + usuarioEdit.IdUsuario + Path.GetExtension(usuarioEdit.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    usuarioEdit.Avatar = Path.Combine("/Uploads", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        usuarioEdit.AvatarFile.CopyTo(stream);
                    }
                }
                else
                {
                    usuarioEdit.Avatar = usuario.Avatar;
                }

                usuarioEdit.IdUsuario = id;
                var res = RepoUsuario.UpdateUsuario(con, usuarioEdit);
                ViewBag.Roles = Usuario.ObtenerRoles();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            Usuario usuario = RepoUsuario.GetUsuario(con, id);
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Usuario usuario)
        {
            try
            {
                var usuario_avatar = RepoUsuario.GetUsuario(con, id);
                var res = RepoUsuario.DeleteUsuario(con, id);
                if (res > 0)
                {
                    // Delete avatar image
                    if (usuario_avatar.Avatar != null || usuario_avatar.Avatar != "")
                    {
                        string wwwPath = environment.WebRootPath;

                        if (System.IO.File.Exists(wwwPath + usuario_avatar.Avatar))
                        {
                            System.IO.File.Delete(wwwPath + usuario_avatar.Avatar);
                            Console.WriteLine("Archivo eliminado exitosamente: ");
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}