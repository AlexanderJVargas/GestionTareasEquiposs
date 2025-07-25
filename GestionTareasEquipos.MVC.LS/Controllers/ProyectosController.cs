﻿using Libreria.API.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionTareasEquipos.Modelos;

namespace GestionTareasEquipos.MVC.LS.Controllers
{
    public class ProyectosController : Controller
    {
        // GET: ProyectosController
        public ActionResult Index()
        {
            var data = Crud<Proyecto>.GetAll();
            return View(data);
        }

        // GET: ProyectosController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            

            var todasTareas = Crud<Tareas>.GetAll();
            var tareasDelProyecto = todasTareas.Where(t => t.ProyectoId == id).ToList();

            // Pasar las tareas a la vista usando ViewBag
            ViewBag.TareasDelProyecto = tareasDelProyecto;

            return View(data);
        }

        // GET: ProyectosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proyecto data)
        {
            try
            {
                Crud<Proyecto>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: ProyectosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            return View(data);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proyecto data)
        {
            try
            {
                Crud<Proyecto>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: ProyectosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            return View(data);
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Proyecto data)
        {
            try
            {
                Crud<Proyecto>.Delete(id);
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
