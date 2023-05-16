using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
 
namespace LogicaAccesoDatos.EF
{
    public class RepositorioTipo : IRepositorioTipo
    {

        private CabanasContext _db = new CabanasContext();  

        public void Add(Tipo obj)
        {

            try
            {
                if (obj == null)
                {
                    throw new Exception("El tipo no puede ser vacio");
                }

                obj.ValidarNombre(obj.Nombre);

                if (_db.Tipos.Any(t => t.Nombre == obj.Nombre))
                {
                    throw new Exception($"Ya existe un tipo con el nombre {obj.Nombre}");
                }

                _db.Tipos.Add(obj);
                _db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw new Exception($"El Usuario no se dio de alta -  {ex.Message}");
            }
        }

        public void Agregar(Tipo t, int topeMin, int topeMax)
        {
            t.ValidarDesc(topeMin, topeMax);
            this.Add(t);
        }

        public IEnumerable<Tipo> FindAll()
        {
            try
            {
                var tipo = _db.Tipos.ToList();
                return tipo;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo encontrar tipos{ex}");
            }
        }

        public Tipo FindById(int id)
        {
            try
            {
                var tipo = _db.Tipos.Find(id);
                if (tipo == null)
                {
                    throw new Exception("El tipo no existe");
                }
                return tipo;
            }
            catch (Exception ex)
            { 
                throw new Exception($"No se pudo encontrar el tipo: {ex}");
            }
        }

        public IEnumerable<Tipo> ListarTipo()
        {
            try
            {
                var tipo = _db.Tipos.ToList(); 
                return tipo;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener los tipos de cabañas: {ex}");
            }
        }

        public void RemovePorNombre(string nombreT)
        {
            try
            {
                var tipo = _db.Tipos.FirstOrDefault(t => t.Nombre == nombreT);

                if (tipo == null)
                {
                    throw new Exception($"No se encontró el tipo {nombreT}");
                }

                var cabañasConTipo = _db.Cabañas.Where(c => c.Tipo == tipo).ToList();

                if (cabañasConTipo.Any())
                {
                    throw new Exception($"El tipo {nombreT} está siendo usado por {cabañasConTipo.Count} cabañas y no puede ser eliminado.");
                }

                _db.Tipos.Remove(tipo);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el tipo {nombreT}: {ex.Message}");
            }
        }

        public Tipo SearchTipo(string nombreT)
        {
            try
            {
                var tipo = _db.Tipos.Find(nombreT);
                if (tipo == null)
                {
                    throw new Exception($"Tipo con nombre {nombreT} no encontrado.");
                }
                return tipo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el tipo: {ex}");
            }
        }

        public IEnumerable<Tipo> GetTipos(string nombreT)
        {
            try
            {
                var tipos = _db.Tipos.Where(t => t.Nombre == nombreT).ToList();
                if (tipos.Count == 0)
                {
                    throw new Exception($"Tipo con nombre {nombreT} no encontrado.");
                }
                return tipos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el tipo: {ex}");
            }
        }

        public void UpdateTipo(Tipo tipoActualizado, int topeMin, int topeMax)
        {
            try
            {
                // Buscamos el tipo actual en la base de datos
                var tipoExistente = _db.Tipos.FirstOrDefault(t => t.Id == tipoActualizado.Id);
                
                if (tipoExistente != null)
                {
                    tipoActualizado.Descripcion = tipoActualizado.Descripcion.Trim();
              
                    // Actualizamos las propiedades del tipo existente con las del tipo actualizado

                    if (tipoActualizado.Descripcion == "") {
                        tipoActualizado.Descripcion = tipoExistente.Descripcion; 
                    }
                    tipoActualizado.ValidarDesc(topeMin, topeMax);
                    tipoExistente.Descripcion = tipoActualizado.Descripcion;

                    if (tipoActualizado.CostoPorHuesped <= 0) {
                        tipoActualizado.CostoPorHuesped = tipoExistente.CostoPorHuesped;
                    }

                    if (tipoActualizado.CostoPorHuesped > 0)
                    {
                        tipoExistente.CostoPorHuesped = tipoActualizado.CostoPorHuesped;
                    }
                    
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar el tipo {tipoActualizado}: {ex.Message}, [CAMPOS INVALIDOS]");
            }
        }

        public void Remove(Tipo obj)
        {
            try
            {
           
                var Asociado = _db.Tipos.Join(_db.Cabañas, tipo => obj.Id, cabana => cabana.TipoId,
                                (tipo, cabana) => new { Tipo = tipo, Cabana = cabana }).ToList();

                if (Asociado.Count == 0) {
                    _db.Tipos.Remove(obj);
                    _db.SaveChanges();
                }
                throw new Exception("El tipo se encuentra asociado a una Cabaña");
                
            } 
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el tipo {obj.Nombre} - {ex.Message}");
            }
        }

        public void Update(Tipo obj)
        {
            throw new NotImplementedException();
        }
    }
}
