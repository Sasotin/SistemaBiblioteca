namespace SistemaBiblioteca
{
    public class Livro
    {
        //atributos
        public string _tituloLivro { get; set; }
        public  string _autorLivro { get; set; }
        public  int _anoPublicacaoLivro { get; set; }
        public string _codigoLivro { get; set; }
        public bool _disponibilidade{ get; set; }
               
        //construtor
        public Livro(string tituloLivro, string autorLivro, int anoPublicacaoLivro, string codigoLivro, bool disponibilidade)
        {
            _tituloLivro = tituloLivro;
            _autorLivro = autorLivro;
            _anoPublicacaoLivro = anoPublicacaoLivro;
            _codigoLivro = codigoLivro;
            _disponibilidade = disponibilidade;
        }
    }
}
