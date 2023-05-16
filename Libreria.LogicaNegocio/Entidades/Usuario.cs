using Libreria.LogicaNegocio.InterfazEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades
{
    [Table("Usuario")]
    public class Usuario : IValidable
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validar() // Validacion a la hora de hacer login
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                throw new Exception("No se pudo autenticar los datos.");
            }

            ValidarPass(Password);
            ValidarLogicaMail(Email);


        }

        public void ValidarPass(string password) // Validacion de pwd a la hora del registro.
        {

            bool minuscula = false;
            bool mayuscula = false;
            bool numero = false;

            char[] charArray = password.ToCharArray();
            if (string.IsNullOrEmpty(password.Trim()) || password.Length < 6)
            {
                throw new Exception("- La contraseña no puede estar vacía ni ser menor a 6 caracteres. ");
            }

            foreach (char chars in charArray)
            {
                if (char.IsUpper(chars))
                {
                    mayuscula = true;
                }
                else if (char.IsLower(chars))
                {
                    minuscula = true;
                }
                else if (char.IsDigit(chars))
                {
                    numero = true;
                }
                if (mayuscula && minuscula && numero)
                {
                    return;
                }
            }

            throw new Exception("- La contraseña debe tener al menos una mayuscula, una minuscula y un numero.");
        }

        // VALIDAR EMAIL REPETIDO P0ARA EL REGISTRO.   Este 
        public void ValidarEmailRepetido(IEnumerable<Usuario> usuarios)
        {
            if (usuarios.Any(u => u.Email == Email))
            {
                throw new Exception("Email ya utilizado.");
            }
        }


        // Validacion de mail a la hora del registro.
        public void ValidarLogicaMail(string email)
        {
            if (string.IsNullOrEmpty(email.Trim()))
            {
                throw new Exception("El email no puede estar vacío");
            }

            bool arroba = email.Contains("@");
            bool verifPosArroba = email.StartsWith("@") || email.EndsWith("@");

            if (!arroba || verifPosArroba)
            {
                throw new Exception("El correo es inválido o no presenta @");
            }
        }

    }

}
