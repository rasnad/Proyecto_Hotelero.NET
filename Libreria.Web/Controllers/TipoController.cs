using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Libreria.Web.Controllers
{
    public class TipoController : Controller
    {
        public CabanasContext CabanasContext = new CabanasContext();

        IRepositorioTipo _repoTipo = CrearRepositorio();
        IRepositorioParametro _repoParametro = CrearRepositorioParametro();

        private static IRepositorioTipo CrearRepositorio()
        {
            return new RepositorioTipo();
        }
        private static IRepositorioParametro CrearRepositorioParametro()
        {
            return new RepositorioParametro();

        }

        private readonly ISession _session;

        public TipoController(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }


        // GET: TipoController
        public ActionResult Index()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View(CabanasContext.Tipos.ToList());
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        // GET: TipoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoController/Create
        public ActionResult Create()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
           
        }

        // POST: TipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tipo nuevoTipo)
        {
            try
            { 
                if (nuevoTipo == null)
                {
                    return BadRequest("El tipo es nulo, no podemos seguir adelante");
                }

                int topeMinTipo = _repoParametro.GetValor("TopeMinDescTipo");
                int topeMaxTipo = _repoParametro.GetValor("TopeMaxDescTipo");
                    
                _repoTipo.Agregar(nuevoTipo, topeMinTipo, topeMaxTipo); 
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(nuevoTipo);
            }
        }

        // GET: TipoController/Edit/5
        public ActionResult Edit(int id)
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDescripcionCosto(int id) {
            Tipo tipo = _repoTipo.FindById(id);
            
            if (tipo == null)
            {
                return NotFound();
            }
         
            return View(tipo);
        }

        
        [HttpPost]
        public ActionResult ActualizarDescripcionCosto(int id, string descripcion, int costo)
        {
            Tipo tipo = _repoTipo.FindById(id);

            if (tipo == null)
            {
                return NotFound();
            }

            tipo.Descripcion = descripcion;
            tipo.CostoPorHuesped = costo;

            try
            {
                int topeMinTipo = _repoParametro.GetValor("TopeMinDescTipo");
                int topeMaxTipo = _repoParametro.GetValor("TopeMaxDescTipo");
                // falta validar los campos     tipo.Validar();
                _repoTipo.UpdateTipo(tipo, topeMinTipo, topeMaxTipo);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Ocurrio un error {ex.Message}";
                return View("EditarDescripcionCosto", tipo);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_session.Keys.Any()) // Hay variables de Sesion ?
                {
                    var Tipo = CabanasContext.Tipos.First(t => t.Id == id);
                    _repoTipo.Remove(Tipo);
                  
                    return RedirectToAction(nameof(Index));
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = ex.Message;
                return RedirectToAction(nameof(BuscadorNombreTipo));
            }
           

        }

        // GET: 
        public ActionResult BuscadorNombreTipo()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscadorNombreTipo(string nombre)
        {

            try
            {
                if (!String.IsNullOrEmpty(nombre))
                {
                    nombre = nombre.Trim();
                    nombre = nombre.ToUpper();
                    Tipo tipoEncontrado = CabanasContext.Tipos.First(t => t.Nombre.ToUpper() == nombre);
                       
                    ViewBag.TipoEncontrado = tipoEncontrado;
                    return View();
                }
                ViewBag.Mensaje = "Intentando con Campo Vacio";
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "No hubo coincidencia con algun Tipo";
                return View();
            }
        }

    }
}
