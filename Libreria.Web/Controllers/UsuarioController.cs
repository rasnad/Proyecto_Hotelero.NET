using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Libreria.Web.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly ISession _session;

        public UsuarioController(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }



        public CabanasContext CabanasContext = new CabanasContext();

      

        // GET: Usuario
        IRepositorioUsuario _repoUsuario = CrearRepositorio();

        private static IRepositorioUsuario CrearRepositorio()
        {
            return new RepositorioUsuario();
        }

        public ActionResult Index()
        {
            return View(CabanasContext.Usuarios.ToList());
        }
        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: Usuario/Login
        public IActionResult Login()
        {
            if (!_session.Keys.Any()) { // Hay variables de Sesion ?
                ViewBag.Mensaje = "";
                return View();
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            try
            {
                bool login = _repoUsuario.Login(email, pass);
                if (login)
                {
                    _session.SetString("Email", email); // Guardar email en la sesión
                    _session.SetString("Logueado", "true"); // Guardar "true" en la sesión para indicar que el usuario está logueado
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception ex) {
                ViewBag.Error = $"Error Login - {ex.Message}";
                return View();
            }
            
        }

        // Usuario/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }


        // GET: Usuario/Create
        public ActionResult Create()
        {         
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario nuevoUsuario)
        {
            try
            {
                if(nuevoUsuario == null)
                {
                    return BadRequest("El usuario es nulo, no podemos seguir adelante");
                }
                _repoUsuario.Add(nuevoUsuario); 
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nuevoUsuario);
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
           ;
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
