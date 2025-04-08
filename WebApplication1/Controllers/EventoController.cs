using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApplication1.Reports;
namespace WebApplication1.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Report(Evento eventos)
        {
            EventoReport report = new EventoReport();
            byte[] bytes = report.ConfigurarPdfs();
            return File(bytes, "application/pdf");
        }
        public FileResult BaixarPDf()
        {
            EventoReport report = new EventoReport();
            byte[] bytes = report.ConfigurarPdfs();
            return File(bytes, "application/pdf", "ListaEventos.pdf");
        }

        public ActionResult List()
        {
            Evento.GerarLista(Session);
            return View(Session["ListaEve"] as List<Evento>);
        }
        public ActionResult Details(Evento evento, int id)
        {
            ViewBag.id = id;
            return View(Evento.Procurar(Session, id));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Evento());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento evento)
        {
            evento.Adicionar(Session);
            return RedirectToAction("List");

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Evento.Procurar(Session, id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Evento evento, int id)
        {
            Evento.Procurar(Session, id)?.Excluir(Session);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Evento.Procurar(Session, id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Evento evento)
        {
            evento.Editar(Session, id);
            return RedirectToAction("List");
        }
    }
}