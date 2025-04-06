using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApplication1.Models;
using WebApplication1.Reports;

namespace WebApplication1.Controllers
{
    public class CarroController : Controller
    {
        public ActionResult Report(Carro carro)
        {
            CarroReport carroReport = new CarroReport();
            Carro.GerarLista(Session);

            List<Carro> lista = (List<Carro>)Session["ListaCarro"];
            byte[] bytes = carroReport.Preapare(lista);
            return File(bytes, "application/pdf");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista()
        {
            Carro.GerarLista(Session);
            return View(Session["ListaCarro" ] as List<Carro>);
        }

        public ActionResult Exibir(int id) {
            ViewBag.id = id;
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Carro());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carro carro)
        {
            carro.Adicionar(Session);
            return RedirectToAction("Lista");
        }      
   
        public ActionResult Delete(int id)
        {
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Carro carro, int id)
        {
            Carro.Procurar(Session, id)?.Excluir(Session);
            return RedirectToAction("Lista");
        }
        public ActionResult Editar(int id)
        {
            return View(Carro.Procurar(Session, id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Carro carro)
        {
            carro.Editar(Session, id);
            return RedirectToAction("Lista");
        }


    }
}