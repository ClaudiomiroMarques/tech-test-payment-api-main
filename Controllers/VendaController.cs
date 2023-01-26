using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [Route("[controller]")]
    public class VendaController : Controller
    {
        private readonly ApiContext _logger;

        public VendaController(ApiContext logger)
        {
            _logger = logger;
        }
    
        [HttpPost("CadastrarVenda")]
        public IActionResult CadastrarVenda(int IdVendedor, List<int> IdProdutos){
            Vendedor vendedor = _logger.Vendedores.Find(IdVendedor);
            if (vendedor == null){
                return NotFound("Vendedor não encontrado");
            }
            else if(IdProdutos.Count == 0){
                return BadRequest("Favor informar um produto");
            }

            List<Produto> produtos = new List<Produto>();
            
            foreach (int Id in IdProdutos){
                Produto produto = _logger.Produtos.Find(Id);
                if (produto == null){
                    return NotFound("Produto não encontrado");
                }
                produtos.Add(produto);
            }

            Venda venda = new Venda(vendedor.IdVendedor, produtos);

            _logger.Vendas.Add(venda);
            _logger.SaveChanges();
            return Ok(venda);
        }

        [HttpGet("BuscarVenda{Id}")]
        public IActionResult BuscarVenda(int Id){
            var venda = _logger.Vendas.Find(Id);
            if (venda == null){
                return NotFound("Venda não encontrada");
            }
            return Ok(venda);
        }

        [HttpGet("ListarTodasAsVendas")]
        public IActionResult BuscarTodasAsVendas(){
            return Ok(_logger.Vendas.ToList());
        }

        [HttpPatch("AtualizarStatus")]
        public IActionResult AtualizarStatus(int Id, StatusVenda status){
            string opcao = status.ToString();
            var venda = _logger.Vendas.Find(Id);
            if (venda == null){
                return NotFound("Venda não encontrada");
            }
            else{
/*
A atualização de status deve permitir somente as seguintes transições:

De: Aguardando pagamento Para: Pagamento Aprovado

De: Aguardando pagamento Para: Cancelada

De: Pagamento Aprovado Para: Enviado para Transportadora

De: Pagamento Aprovado Para: Cancelada

De: Enviado para Transportador. Para: Entregue

*/
                switch (venda.Status){
                    case "AguardandoPagamento":
                        if (!(opcao == "PagamentoAprovado" || opcao == "Cancelada"))
                            { 
                            return BadRequest("'PagamentoAprovado' ou 'Cancelada'"); 
                            }
                        break;

                    case "PagamentoAprovado":
                        if (!(opcao == "EnviadoParaTransportadora" || opcao == "Cancelada"))
                            { 
                            return BadRequest("'EnviadoParaTransportadora' ou 'Cancelada'");
                            }
                        break;

                    case "EnviadoParaTransportadora":
                        if (!(opcao == "Entregue"))
                            { 
                            return BadRequest("Entregue");
                            }
                        break;
                }
            }
            venda.Status = opcao;
            _logger.Vendas.Update(venda);
            _logger.SaveChanges();
            return Ok(venda);
        }

        [HttpDelete("ExcluirVenda{Id}")]
        public IActionResult ExcluirVenda(int Id){
            var venda = _logger.Vendas.Find(Id);
            if (venda == null){
                return BadRequest("Venda Não Encontrada");
            }

            _logger.Vendas.Remove(venda);
            _logger.SaveChanges();
            return NoContent();
        }
    }
}