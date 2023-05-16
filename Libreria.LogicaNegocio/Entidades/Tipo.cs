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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Libreria.LogicaNegocio.Entidades
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Tipo : IValidable
    {
        [Key]

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CostoPorHuesped { get; set; }



        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre) || CostoPorHuesped <= 0 || string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("Error: campos inválidos");
            }
        }

        public void ValidarNombre(string nombre) {
            nombre = nombre.Trim();
            Regex regex = new Regex(@"^[\p{L} ]+$");

            if (!regex.IsMatch(nombre))
            {
                throw new Exception("El nombre solo debe contener letras.");
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