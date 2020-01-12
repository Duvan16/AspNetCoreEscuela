using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreEscuela.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreEscuela.Controllers
{
    public class AlumnoController : Controller
    {
       public IActionResult Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var alumno = from alumn in _context.Alumnos
                                 where alumn.Id == id
                                 select alumn;

                return View(alumno.SingleOrDefault());
            }
            else
            {
                return View("MultiAlumno", _context.Alumnos);
            }

        }
        public IActionResult MultiAlumno()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAlumno", _context.Alumnos);
        }

        private EscuelaContext _context;
        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}