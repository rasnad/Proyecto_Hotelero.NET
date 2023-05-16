using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioCantFotos : IRepositorioFoto
    {
        CabanasContext _db = new CabanasContext();

        public void Add()
        { 
            CantidadFotos CantidadFotos = new CantidadFotos();

            _db.Add(CantidadFotos);
        }

    }
}
