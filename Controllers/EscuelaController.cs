using System;
using System.Linq;
using AspNetCoreEscuela.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreEscuela.Controllers
{
    public class EscuelaController : Controller
    {
        private EscuelaContext _context;
        public IActionResult Index()
        {
            ViewBag.CosaDinamica = "La Monja";
            var escuela = _context.Escuelas.FirstOrDefault();
            return View(escuela);
        }

        public EscuelaController(EscuelaContext context)
        {
            _context = context;
        }
    }
}