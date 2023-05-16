using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private CabanasContext _db = new CabanasContext();

        public void Add(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException("Error: El usuario no puede ser nulo");
            //Si no tengo la certeza de que el validar lanzaría una excepción
            // if (!usuario.Validar()) throw new InvalidOperationException("El usuario no es válido");

            //como sé que Validar() lanza una excepción solo la dejo pasar en caso de que se produzca.
           
            try
            {
                usuario.Validar();
                _db.Usuarios.Add(usuario);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo dar de alta el usuario. {ex.Message}");
            }
        }


        public void Remove(Usuario obj)
        {
            try
            {
                var usuario = _db.Usuarios.FirstOrDefault(u => u == obj);

                if (usuario == null)
                {
                    throw new Exception($"No se encontró el usuario {obj}");
                }

                _db.Usuarios.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el tipo {obj}: {ex.Message}");
            }
        }


        public IEnumerable<Usuario> FindAll()
        {
            try
            {

                var usuarios = _db.Usuarios.ToList();
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo encontrar Usuarios{ex}");
            }
        }


        public Usuario FindById(int id)
        {
            try
            {
                var usuario = _db.Usuarios.Find(id);
                if (usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo encontrar el usuario: {ex}");
            }
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public bool Login(string email, string pass)
        {
            try 
            {
                var usuario = _db.Usuarios.Any(u => u.Email == email && u.Password == pass);
                if (usuario) {
                    return usuario;
                }

                throw new Exception();
            }
            catch (Exception ex) 
            { 
                throw new Exception("Credenciales Incorrectas");
            }
          
                

           
        }
    }
}
