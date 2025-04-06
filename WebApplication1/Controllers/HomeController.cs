using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //so pra testar a ferramenta
        //public ActionResult pdf()
        //{

        //    FileStream fs = new FileStream("c://pdf/report.pdf", FileMode.Create);
        //    Document document = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);
        //    PdfWriter pw = PdfWriter.GetInstance(document, fs);

        //    document.Open();
        //    document.Add(new Paragraph("Olá"));
        //    document.Close();

        //    return null;
        //}

    }
}