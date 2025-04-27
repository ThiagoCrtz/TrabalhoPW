using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O cep é obrigatório.")]
        public string cep { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string endereço { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "O local é obrigatório.")]
        public string Local { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
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
                    Id = 0,
                    cep = "01000-000",
                    endereço = "Av. Paulista, 1000",
                    cidade = "São Paulo",
                    Local = "São Paulo",
                    Data = new DateTime(2023, 5, 10)
                });

                listaEventos.Add(new Evento
                {
                    Id = 1,
                    cep = "20000-000",
                    endereço = "Praia de Copacabana, 50",
                    cidade = "Rio de Janeiro",
                    Local = "Rio de Janeiro",
                    Data = new DateTime(2023, 6, 15)
                });

                listaEventos.Add(new Evento
                {
                    Id = 2,
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
            var lista = session["ListaEve"] as List<Evento>;

            this.Id = lista.Count;

            lista.Add(this);
        }
        public void Excluir(HttpSessionStateBase session)
        {
            var lista = session["ListaEve"] as List<Evento>;
            lista.RemoveAll(a => a.Id == this.Id);
            Debug.WriteLine("Evento excluído. Lista agora contém " + lista.Count + " Evento.");
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
