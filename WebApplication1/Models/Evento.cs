using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Evento
    {
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
                listaEventos.Add(new Evento { Local = "São Paulo", Data = new DateTime(2023, 5, 10) });
                listaEventos.Add(new Evento { Local = "Rio de Janeiro", Data = new DateTime(2023, 6, 15) });
                listaEventos.Add(new Evento { Local = "Belo Horizonte", Data = new DateTime(2023, 7, 20) });
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
            }
        }
    }
}
