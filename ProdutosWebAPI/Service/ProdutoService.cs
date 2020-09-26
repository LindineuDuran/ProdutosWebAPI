using ProdutosWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProdutosWebAPI.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly List<Produto> produtos;

        public ProdutoService()
        {
            this.produtos = new List<Produto>
            {
                new Produto() { Id = 1, Nome = "Suco de Tomate", Categoria = "Alimentos", Preco = 2.39},
                new Produto() { Id = 2, Nome = "Tablet Ipad", Categoria = "Informática", Preco = 1230.75},
                new Produto() { Id = 3, Nome = "NoteBook CCE", Categoria = "Informática", Preco = 1500.25},
                new Produto() { Id = 4, Nome = "Caneta", Categoria = "Papelaria", Preco = 0.75},
                new Produto() { Id = 5, Nome = "Caderno Espiral", Categoria = "Papelaria", Preco = 1.99}
            };
        }

        public void AddProduto(Produto item)
        {
            this.produtos.Add(item);
        }

        public void DeleteProduto(int id)
        {
            var model = this.produtos.Where(m => m.Id == id).FirstOrDefault();
            this.produtos.Remove(model);
        }

        public bool ProdutoExists(int id)
        {
            return this.produtos.Any(m => m.Id == id);
        }

        public Produto GetProduto(int id)
        {
            return this.produtos.Where(m => m.Id == id).FirstOrDefault();
        }

        public List<Produto> GetProdutos()
        {
            return this.produtos.ToList();
        }

        public void UpdateProduto(Produto item)
        {
            var model = this.produtos.Where(m => m.Id == item.Id).FirstOrDefault();

            model.Nome = item.Nome;
            model.Categoria = item.Categoria;
            model.Preco = item.Preco;
        }
    }
}
