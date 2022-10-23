namespace Biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ISBN { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Autor { get; set; }
        public byte[] ImagemCapa  { get; set; }
        public int EditoraId { get; set; }
        public Editora Editora { get; set; }
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}
