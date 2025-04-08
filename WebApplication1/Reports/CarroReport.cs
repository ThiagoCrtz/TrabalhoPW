using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApplication1.Models;

namespace WebApplication1.Reports
{
    public class CarroReport
    {
        public byte[] Preapare(List<Carro> carros)
        {
            var cabecalhos = new[] { "Placa", "Cor", "Ano" };

            var dados = carros.Select(c => new string[]
            {
            c.Placa,
            c.Cor,
            c.Data.Year.ToString()
            }).ToList();

            var gerador = new PdfReportGenerator();
            return gerador.GerarRelatorio("Carros", cabecalhos, dados);
        }
        public byte[] ConfigurarPdfs()
        {
            CarroReport carroReport = new CarroReport();

            var session = HttpContext.Current.Session;
            List<Carro> listaveiculos = (List<Carro>)session["ListaCarro"];
            byte[] bytes = carroReport.Preapare(listaveiculos);

            return bytes;
        }
    }
    // Codigo inspirado no yt e com base nele eu criei uma pequena "regra de negocio" no arquivo "regra.cs"
    //public class CarroReport 
    //{
    //    int _colunas = 3;
    //    Document _document;
    //    Font _fontstyle;
    //    PdfPTable _table = new PdfPTable(3);
    //    PdfPCell _pdfcell;
    //    MemoryStream _memoryStream = new MemoryStream();
    //    List<Carro> _carro = new List<Carro>();


    //    public byte[] Preapare(List<Carro> carros)
    //    {
    //        _carro = carros ?? new List<Carro>();

    //        _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
    //        _document.SetPageSize(PageSize.A4);
    //        _document.SetMargins(20f, 20f, 20f, 20f);
    //        _table = new PdfPTable(3); 
    //        _table.WidthPercentage = 100;   
    //        _table.HorizontalAlignment = Element.ALIGN_LEFT;
    //        _fontstyle = FontFactory.GetFont("Tahoma", 8f, Font.BOLD);
    //        PdfWriter.GetInstance(_document, _memoryStream);
    //        _document.Open();

    //        _table.SetWidths(new float[] { 20f, 150f, 100f });

    //        ReportHeader();
    //        ReportBody();
    //        _table.HeaderRows = 2;  
    //        _document.Add(_table);
    //        _document.Close();
    //        return _memoryStream.ToArray();
    //    }
    //    private void ReportHeader() {
    //        _fontstyle = FontFactory.GetFont("Tahoma",11f,1);
    //        _pdfcell = new PdfPCell(new Phrase("Carros", _fontstyle));
    //        _pdfcell.Colspan = _colunas;
    //        _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        _pdfcell.Border =0;
    //        _pdfcell.BackgroundColor = BaseColor.WHITE;
    //        _pdfcell.ExtraParagraphSpace = 0;
    //        _table.AddCell(_pdfcell);
    //        _table.CompleteRow();




    //    }

    //    private void ReportBody()
    //    {
    //        _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
    //        _pdfcell = new PdfPCell(new Phrase("Placa", _fontstyle));
    //        _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        _pdfcell.VerticalAlignment  = Element.ALIGN_MIDDLE;
    //        _pdfcell.BackgroundColor = BaseColor.LIGHT_GRAY;
    //        _table.AddCell(_pdfcell); 

    //        _pdfcell = new PdfPCell(new Phrase("Cor", _fontstyle));
    //        _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        _pdfcell.VerticalAlignment  = Element.ALIGN_MIDDLE;
    //        _pdfcell.BackgroundColor = BaseColor.LIGHT_GRAY;
    //        _table.AddCell(_pdfcell);

    //        _pdfcell = new PdfPCell(new Phrase("Ano", _fontstyle));
    //        _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        _pdfcell.VerticalAlignment  = Element.ALIGN_MIDDLE;
    //        _pdfcell.BackgroundColor = BaseColor.LIGHT_GRAY;
    //        _table.AddCell(_pdfcell);
    //        _table.CompleteRow();


    //        _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
    //        foreach (Carro carro in _carro)
    //        {

    //            _pdfcell = new PdfPCell(new Phrase(carro.Placa, _fontstyle));
    //            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
    //            _pdfcell.BackgroundColor = BaseColor.WHITE;
    //            _table.AddCell(_pdfcell);

    //            _pdfcell = new PdfPCell(new Phrase(carro.Cor, _fontstyle));
    //            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
    //            _pdfcell.BackgroundColor = BaseColor.WHITE;
    //            _table.AddCell(_pdfcell);

    //            _pdfcell = new PdfPCell(new Phrase(carro.Data.ToString(), _fontstyle));
    //            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
    //            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
    //            _pdfcell.BackgroundColor = BaseColor.WHITE;
    //            _table.AddCell(_pdfcell);
    //            _table.CompleteRow();
    //        }

    //    }

    //}
}