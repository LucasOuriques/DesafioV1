using DesafioV1.Dtos;
using DesafioV1.Models;
using DesafioV1.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioV1.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase {
        private readonly EmpacotadorService _empacotador;

        public PedidoController(EmpacotadorService empacotador) {
            _empacotador = empacotador;
        }

        [HttpPost("empacotar")]
        public async Task<IActionResult> EmpacotarPedidos([FromBody] PedidoInputDto input) {
            // Conversão de DTO para Model
            var pedidosModel = input.Pedidos.Select(p => new Pedido {
                Pedido_Id = p.Pedido_Id,
                Produtos = p.Produtos.Select(prod => new Produto {
                    Produto_Id = prod.Produto_Id,
                    Altura = prod.Dimensoes.Altura,
                    Largura = prod.Dimensoes.Largura,
                    Comprimento = prod.Dimensoes.Comprimento
                }).ToList()
            }).ToList();

            var resultado = await _empacotador.EmpacotarPedidosAsync(pedidosModel);
            return Ok(new { pedidos = resultado });
        }
    }

}


