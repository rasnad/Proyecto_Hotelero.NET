using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioCabana : IRepositorio<Cabaña>
    {
        List<Cabaña> SearchCabaña(string nombreC);
        List<Cabaña> SearchCabañaXTipo(int TipoId);
        IEnumerable<Cabaña> SearchCabañaXCap(int cant);
        IEnumerable<Cabaña> SearchCabañaHabilitada();
        void Agregar(Cabaña c, int topeMin, int topeMax);
        IEnumerable<Cabaña> SearchCabañaXHabitacion(int idHab);

    }
}
