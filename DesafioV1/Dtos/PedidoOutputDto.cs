namespace DesafioV1.Dtos {
    public class PedidoOutputDto {
        public int Pedido_Id { get; set; }
        public List<CaixaOutputDto> Caixas { get; set; } = new();
    }

    public class CaixaOutputDto {
        public string? CaixaId { get; set; }
        public List<string> Produtos { get; set; } = new();
        public string? Observacao { get; set; }
    }

}
