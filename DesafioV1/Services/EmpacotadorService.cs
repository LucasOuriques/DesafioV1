using DesafioV1.Data;
using DesafioV1.Dtos;
using DesafioV1.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DesafioV1.Services {
    public class EmpacotadorService {
        private readonly AppDbContext _context;

        public EmpacotadorService(AppDbContext context) {
            _context = context;
        }

        public async Task<List<PedidoOutputDto>> EmpacotarPedidosAsync(List<Pedido> pedidos) {
            var caixasDisponiveis = await _context.Caixa.OrderBy(c => c.Altura * c.Largura * c.Comprimento).ToListAsync();
            caixasDisponiveis = caixasDisponiveis.OrderBy(c => c.Volume).ToList();

            var resultado = new List<PedidoOutputDto>();

            foreach (var pedido in pedidos) {
                var caixasDoPedido = new List<CaixaOutputDto>();
                var produtosRestantes = new List<Produto>(pedido.Produtos);

                while (produtosRestantes.Any()) {
                    bool encaixou = false;

                    foreach (var caixa in caixasDisponiveis) {
                        var produtosNaCaixa = new List<Produto>();
                        double volumeRestante = caixa.Volume;

                        foreach (var produto in produtosRestantes.ToList()) {
                            if (ProdutoEncaixa(produto, caixa) && produto.Volume <= volumeRestante) {
                                produtosNaCaixa.Add(produto);
                                volumeRestante -= produto.Volume;
                                produtosRestantes.Remove(produto);
                            }
                        }

                        if (produtosNaCaixa.Any()) {
                            caixasDoPedido.Add(new CaixaOutputDto {
                                CaixaId = caixa.Nome,
                                Produtos = produtosNaCaixa.Select(p => p.Produto_Id).ToList()
                            });
                            encaixou = true;
                            break;
                        }
                    }

                    if (!encaixou) {
                        // produto que não encaixa em nenhuma caixa
                        var produto = produtosRestantes.First();
                        caixasDoPedido.Add(new CaixaOutputDto {
                            CaixaId = null,
                            Produtos = new List<string> { produto.Produto_Id },
                            Observacao = "Produto não cabe em nenhuma caixa disponível."
                        });
                        produtosRestantes.Remove(produto);
                    }
                }

                resultado.Add(new PedidoOutputDto {
                    Pedido_Id = pedido.Pedido_Id,
                    Caixas = caixasDoPedido
                });
            }

            return resultado;
        }

        private bool ProdutoEncaixa(Produto produto, Caixa caixa) {
            var dimProduto = new[] { produto.Altura, produto.Largura, produto.Comprimento }.OrderBy(x => x).ToArray();
            var dimCaixa = new[] { caixa.Altura, caixa.Largura, caixa.Comprimento }.OrderBy(x => x).ToArray();
            return dimProduto[0] <= dimCaixa[0] &&
                   dimProduto[1] <= dimCaixa[1] &&
                   dimProduto[2] <= dimCaixa[2];
        }
    }
}
