using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
namespace WebApplication1.Reports
{
    public class EventoReport
    {
        public byte[] Prepare(List<Evento> eventos)
        {
            var cabecalhos = new[] { "Local", "Cep", "Endereço", "Cidade", "Data"};
            var dados = eventos.Select(c => new string[]
            {
                c.Local,
                c.cep,
                c.endereço,
                c.cidade,
                c.GetFormattedData(),
            }).ToList();
            var gerador = new PdfReportGenerator();
            return gerador.GerarRelatorio("Eventos", cabecalhos, dados);

        }
        public byte[] ConfigurarPdfs()
        {
            EventoReport eventoReport = new EventoReport();

            var session = HttpContext.Current.Session;
            List<Evento> lista = (List<Evento>)session["ListaEve"];
            byte[] bytes = eventoReport.Prepare(lista);
            return bytes;

        }
    }
}