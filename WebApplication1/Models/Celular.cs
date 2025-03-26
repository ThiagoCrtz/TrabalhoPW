using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Celular
    {
        public string Numero { get; set; }
        public string Marca { get; set; }
        public bool Novo { get; set; }

        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaCell"] != null)
            {
                if (((List<Celular>)session["ListaCell"]).Count > 0)
                {
                    return;
                }
            }
            var listaTelefones = new List<Celular>();
            {
                listaTelefones.Add(new Celular { Numero = "1234567890", Marca = "Samsung", Novo = true });
                listaTelefones.Add(new Celular { Numero = "987654-210", Marca = "Apple", Novo = false });
                listaTelefones.Add(new Celular { Numero = "555-1234567", Marca = "Nokia", Novo = true });
            }
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
            if (session["ListaCell"] != null)
            {
                (session["ListaCell"] as List<Celular>).Add(this);
            }
        }
        public void Excluir(HttpSessionStateBase session)
        {
            if (session["ListaCell"] != null)
            {
                (session["ListaCell"] as List<Celular>).Remove(this);
            }
        }
        public void Editar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCell"] != null)
            {
                var celular = Celular.Procurar(session, id);
                celular.Numero = this.Numero;
                celular.Marca = this.Marca;
                celular.Novo = this.Novo;
            }
        }


    }
}
