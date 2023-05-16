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
    public class RepositorioMantenimiento : IRepositorioMantenimiento
    {

        public CabanasContext _db = new CabanasContext();

        public void Add(Mantenimiento obj)
        {
            
            try
            {
                if (obj != null)
                {
                    obj.ValidarFecha();

                    _db.Entry(obj.Cabaña).State = EntityState.Unchanged;
                    _db.Mantenimientos.Add(obj);
                    _db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"El Mantenimiento no se dio de alta {ex}");
            }
        }

        public void Agregar(Mantenimiento m, int topeMin, int topeMax)
        {
            m.ValidarDesc(topeMin, topeMax);
            this.Add(m);
        }


        public IEnumerable<Mantenimiento> FindAll()
        {
            try
            {
                var mantenimiento = _db.Mantenimientos.ToList();
                return mantenimiento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los mantenimientos: {ex}");
            }
        }

        public Mantenimiento FindById(int id)
        {
            try
            {
                var mantenimiento = _db.Mantenimientos.Find(id);
                if (mantenimiento == null)
                {
                    throw new Exception("El mantenimiento no existe");
                }
                return mantenimiento;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo encontrar el mantenimiento: {ex}");
            }
        }

        public IEnumerable<Mantenimiento> MantenimientoEntreFechas(DateTime fecha1, DateTime fecha2)
        {
            try
            {
                var mantenimientos = _db.Mantenimientos
                    .Where(m => m.FechaMantenimiento >= fecha1 && m.FechaMantenimiento <= fecha2)
                    .ToList();

                if (mantenimientos.Count == 0)
                {
                    throw new Exception($"No se encontraron mantenimientos entre las fechas {fecha1.ToShortDateString()} y {fecha2.ToShortDateString()}");
                }

                return mantenimientos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los mantenimientos: {ex.Message}");
            }
        }


        public void Remove(Mantenimiento obj)
        {
            try
            {
                var mantenimiento = _db.Mantenimientos.FirstOrDefault(m => m == obj);

                if (mantenimiento == null)
                {
                    throw new Exception($"No se encontró el mantenimiento {obj}");
                }

                _db.Mantenimientos.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el tipo {obj}: {ex.Message}");
            }
        }




        public IEnumerable<Mantenimiento> GetMantenimientosPorCabaña(int numeroHabitacion)
        {
            try
            {
                var cabaña = _db.Cabañas.FirstOrDefault(c => c.NumeroHabitacion == numeroHabitacion);

                if (cabaña == null)
                {
                    throw new Exception($"No se encontró una cabaña con el número de habitación {numeroHabitacion}.");
                }

                var mantenimientos = _db.Mantenimientos
                    .Where(m => m.IdCabaña == cabaña.NumeroHabitacion)
                    .ToList();
                if (mantenimientos == null)
                {
                    throw new Exception($"No se encontró un mantenimiento con el número de habitación {numeroHabitacion}.");
                }
                return mantenimientos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los mantenimientos de la cabaña con número de habitación {numeroHabitacion}: {ex.Message}");
            }
        }

        public int CantidadMantenimientosDia(int idCabaña, DateTime fecha)
        {
            try
            {
                var cantMantenimientos = _db.Mantenimientos
                 .Count(m => m.IdCabaña == idCabaña && m.FechaMantenimiento.Date == fecha);
                if (cantMantenimientos >= 3)
                {
                    throw new Exception($"Solo se pueden realizar 3 mantenimientos por día");
                }
                return cantMantenimientos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


















        public void Update(Mantenimiento obj)
        {
            throw new NotImplementedException();
        }
    }
}
