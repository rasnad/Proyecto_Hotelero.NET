using Libreria.LogicaNegocio.InterfazEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    public class Mantenimiento : IValidable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMantenimiento { get; set; }
        [ForeignKey(nameof(Cabaña))] // Foreign key que hace referencia a la cabaña y tambien su key es la de la cabaña
        public int IdCabaña { get; set; }
        public Cabaña Cabaña { get; set; }
        public DateTime FechaMantenimiento { get; set; } 
        public string Descripcion { get; set; }
        public int Costo { get; set; }
        public string Trabajador { get; set; } 
 

        public void Validar()
        {
            if (Costo <= 0 || string.IsNullOrEmpty(Trabajador)
                || Cabaña == null)
            {
                throw new Exception("Error: campos inválidos");
            }
        }

        public void ValidarFecha()
        {
            if (FechaMantenimiento.Year < 2023) {
                throw new Exception("Error: Fecha/Año no puede ser menor a 2023.");
            } 
        }

        public void ValidarDesc(int topeMin, int topeMax)
        {
            if (Descripcion.Length > topeMin && Descripcion.Length < topeMax)
            {
                this.Validar();
            }
            else
            {
                throw new Exception($"Error: la descripcion debe estar entre los valores {topeMin} y {topeMax}");
            }

        }

        

    }
}
