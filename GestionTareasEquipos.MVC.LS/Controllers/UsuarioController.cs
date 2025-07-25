using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionTareasEquipos.Modelos;
using Libreria.API.Consumer;

namespace GestionTareasEquipos.MVC.LS.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            var data = Crud<Usuario>.GetAll();
            return View(data);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Usuario>.GetById(id);
            return View(data);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario data)
        {
            try
            {
                Crud<Usuario>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Usuario>.GetById(id);
            return View(data);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario data)
        {
            try
            {
                Crud<Usuario>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Usuario>.GetById(id);
            return View(data);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario data)
        {
            try
            {
                Crud<Usuario>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }
    }
}
