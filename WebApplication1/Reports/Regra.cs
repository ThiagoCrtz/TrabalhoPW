using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebApplication1.Models;

namespace WebApplication1.Reports
{
    public class PdfReportGenerator
    {
        private int _colunas;
        private Font _fontstyle;
        private PdfPTable _table;
        private Document _document;
        private MemoryStream _memoryStream;

        public byte[] GerarRelatorio(string titulo, string[] cabecalhos, List<string[]> dados)
        {
            _colunas = cabecalhos.Length;
            _table = new PdfPTable(_colunas);
            _table.WidthPercentage = 100;
            _table.HorizontalAlignment = Element.ALIGN_LEFT;

            _memoryStream = new MemoryStream();
            _document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            AdicionarTitulo(titulo);
            AdicionarCabecalhos(cabecalhos);
            AdicionarLinhas(dados);

            _document.Add(_table);
            _document.Close();

            return _memoryStream.ToArray();
        }

        private void AdicionarTitulo(string titulo)
        {
            _fontstyle = FontFactory.GetFont("Tahoma", 11f, Font.BOLD);
            var cell = new PdfPCell(new Phrase(titulo, _fontstyle))
            {
                Colspan = _colunas,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                BackgroundColor = BaseColor.WHITE,
            };
            _table.AddCell(cell);
            _table.CompleteRow();
        }

        private void AdicionarCabecalhos(string[] cabecalhos)
        {
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, Font.BOLD);
            foreach (var header in cabecalhos)
            {
                var cell = new PdfPCell(new Phrase(header, _fontstyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.LIGHT_GRAY
                };
                _table.AddCell(cell);
            }
            _table.CompleteRow();
        }

        private void AdicionarLinhas(List<string[]> dados)
        {
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, Font.NORMAL);

            foreach (var linha in dados)
            {
                foreach (var coluna in linha)
                {
                    var cell = new PdfPCell(new Phrase(coluna, _fontstyle))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        BackgroundColor = BaseColor.WHITE
                    };
                    _table.AddCell(cell);
                }
                _table.CompleteRow();
            }
        }
    }



}