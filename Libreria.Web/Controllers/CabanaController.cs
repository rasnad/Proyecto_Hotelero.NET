using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;



using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Web.Controllers
{
    public class CabanaController : Controller
    {
        public CabanasContext CabanasContext = new CabanasContext();

        IRepositorioCabana _repoCabana = CrearRepositorio();
        IRepositorioTipo _repoTipo = CrearRepositorioTipo();
        IRepositorioFoto _repoFotos = CrearRepositorioFoto();
        IRepositorioParametro _repoParametro = CrearRepositorioParametro();
        private readonly ISession _session;
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _environment;
        IRepositorioMantenimiento _repoMantenimiento = CrearRepositorioMantenimiento();

        public CabanaController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, 
            IWebHostEnvironment environment)
        {
            _logger = logger;
            this._environment = environment;
            _session = httpContextAccessor.HttpContext.Session;
        }

        private static IRepositorioMantenimiento CrearRepositorioMantenimiento()
        {
            return new RepositorioMantenimiento();

        }

        private static IRepositorioFoto CrearRepositorioFoto()
        {
            return new RepositorioCantFotos();

        }

        private static IRepositorioTipo CrearRepositorioTipo()
        {
            return new RepositorioTipo();

        }
        private static IRepositorioParametro CrearRepositorioParametro()
        {
            return new RepositorioParametro();

        }

        private static IRepositorioCabana CrearRepositorio()
        {
            return new RepositorioCabana();

        }

        // GET: Cabana
        public ActionResult Index()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View(CabanasContext.Cabañas.ToList());
            }
            else
                return RedirectToAction("Index", "Home");
           
        }

        // GET: Cabana/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cabana/Create
        public ActionResult Create()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                IEnumerable<Tipo> tipos = _repoTipo.FindAll();
                if (tipos != null)
                {
                    ViewBag.MisTipos = tipos;
                }
                else
                {
                    ViewBag.MisTipos = new List<Tipo>();
                }
                return View();
            }
            else
                return RedirectToAction("Index", "Home");

           
        }

        // POST: Cabana/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cabaña unaCabana, IFormFile imagen)
        {

            IEnumerable<Tipo> tipos = _repoTipo.FindAll();
            
            if (tipos != null)
            {
                ViewBag.MisTipos = tipos;
            }

            unaCabana.Tipo = CabanasContext.Tipos.FirstOrDefault(u => u.Id == unaCabana.TipoId);
            try
            {
                if (unaCabana == null)
                {
                    return BadRequest("El usuario es nulo, no podemos seguir adelante");
                }

                int topeMinCab = _repoParametro.GetValor("TopeMinDescCabaña");
                int topeMaxCab = _repoParametro.GetValor("TopeMaxDescCabaña");

                if (GuardarImagen(imagen, unaCabana))
                {
                    _repoCabana.Agregar(unaCabana, topeMinCab, topeMaxCab);
                    return RedirectToAction(nameof(Index));
                }
                @ViewBag.Error = "La imagen es OBLIGATORIA";
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(unaCabana);
            }
        }

        
        public ActionResult Edit(int id)
        {
            return View();
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


        public ActionResult BuscarXNombre()
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
        public ActionResult BuscarXNombre(string nombre)
        {
            try
            {
                if (!String.IsNullOrEmpty(nombre))
                {
                    ViewBag.Lista = _repoCabana.SearchCabaña(nombre);

                    return View();
                }
                ViewBag.Mensaje = "Intentando con Campo Vacio";
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "No hubo coincidencia con alguna cabaña";
                return View();
            }
        }



        public ActionResult BuscarXTipo()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }


        //BUSCAR POR TIPO
        [HttpPost]
        public ActionResult BuscarXTipo(int TipoId)
        {

            try
            {
                if (TipoId != 0)
                {
                    var cabañasEncontradas = _repoCabana.SearchCabañaXTipo(TipoId);
                    ViewBag.CabañasEncontradas = cabañasEncontradas;

                    return View();
                }
                ViewBag.Mensaje = "Intentando con Campo Vacio";
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "No hubo coincidencia con alguna cabaña";
                return View();
            }
        }



        public ActionResult BuscarXCantPersonas()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }


        //BUSCAR POR cantidad personas
        [HttpPost]
        public ActionResult BuscarXCantPersonas(int cantidad)
        {

            try
            {
                if (cantidad > 0)
                {
                    var cabañasEncontradas = _repoCabana.SearchCabañaXCap(cantidad);
                    ViewBag.CabañasEncontradas = cabañasEncontradas;

                    return View();
                }
                ViewBag.Mensaje = "Intentando con Campo Vacio";
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "No hubo coincidencia con alguna cabaña";
                return View();
            }
        }


        public ActionResult BuscarHabilitadas()
        {
            if (_session.Keys.Any()) // Hay variables de Sesion ?
            {
                try
                {
                var cabañasEncontradas = _repoCabana.SearchCabañaHabilitada();
                ViewBag.CabañasEncontradas = cabañasEncontradas;

                return View();

                }
                catch
                {
                    ViewBag.Mensaje = "No hubo coincidencia con alguna cabaña";
                    return View();
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }




        private bool GuardarImagen(IFormFile imagen, Cabaña p)
        {
            if (imagen == null || p == null) return false;
            // SUBIR LA IMAGEN
            //ruta física de wwwroot
            string rutaFisicaWwwRoot = _environment.WebRootPath;
            _repoFotos.Add();
            var indice = CabanasContext.Fotos.OrderByDescending(n => n.Id).FirstOrDefault().Id.ToString();

            string format = "";
            string[] formato = { p.Nombre, "_", indice, imagen.FileName.Substring(imagen.FileName.Length - 4, 4) };

            for (int i = 0; i < formato.Length; i++)
            {
                format += formato[i];
            }

            string nombreImagen = format;
            //ruta donde se guardan las fotos de las personas
            string rutaFisicaFoto = Path.Combine
            (rutaFisicaWwwRoot, "imagenes", "fotos", nombreImagen);
            //FileStream permite manejar archivos
            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar
                using (FileStream f = new FileStream(rutaFisicaFoto, FileMode.Create))
                {
                    //Para archivos grandes o varios archivos usar la versión
                    //asincrónica de CopyTo. Sería: await imagen.CopyToAsync (f);
                    imagen.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO 
                p.Foto = nombreImagen;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public ActionResult MantenimientoDeCabaña(int idHab)
        {
            try
            {
                var cabañas = _repoCabana.SearchCabañaXHabitacion(idHab);
                var mantenimientos = new List<Mantenimiento>();
                if (cabañas != null && cabañas.Any())
                {
                    mantenimientos = (List<Mantenimiento>)_repoMantenimiento.GetMantenimientosPorCabaña(cabañas.First().NumeroHabitacion);
                    ViewBag.NumHab = idHab;
                    return View("MantenimientoDeCabaña", mantenimientos);
                }
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }



    }
}
