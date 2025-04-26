using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Org.BouncyCastle.Utilities;
using WebApplication1.Models;

namespace WebApplication1.Reports
{
    public class CelularReport
    {
        public byte[] Prepare(List<Celular> celulares)
        {
            var cabecalhos = new[] {"Número", "Marca", "Novo", "Data de fabricação"};
            var dados = celulares.Select(c => new string[]
            {
                c.Numero,
                c.Marca,
                c.Novo.ToString(),
                c.GetFormattedData(),
            }).ToList();
            var gerador = new PdfReportGenerator();
            return gerador.GerarRelatorio("Celulares", cabecalhos, dados);

        }
        public byte[] ConfigurarPdfs()
        {
            CelularReport celularReport = new CelularReport();

            var session = HttpContext.Current.Session;
            List<Celular> listaTelefones = (List<Celular>)session["ListaCell"];
            byte[] bytes = celularReport.Prepare(listaTelefones);

            return bytes;
        }
  
    }
}
