using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioParametro : IRepositorio<Parametro>
    {
        int GetValor(string nombreParametro);
        void SetValor(string nombreParametro, int nuevoValor);
    }
}
