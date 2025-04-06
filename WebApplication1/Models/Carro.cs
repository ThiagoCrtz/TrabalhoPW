using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace WebApplication1.Models
{
    public class Carro
    {
        public string Placa { get; set; }
        public string Cor { get; set; }
        public DateTime Data { get; set; }
        public string GetFormattedData()
        {
            return Data.ToString("yyyy");
        }
        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                if(((List<Carro>)session["ListaCarro"]).Count >0)
                {
                    return;
                }
            }
            var listaVeiculos = new List<Carro>();
            listaVeiculos.Add(new Carro { Placa = "ABC-1234", Cor = "Preto", Data = new DateTime(2015, 5, 10) });
            listaVeiculos.Add(new Carro { Placa = "DEF-5678",  Cor = "Branco", Data = new DateTime(2018, 8, 23) });
            listaVeiculos.Add(new Carro { Placa = "GHI-9012", Cor = "Azul", Data = new DateTime(2020, 3, 15) });

            session.Remove("ListaCarro");
            session.Add("ListaCarro", listaVeiculos);
        }
        public static List<Carro> ObterLista(HttpSessionStateBase session)
        {
            return (List<Carro>)session["ListaCarro"];
        }

        public static Carro Procurar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCarro"] != null)
            {
                return (session["ListaCarro"] as List<Carro>).ElementAt(id);
            }

            return null;
        }
        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                (session["ListaCarro"] as List<Carro>).Add(this);
            }
        }
        public void Excluir(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                (session["ListaCarro"] as List<Carro>).Remove(this);
            }
        }
        public void Editar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCarro"] != null)
            {
                var carro = Carro.Procurar(session, id);
                carro.Placa = this.Placa;
                carro.Data = this.Data;
                carro.Cor = this.Cor;
            }
        }

    }
}
