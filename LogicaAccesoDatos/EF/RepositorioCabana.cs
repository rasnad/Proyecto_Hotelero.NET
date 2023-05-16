using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioCabana : IRepositorioCabana
    {
        public CabanasContext _db = new CabanasContext();

        public void Add(Cabaña unaCabana)
        {

            try
            {
                if (unaCabana != null)
                {
                    unaCabana.ValidarNombre(unaCabana.Nombre);
                    unaCabana.ValidarNombreRepetido(FindAll(), unaCabana.Nombre);

                    _db.Entry(unaCabana.Tipo).State = EntityState.Unchanged;
                    _db.Cabañas.Add(unaCabana);
                    _db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la cabaña: {ex.Message}");
            }
        }


        public IEnumerable<Cabaña> FindAll()
        {
            try
            {
                var cabaña = _db.Cabañas.ToList();
                return cabaña;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las cabañas: {ex}");
            }
        }

        public Cabaña FindById(int id)
        {
            try
            {
                var cabaña = _db.Cabañas.Find(id);
                if (cabaña == null)
                {
                    throw new Exception("El cabaña no existe");
                }
                return cabaña;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo encontrar el cabaña: {ex}");
            }
        }

        public void Remove(Cabaña obj)
        {
            try
            {
                var cabaña = _db.Cabañas.FirstOrDefault(c => c == obj);

                if (cabaña == null)
                {
                    throw new Exception($"No se encontró la cabaña {obj}");
                }

                _db.Cabañas.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el tipo {obj}: {ex.Message}");
            }
        }

        public List<Cabaña> SearchCabaña(string nombreC)
        {
            try
            {
                List<Cabaña> cabañas = _db.Cabañas.Where(c => c.Nombre.Contains(nombreC) || c.Descripcion.Contains(nombreC)).ToList();

                if (cabañas.Count == 0)
                {
                    throw new Exception($"No se encontraron cabañas con el texto '{nombreC}'.");
                }

                return cabañas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las cabañas: {ex}");
            }
        }

        public IEnumerable<Cabaña> SearchCabañaHabilitada()
        {
            try
            {
                var cabañasHabilitadas = _db.Cabañas.Where(c => c.Habilitada == true).ToList();

                if (cabañasHabilitadas.Count == 0)
                {
                    throw new Exception($"No se encontraron cabañas habilitadas.");
                }

                return cabañasHabilitadas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las cabañas habilitadas: {ex}");
            }
        }


        public IEnumerable<Cabaña> SearchCabañaXCap(int cant)
        {
            try
            {
                if (cant <= 0)
                {
                    throw new Exception($"La cantidad debe ser mayor a 0");
                }
                else if (cant < 3)
                {
                    throw new Exception($"No se encontró una cabaña con capacidad suficiente para {cant} personas.");
                }

                var cabañas = _db.Cabañas.Where(c => c.CantPersonas >= cant).ToList();

                return cabañas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las cabañas: {ex}");
            }
        }



        public List<Cabaña> SearchCabañaXTipo(int TipoId)
        {

            try
            {
                List<Cabaña> ListaCabañas = _db.Cabañas.Where(c => c.TipoId == TipoId).ToList();

                return ListaCabañas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las cabañas: {ex.Message}");
            }
        }

        public void Agregar(Cabaña c, int topeMin, int topeMax)
        {
            c.ValidarDesc(topeMin, topeMax);
            this.Add(c);
        }



        public void Update(Cabaña obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cabaña> SearchCabañaXHabitacion(int idHab)
        {
            try
            {
                var cabañasHabilitadas = _db.Cabañas
                    .Where(c => c.NumeroHabitacion == idHab)
                    .ToList();

                if (cabañasHabilitadas.Count == 0)
                {
                    throw new Exception($"No se encontraron cabañas con el número de habitación {idHab}.");
                }

                return cabañasHabilitadas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las cabañas habilitadas: {ex.Message}");
            }
        }
    }
}
