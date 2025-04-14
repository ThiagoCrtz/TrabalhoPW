using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Evento
    {
        public string cep { get; set; }
        public string endereço { get; set; }
        public string cidade { get; set; }

        public string Local { get; set; }
        public DateTime Data { get; set; }
        public string GetFormattedData()
        {
            return Data.ToString("dd/MM/yyyy"); 
        }
        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaEve"] != null)
            {
                if (((List<Evento>)session["ListaEve"]).Count > 0)
                {
                    return;
                }
            }
            var listaEventos = new List<Evento>();
            {
                listaEventos.Add(new Evento
                {
                    cep = "01000-000",
                    endereço = "Av. Paulista, 1000",
                    cidade = "São Paulo",
                    Local = "São Paulo",
                    Data = new DateTime(2023, 5, 10)
                });

                listaEventos.Add(new Evento
                {
                    cep = "20000-000",
                    endereço = "Praia de Copacabana, 50",
                    cidade = "Rio de Janeiro",
                    Local = "Rio de Janeiro",
                    Data = new DateTime(2023, 6, 15)
                });

                listaEventos.Add(new Evento
                {
                    cep = "30000-000",
                    endereço = "Praça da Liberdade, 80",
                    cidade = "Belo Horizonte",
                    Local = "Belo Horizonte",
                    Data = new DateTime(2023, 7, 20)
                });
            }
            session.Remove("ListaEve");
            session.Add("ListaEve", listaEventos);
        }
        public static Evento Procurar(HttpSessionStateBase session, int id)
        {
            if (session["ListaEve"] != null)
            {
                return (session["ListaEve"] as List<Evento>).ElementAt(id);
            }

            return null;
        }


        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaEve"] != null)
            {
                (session["ListaEve"] as List<Evento>).Add(this);
            }
        }
        public void Excluir(HttpSessionStateBase session)
        {
            if (session["ListaEve"] != null)
            {
                (session["ListaEve"] as List<Evento>).Remove(this);
            }
        }
        public void Editar(HttpSessionStateBase session, int id)
        {   
            if (session["ListaEve"] != null)
            {
                var evento = Evento.Procurar(session, id);
                evento.Data = this.Data;
                evento.Local = this.Local;
                evento.cep = this.cep;
                evento.endereço = this.endereço;
                evento.cidade = this.cidade;
            }
        }
    }
}
