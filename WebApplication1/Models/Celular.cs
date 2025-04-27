using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Celular
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O Novo é obrigatório.")]
        public bool Novo { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; }
        public string GetFormattedData()
        {
            return Data.ToString("dd/MM/yyyy");
        }
        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaCell"] != null)
            {
                if (((List<Celular>)session["ListaCell"]).Count > 0)
                {
                    return;
                }
            }
            var listaTelefones = new List<Celular>
            {
                new Celular {Id = 0, Numero = "1234567890", Marca = "Samsung", Novo = true, Data = new DateTime(2023, 5, 10) },
                new Celular {Id = 1, Numero = "987654-210", Marca = "Apple", Novo = false, Data = new DateTime(2022, 11, 20) },
                new Celular {Id = 2, Numero = "555-1234567", Marca = "Nokia", Novo = true, Data = new DateTime(2024, 1, 3) }
            };
            session.Remove("ListaCell");
            session.Add("ListaCell", listaTelefones);
        }
        public static Celular Procurar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCell"] != null)
            {
                return (session["ListaCell"] as List<Celular>).ElementAt(id);
            }

            return null;
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            var lista = session["ListaCell"] as List<Celular>;

            this.Id = lista.Count;

            lista.Add(this);
        }
        public void Excluir(HttpSessionStateBase session)
        {
            var lista = session["ListaCell"] as List<Celular>;
            lista.RemoveAll(a => a.Id == this.Id);
            Debug.WriteLine("Celular excluído. Lista agora contém " + lista.Count + " celular.");
        }
        public void Editar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCell"] != null)
            {
                var celular = Celular.Procurar(session, id);
                celular.Numero = this.Numero;
                celular.Marca = this.Marca;
                celular.Novo = this.Novo;
                celular.Data = this.Data;
            }
        }


    }
}
