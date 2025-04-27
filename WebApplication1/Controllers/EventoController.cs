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
using OfficeOpenXml;
using System.Drawing;
namespace WebApplication1.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadExcel()
        {
            var lista = Session["ListaEve"] as List<Evento>;
            if (lista == null || !lista.Any())
                return RedirectToAction("Listar");

            using (var pacote = new ExcelPackage())
            {
        
                var planilha = pacote.Workbook.Worksheets.Add("Evento");
                planilha.Cells[1, 1].Value = "Cep";
                planilha.Cells[1, 2].Value = "Endereço";
                planilha.Cells[1, 3].Value = "Cidade";
                planilha.Cells[1, 4].Value = "Local";
                planilha.Cells[1, 5].Value = "Data";

                planilha.Row(1).Style.Font.Bold = true;
                planilha.Row(1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                planilha.Row(1).Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                for (int i = 0; i < lista.Count; i++)
                {
                    var celular = lista[i];
                    planilha.Cells[i + 2, 1].Value = celular.cep;
                    planilha.Cells[i + 2, 2].Value = celular.endereço;
                    planilha.Cells[i + 2, 3].Value = celular.cidade;
                    planilha.Cells[i + 2, 4].Value = celular.Local;
                    planilha.Cells[i + 2, 5].Value = celular.Data.ToShortDateString();
                }

                planilha.Cells.AutoFitColumns();
                return File(pacote.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Alunos.xlsx");
            }
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
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    return View(Evento.Procurar(Session, id));
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Evento evento, int id)
        //{
        //    Evento.Procurar(Session, id)?.Excluir(Session);
        //    return RedirectToAction("List");
        //}
        public ActionResult DeleteAjax(int id)
        {
            var evento = Evento.Procurar(Session, id);
            if (evento != null)
            {
                evento.Excluir(Session);
                return Json(new { sucesso = true });
            }
            return new HttpStatusCodeResult(404, "Evento não encontrado");
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