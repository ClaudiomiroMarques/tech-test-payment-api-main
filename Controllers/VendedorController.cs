using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers;
[ApiController]
[Route("[controller]")]
public class VendedorController : ControllerBase
{
    private readonly ApiContext _logger;

    public VendedorController(ApiContext logger)
    {
        _logger = logger;
    }

    [HttpPost("CadastrarVendedor")]
    public IActionResult CadastrarVendedor(string Nome, string Cpf, string Email, string Telefone)
    {
        try
        {
            Vendedor vendedorACadastrar = new Vendedor(Nome, Cpf, Email, Telefone);
            _logger.Vendedores.Add(vendedorACadastrar);
            _logger.SaveChanges();
            return Ok(vendedorACadastrar);
        }
        catch (ArgumentException exception)
        { 
            return BadRequest(exception.Message); 
        }

    }

    [HttpGet("BuscarPor{Id}")]
    public IActionResult BuscarVendedor(int Id){
        var vendedor = _logger.Vendedores.Find(Id);
        if (vendedor == null){
            return NotFound();
        }

        return Ok(vendedor);
    }

    [HttpGet("ListarVendedores")]
    public IActionResult BuscarTodosVendedores(){
        var vendedores = _logger.Vendedores.ToList();
        return Ok(vendedores);
    }

    [HttpDelete("ExcluirVendedor{Id}")]
    public IActionResult ExlcuirVendedor(int Id){
        var vendedor = _logger.Vendedores.Find(Id);
        if (vendedor == null){
            return NotFound();
        }

        _logger.Vendedores.Remove(vendedor);
        _logger.SaveChanges();
        return NoContent();
    }
}
