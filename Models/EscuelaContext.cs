using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreEscuela.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }

        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();
            escuela.AñoDeCreacion = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Platzi School";
            escuela.Ciudad = "Bogota";
            escuela.Pais = "Colombia";
            escuela.Dirección = "Avenida Siempre Viva";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            //Cargar cursos de la escuela
            var cursos = CargarCursos(escuela);

            //X cada curso cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);

            //X cada curso cargar alumnos
            var alumnos = CargarAlumnos(cursos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();
            Random rnd = new Random();
            foreach (var curso in cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                var tmpList = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmpList);
            }
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var Curso in cursos)
            {
                var tmpList = new List<Asignatura>(){
                    new Asignatura{Id = Guid.NewGuid().ToString(),CursoId = Curso.Id,Nombre="Matemáticas"},
                    new Asignatura{Id = Guid.NewGuid().ToString(),CursoId = Curso.Id, Nombre="Educación Física"},
                    new Asignatura{Id = Guid.NewGuid().ToString(),CursoId = Curso.Id, Nombre="Castellano"},
                    new Asignatura{Id = Guid.NewGuid().ToString(),CursoId = Curso.Id, Nombre="Ciencia Naturales"}
                };
                listaCompleta.AddRange(tmpList);
                // Curso.Asignaturas = tmpList;
            }
            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso() { Id = Guid.NewGuid().ToString(),EscuelaId = escuela.Id, Nombre = "101",
                Dirección = "Avenida Siempre Viva", Jornada=TiposJornada.Mañana},
                new Curso() { Id = Guid.NewGuid().ToString(),EscuelaId = escuela.Id, Nombre = "201",
                Dirección = "Avenida Siempre Viva",Jornada=TiposJornada.Mañana},
                new Curso() { Id = Guid.NewGuid().ToString(),EscuelaId = escuela.Id, Nombre = "301"
                , Dirección = "Avenida Siempre Viva",Jornada=TiposJornada.Mañana},
                new Curso() { Id = Guid.NewGuid().ToString(),EscuelaId = escuela.Id, Nombre = "401", Dirección = "Avenida Siempre Viva",Jornada=TiposJornada.Tarde},
                new Curso() { Id = Guid.NewGuid().ToString(),EscuelaId = escuela.Id, Nombre = "501", Dirección = "Avenida Siempre Viva",Jornada=TiposJornada.Tarde},
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(
            Curso curso,
            int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipe", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Marty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $" {n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((a1) => a1.Id).Take(cantidad).ToList();
        }
    }
}