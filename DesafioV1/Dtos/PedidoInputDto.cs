namespace DesafioV1.Dtos {
    public class PedidoInputDto {
        public List<PedidoDto> Pedidos { get; set; }
    }

    public class PedidoDto {
        public int Pedido_Id { get; set; }
        public List<ProdutoDto> Produtos { get; set; }
    }

    public class ProdutoDto {
        public string Produto_Id { get; set; }
        public DimensoesDto Dimensoes { get; set; }
    }

    public class DimensoesDto {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
    }
}
