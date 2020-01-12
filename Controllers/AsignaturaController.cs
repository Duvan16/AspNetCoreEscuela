using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreEscuela.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreEscuela.Controllers
{
    public class AsignaturaController : Controller
    {
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignturaId}")]
        public IActionResult Index(string asignturaId)
        {
            if (!string.IsNullOrWhiteSpace(asignturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                 where asig.Id == asignturaId
                                 select asig;

                return View(asignatura.SingleOrDefault());
            }
            else
            {
                return View("MultiAsignatura", _context.Asignaturas);
            }

        }
        public IActionResult MultiAsignatura()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAsignatura", _context.Asignaturas);
        }

        private EscuelaContext _context;
        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }
    }
}