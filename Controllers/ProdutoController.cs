using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly ApiContext _logger;

        public ProdutoController(ApiContext logger)
        {
            _logger = logger;
        }
    
        [HttpPost("CadastrarProduto")]
        public IActionResult CadastrarProduto(String Nome){
            Produto produto = new Produto(Nome);

            _logger.Produtos.Add(produto);
            _logger.SaveChanges();
            return Ok(produto);
        }

        [HttpGet("BuscarProduto{Id}")]
        public IActionResult BuscarProduto(int Id){
            var produto = _logger.Produtos.Find(Id);
            if (produto == null){
                return NotFound("Produto não Encontrado");
            }
            return Ok(produto);
        }

        [HttpGet("ListarTodosOsProdutos")]
        public IActionResult ListarTodosOsProdutos(){
            return Ok(_logger.Produtos.ToList());
        }

        [HttpPut("AtualizarProduto")]
        public IActionResult Produto(int Id, string Nome){
            var produto = _logger.Produtos.Find(Id);
            if (produto == null){
                return NotFound("Produto não Encontrado");
            }
            produto.Nome = Nome;
            _logger.Produtos.Update(produto);
            _logger.SaveChanges();
            return Ok(produto);
        }

        [HttpDelete("ExcluirProduto{Id}")]
        public IActionResult ExcluirProduto(int Id){
            var produto = _logger.Produtos.Find(Id);
            if (produto == null){
                return NotFound("Produto não Encontrado");
            }

            _logger.Produtos.Remove(produto);
            _logger.SaveChanges();
            return NoContent();
        }
    }
}