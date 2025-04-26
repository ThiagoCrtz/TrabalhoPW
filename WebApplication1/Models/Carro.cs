using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Carro
    {
        [Required(ErrorMessage = "A placa é obrigatória")]
        [StringLength(7, ErrorMessage = "A placa deve ter 7 caracteres")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório")]
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
            listaVeiculos.Add(new Carro { Placa = "ABC1D23", Cor = "Preto", Data = new DateTime(2015, 5, 10) });
            listaVeiculos.Add(new Carro { Placa = "DEF2G45", Cor = "Branco", Data = new DateTime(2018, 8, 23) });
            listaVeiculos.Add(new Carro { Placa = "GHI3J67", Cor = "Azul", Data = new DateTime(2020, 3, 15) });


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
