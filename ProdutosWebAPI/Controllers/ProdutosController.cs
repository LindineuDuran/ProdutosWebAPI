using Microsoft.AspNetCore.Mvc;
using ProdutosWebAPI.Models;
using ProdutosWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProdutosWebAPI.Controllers
{
    [Route("produtos")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoService service;
        public ProdutosController(IProdutoService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var model = service.GetProdutos();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }
        [HttpGet("{id}", Name = "GetProduto")]
        public IActionResult Get(int id)
        {
            var model = service.GetProduto(id);
            if (model == null)
                return NotFound();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }
        [HttpPost]
        public IActionResult Create([FromBody]ProdutoInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();
            var model = ToDomainModel(inputModel);
            service.AddProduto(model);
            var outputModel = ToOutputModel(model);
            return CreatedAtRoute("GetProduto", new { id = outputModel.Id }, outputModel);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ProdutoInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();
            if (!service.ProdutoExists(id))
                return NotFound();
            var model = ToDomainModel(inputModel);
            service.UpdateProduto(model);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.ProdutoExists(id))
                return NotFound();
            service.DeleteProduto(id);
            return NoContent();
        }
        //--------------------------------------------------
        //Mapeamentos : modelos envia/recebe dados via API
        private ProdutoOutputModel ToOutputModel(Produto model)
        {
            return new ProdutoOutputModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Categoria = model.Categoria,
                Preco = model.Preco,
                UltimoAcesso = DateTime.Now
            };
        }
        private List<ProdutoOutputModel> ToOutputModel(List<Produto> model)
        {
            return model.Select(item => ToOutputModel(item)).ToList();
        }
        private Produto ToDomainModel(ProdutoInputModel inputModel)
        {
            return new Produto
            {
                Id = inputModel.Id,
                Nome = inputModel.Nome,
                Categoria = inputModel.Categoria,
                Preco = inputModel.Preco
            };
        }
        private ProdutoInputModel ToInputModel(Produto model)
        {
            return new ProdutoInputModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Categoria = model.Categoria,
                Preco = model.Preco
            };
        }
    }
}
