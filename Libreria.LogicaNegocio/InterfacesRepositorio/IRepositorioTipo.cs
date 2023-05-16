using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioTipo: IRepositorio<Tipo>
    {
        IEnumerable<Tipo> ListarTipo();
        Tipo SearchTipo(string nombreT);
        IEnumerable<Tipo> GetTipos(string nombreT);
        void RemovePorNombre(string nombreT);
        void Agregar(Tipo t, int topeMin, int topeMax);
        void UpdateTipo(Tipo tipoActualizado, int topeMin, int topeMax);
    }
}
