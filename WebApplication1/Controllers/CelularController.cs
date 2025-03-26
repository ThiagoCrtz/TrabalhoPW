using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CelularController : Controller
    {
        // GET: Celular
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Lista()
        {
            Celular.GerarLista(Session);
            return View(Session["ListaCell"] as List<Celular>);
        }
        public ActionResult Details(Celular celular, int id)
        {
            ViewBag.id = id;
            return View(Celular.Procurar(Session, id));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Celular());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Celular celular)
        {
            celular.Adicionar(Session);
            return RedirectToAction("Lista");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Celular.Procurar(Session, id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Celular celular, int id)
        {
            Celular.Procurar(Session, id)?.Excluir(Session);
            return RedirectToAction("Lista");
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            return View(Celular.Procurar(Session, id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Celular celular)
        {
            celular.Editar(Session, id);
            return RedirectToAction("Lista");
        }


    }
}