using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Carro
    {
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }

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
            listaVeiculos.Add(new Carro { Placa = "ABC-1234", Ano = 2015, Cor = "Preto" });
            listaVeiculos.Add(new Carro { Placa = "DEF-5678", Ano = 2018, Cor = "Branco" });
            listaVeiculos.Add(new Carro { Placa = "GHI-9012", Ano = 2020, Cor = "Azul" });
            session.Remove("ListaCarro");
            session.Add("ListaCarro", listaVeiculos);
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
                carro.Ano = this.Ano;
                carro.Cor = this.Cor;
            }
        }

    }
}
