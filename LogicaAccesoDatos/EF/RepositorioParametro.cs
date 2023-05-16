using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioParametro : IRepositorioParametro
    {
        public CabanasContext _db = new CabanasContext();
        public void Add(Parametro p)
        {
            if (p == null)
            {
                throw new Exception("El Parametro no puede ser vacio");
            }
            try
            {
                _db.Parametros.Add(p);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"El Parametro no se dio de alta {ex}");
            }
        }

        public IEnumerable<Parametro> FindAll()
        {

            try
            {
                var parametro = _db.Parametros.ToList();
                return parametro;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las parametro: {ex}");
            }
        }

        public Parametro FindById(int id)
        {
            try
            {
                var parametro = _db.Parametros.Find(id);
                if (parametro == null)
                {
                    throw new Exception("El parametro no existe");
                }
                return parametro;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo encontrar el parametro: {ex}");
            }
        }

        public void Remove(Parametro obj)
        {
            try
            {
                var parametro = _db.Parametros.FirstOrDefault(p => p == obj);

                if (parametro == null)
                {
                    throw new Exception($"No se encontró el parametro {obj}");
                }

                _db.Parametros.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el tipo {obj}: {ex.Message}");
            }
        }

        public void Update(Parametro obj)
        {
            throw new NotImplementedException();
        }

        public int GetValor(string nombreParametro)
        {
            try
            {
                var parametro = _db.Parametros.SingleOrDefault(p => p.Nombre == nombreParametro);
                if (parametro == null)
                {
                    throw new Exception($"No se encontró el parámetro con nombre {nombreParametro}");
                }
                return parametro.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo un error {ex}");
            }
        }

        public void SetValor(string nombreParametro, int nuevoValor)
        {
            try
            {
                var q = _db.Parametros.SingleOrDefault(p => p.Nombre == nombreParametro);
                q.Valor = nuevoValor;
                _db.SaveChanges();
            }catch (Exception ex) 
            { 
                throw new Exception($"Hubo un error {ex}");
            }
        }
    }
}
