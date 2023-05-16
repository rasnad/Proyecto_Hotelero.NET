using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioMantenimiento : IRepositorio<Mantenimiento>
    {
        IEnumerable<Mantenimiento> MantenimientoEntreFechas(DateTime fecha1, DateTime fecha2);
        void Agregar(Mantenimiento m, int topeMin, int topeMax);
        IEnumerable<Mantenimiento> GetMantenimientosPorCabaña(int numeroHabitacion);
        int CantidadMantenimientosDia(int idCabaña, DateTime date);

    }
}
