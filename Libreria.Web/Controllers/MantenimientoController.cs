using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Web.Controllers
{
    public class MantenimientoController : Controller
    {
        public CabanasContext CabanasContext = new CabanasContext();

        IRepositorioMantenimiento _repoMantenimiento = CrearRepositorio();
        IRepositorioCabana _repoCabana = CrearRepositorioCabaña();
        IRepositorioParametro _repoParametro = CrearRepositorioParametro();

        private readonly ISession _session;

        public MantenimientoController(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        private static IRepositorioMantenimiento CrearRepositorio()
        {
            return new RepositorioMantenimiento();
        }

        private static IRepositorioCabana CrearRepositorioCabaña()
        {
            return new RepositorioCabana();
        }
        private static IRepositorioParametro CrearRepositorioParametro()
        {
            return new RepositorioParametro();

        }

        // GET: MantenimientoController
        public ActionResult Index()
        {
            return View(CabanasContext.Mantenimientos.ToList());
        }

        // GET: MantenimientoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MantenimientoController/Create
        public ActionResult Create()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                IEnumerable<Cabaña> Cabañas = _repoCabana.FindAll();
                if (Cabañas != null)
                {
                    ViewBag.MisCabanas = Cabañas;
                }
                else
                {
                    ViewBag.MisCabanas = new List<Cabaña>();
                }
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
           
    
        }

        // POST: MantenimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mantenimiento nuevoMant)
        {
            IEnumerable<Cabaña> Cabañas = _repoCabana.FindAll();
            
            if (Cabañas!=null) {
                ViewBag.MisCabanas = Cabañas;
            }

            nuevoMant.Cabaña = CabanasContext.Cabañas.FirstOrDefault(u => u.NumeroHabitacion == nuevoMant.IdCabaña);
 
            try
            {
                if (nuevoMant == null) {
                    return BadRequest("El Mantenimiento es nulo, no podemos seguir adelante");
                }

                // Validar que no se hayan realizado más de 3 mantenimientos en el día
                if (_repoMantenimiento.CantidadMantenimientosDia(nuevoMant.IdCabaña, nuevoMant.FechaMantenimiento.Date) >= 3)
                {
                    throw new Exception("No se pueden realizar más de 3 mantenimientos por día en la cabaña seleccionada.");
                }

                int topeMinMant = _repoParametro.GetValor("TopeMinDescMantenimiento");
                int topeMaxMant = _repoParametro.GetValor("TopeMaxDescMantenimiento");

                _repoMantenimiento.Agregar(nuevoMant, topeMinMant, topeMaxMant);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nuevoMant);
            }
        }

       
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

        public ActionResult Delete(int id)
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


        public ActionResult MantenimientoEntreFechas()
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
        public ActionResult MantenimientoEntreFechas(DateTime fecha1, DateTime fecha2)
        {

            try
            {
                if (fecha1 == fecha2) { 
                    fecha2 = fecha2.AddDays(1);
                }
                var mantenimientoEncontrado = CabanasContext.Mantenimientos.Where(m => m.FechaMantenimiento >= fecha1 && m.FechaMantenimiento <= fecha2).ToList();
                
                ViewBag.Fechas = mantenimientoEncontrado;
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "Hubo un error en la búsqueda";
                return View();
            }
        }
    }
}
