using Libreria.LogicaNegocio.InterfazEntidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    //[Index(nameof(Nombre), IsUnique=true)]
    // Para decir a nivel de bd que ese dato debe ser unique
    [Index(nameof(Nombre), IsUnique = true)]
    public class Cabaña : IValidable
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumeroHabitacion { get; set; }
        
        public string Nombre { get; set; }
        [ForeignKey(nameof(Tipo))]
        public int TipoId {get; set;}
        public Tipo Tipo { get; set; }
        public string Descripcion { get; set; }
        public bool? ConJacuzzi { get; set; } // Bool nulables
        public bool? Habilitada { get; set; } // Bool nulables
        public int CantPersonas { get; set; }
        public string? Foto { get; set; }




        public void ValidarNombre(string nombre)
        {
            nombre = nombre.Trim();
            Regex regex = new Regex(@"^[\p{L} ]+$");
            
            if (!regex.IsMatch(nombre))
            {
                throw new Exception("El nombre solo debe contener letras.");
            }
        }


        public void ValidarNombreRepetido(IEnumerable<Cabaña> cabañas, string nombre)
        {
            if (cabañas.Any(c => c.Nombre.ToUpper() == nombre.ToUpper()))
            {
                throw new Exception("Error: campos inválidos");
            }
        }


        public void Validar() // Valida campos vacíos o nulos.
        {
            if (string.IsNullOrEmpty(Nombre) || Tipo == null || ConJacuzzi == null || Habilitada == null || CantPersonas <= 0
                || string.IsNullOrEmpty(Foto))
            {
                throw new Exception("Error: campos inválidos");
            }
        }

        // HACER LOGICA DE FOTO.
        public void ValidacionFoto()
        {
            throw new NotImplementedException();
        }

        public void ValidarDesc(int topeMin, int topeMax)
        {
            if (Descripcion.Length > topeMin && Descripcion.Length < topeMax)
            {
                this.Validar();
            }
            else {
                throw new Exception($"Error: la descripcion debe estar entre los valores {topeMin} y {topeMax}");
            }
           
        }

    }
}
