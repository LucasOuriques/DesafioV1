namespace DesafioV1.Models {
    public class Produto {
        public string Produto_Id { get; set; } = null!;
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }

        public int Volume => Altura * Largura * Comprimento;
    }

}