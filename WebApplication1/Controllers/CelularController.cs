using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using WebApplication1.Models;
using WebApplication1.Reports;

namespace WebApplication1.Controllers
{
    public class CelularController : Controller
    {
        // GET: Celular
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadExcel()
        {
            var lista = Session["ListaCell"] as List<Celular>;
            if (lista == null || !lista.Any())
                return RedirectToAction("Listar");

            using (var pacote = new ExcelPackage())
            {
                var planilha = pacote.Workbook.Worksheets.Add("Celular");
                planilha.Cells[1, 1].Value = "Numero";
                planilha.Cells[1, 2].Value = "Marca";
                planilha.Cells[1, 3].Value = "Novo";
                planilha.Cells[1, 4].Value = "Data";

                planilha.Row(1).Style.Font.Bold = true;
                planilha.Row(1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                planilha.Row(1).Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                for (int i = 0; i < lista.Count; i++)
                {
                    var celular = lista[i];
                    planilha.Cells[i + 2, 1].Value = celular.Numero;
                    planilha.Cells[i + 2, 2].Value = celular.Marca;
                    planilha.Cells[i + 2, 3].Value = celular.Novo;
                    planilha.Cells[i + 2, 4].Value = celular.Data.ToShortDateString();
                }

                planilha.Cells.AutoFitColumns();
                return File(pacote.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Celular.xlsx");
            }
        }
        public ActionResult Report(Celular celular)
        {
            CelularReport report = new CelularReport();
            byte[] bytes = report.ConfigurarPdfs();
            return File(bytes, "application/pdf");
        }
        public FileResult BaixarPDf()
        {
            CelularReport report = new CelularReport();
            byte[] bytes = report.ConfigurarPdfs();
            return File(bytes, "application/pdf", "ListaCelulares.pdf");
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