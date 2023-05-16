 using Libreria.LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class CabanasContext:DbContext
    {
        public DbSet<CantidadFotos> Fotos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cabaña> Cabañas { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cadenaConexion =
                @"SERVER=(localdb)\mssqllocaldb;
                DATABASE=ObligatorioP32023;
                INTEGRATED SECURITY=TRUE;
                ENCRYPT=False"; //Puede evitar problemas si no hay un certificado y se usa SSL
            optionsBuilder.UseSqlServer(cadenaConexion)
                .EnableDetailedErrors();
        }

       

    }
}
