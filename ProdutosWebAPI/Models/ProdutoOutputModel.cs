﻿using System;

namespace ProdutosWebAPI.Models
{
    public class ProdutoOutputModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public double Preco { get; set; }
        public DateTime UltimoAcesso { get; set; }
    }
}
