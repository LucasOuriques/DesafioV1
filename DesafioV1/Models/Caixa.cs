namespace DesafioV1.Models {
    public class Caixa {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }

        public double Volume => Altura * Largura * Comprimento;
    }
}