using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using WebApplication1.Models;
using WebApplication1.Reports;

namespace WebApplication1.Controllers
{
    public class CarroController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadExcel()
        {
            var lista = Session["ListaCarro"] as List<Carro>;
            if (lista == null || !lista.Any())
                return RedirectToAction("Listar");

            using (var pacote = new ExcelPackage())
            {
                var planilha = pacote.Workbook.Worksheets.Add("Carro");
                planilha.Cells[1, 1].Value = "Placa";
                planilha.Cells[1, 2].Value = "Cor";
                planilha.Cells[1, 3].Value = "Data";

                planilha.Row(1).Style.Font.Bold = true;
                planilha.Row(1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                planilha.Row(1).Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                for (int i = 0; i < lista.Count; i++)
                {
                    var carro = lista[i];
                    planilha.Cells[i + 2, 1].Value = carro.Placa;
                    planilha.Cells[i + 2, 2].Value = carro.Cor;
                    planilha.Cells[i + 2, 3].Value = carro.Data.ToShortDateString();
                }

                planilha.Cells.AutoFitColumns();
                return File(pacote.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Carro.xlsx");
            }
        }

        public ActionResult ReportPDf(Carro carro)
        {
            CarroReport report = new CarroReport();
            byte[] bytes = report.ConfigurarPdfs();
            return File(bytes, "application/pdf");
        }
        public FileResult BaixarPDf()
        {
            CarroReport report = new CarroReport();
            byte[] bytes = report.ConfigurarPdfs();
            return File(bytes, "application/pdf", "ListaCarros.pdf");
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