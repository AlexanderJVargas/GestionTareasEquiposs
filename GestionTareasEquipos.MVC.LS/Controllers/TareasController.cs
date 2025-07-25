using GestionTareasEquipos.Modelos;
using Libreria.API.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionTareasEquipos.MVC.LS.Controllers
{
    public class TareasController : Controller
    {
        // GET: TareasController
        public ActionResult Index()
        {
            var data = Crud<Tareas>.GetAll();
            return View(data);
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Tareas>.GetById(id);
            return View(data);
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tareas data)
        {
            try
            {
                Crud<Tareas>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Tareas>.GetById(id);
            return View(data);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tareas data)
        {
            try
            {
                var existingTarea = Crud<Tareas>.GetById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Tareas>.GetById(id);
            return View(data);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tareas data)
        {
            try
            {
                Crud<Tareas>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        
        public ActionResult Reportes(string estado, string prioridad)
        {
            var todasTareas = Crud<Tareas>.GetAll();

            if (!string.IsNullOrEmpty(estado))
            {
                todasTareas = todasTareas.Where(t => t.Estado == estado).ToList();
            }

            if (!string.IsNullOrEmpty(prioridad))
            {
                todasTareas = todasTareas.Where(t => t.Prioridad == prioridad).ToList();
            }

            // Ordenar por fecha límite
            todasTareas = todasTareas.OrderBy(t => t.FechaLimite).ToList();

            ViewBag.Estados = new SelectList(new[] { "Pendiente", "En Progreso", "Completada", "Cancelada" });
            ViewBag.Prioridades = new SelectList(new[] { "Alta", "Media", "Baja" });
            ViewBag.EstadoSeleccionado = estado;
            ViewBag.PrioridadSeleccionada = prioridad;

            return View(todasTareas);
        }

        public ActionResult Busqueda(int? proyectoId, int? usuarioId)
        {
            var todasTareas = Crud<Tareas>.GetAll();

            if (proyectoId.HasValue)
            {
                todasTareas = todasTareas.Where(t => t.ProyectoId == proyectoId.Value).ToList();
            }

            if (usuarioId.HasValue)
            {
                todasTareas = todasTareas.Where(t => t.UsuarioId == usuarioId.Value).ToList();
            }

            CargarViewBags();
            ViewBag.ProyectoSeleccionado = proyectoId;
            ViewBag.UsuarioSeleccionado = usuarioId;

            return View(todasTareas);
        }

        private void CargarViewBags()
        {
            try
            {
                var proyectos = Crud<Proyecto>.GetAll();
                var usuarios = Crud<Usuario>.GetAll();

                ViewBag.Proyectos = new SelectList(proyectos, "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
                ViewBag.Estados = new SelectList(new[] { "Pendiente", "En Progreso", "Completada", "Cancelada" });
                ViewBag.Prioridades = new SelectList(new[] { "Alta", "Media", "Baja" });
            }
            catch (Exception)
            {
                ViewBag.Proyectos = new SelectList(new List<Proyecto>(), "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(new List<Usuario>(), "Id", "Nombre");
                ViewBag.Estados = new SelectList(new[] { "Pendiente", "En Progreso", "Completada", "Cancelada" });
                ViewBag.Prioridades = new SelectList(new[] { "Alta", "Media", "Baja" });
            }
        }

    }
}
