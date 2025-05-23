namespace SistemaBiblioteca
{
    public class Servicos
    {
        static string separacao = "-------------------------------------";
        private static List<Livro> listaDeLivros = new List<Livro>();  

        private static bool ExistemLivrosNaLista()
        {
            if (listaDeLivros.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão existem livros cadastrados.\n");
                Console.ResetColor();
                return false;
            }
            return true;
        }
        
        static string GerarCodigoLivro() //gera o codigo do livro
        {
            Random random = new Random();
            string parte1 = random.Next(1000, 9999).ToString();
            string parte2 = random.Next(1000, 9999).ToString();
            string parte3 = DateTime.Now.ToString("MMyy");
            return $" {parte1}-{parte2}-{parte3}";
        }

        public static void AdicionarLivro()
        {
            string tituloLivro;
            do
            {
                Console.Write("Titulo do livro: \n");
                tituloLivro = Console.ReadLine();
                if (string.IsNullOrEmpty(tituloLivro))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O título do livro não pode ser vazio.\n");
                    Console.ResetColor();
                }
            }
            while (string.IsNullOrEmpty(tituloLivro));

            string autorLivro;
            do
            {
                Console.Write("Autor do livro: \n");
                autorLivro = Console.ReadLine();
                if (string.IsNullOrEmpty(autorLivro))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O autor do livro não pode ser vazio.\n");
                    Console.ResetColor();
                }
            }
            while (string.IsNullOrEmpty(autorLivro));

            
            int anoPublicacaoLivro; 
            while (true)
            {
                Console.Write("Ano de publicação do livro: \n");
                string entradaAnoPublicacao = Console.ReadLine(); //variavel para receber o input do usuario
                if (int.TryParse(entradaAnoPublicacao, out anoPublicacaoLivro) && anoPublicacaoLivro > 0) //tenta conveter a string para int e verifica se o valor atribuido é válido
                {
                    break; //quebra o loop
                }
                else //caso o if falhe
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O ano de publicação deve ser um número maior que zero.\n");
                    Console.ResetColor();
                }                    
            }

            //atribui o codigo ao livro
            string codigoLivro = GerarCodigoLivro();
            Console.WriteLine($"Código do livro: {codigoLivro}");

            try
            {
                Livro novoLivro = new Livro(tituloLivro, autorLivro, anoPublicacaoLivro, codigoLivro);
                listaDeLivros.Add(novoLivro);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLivro adicionado com sucesso!\n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao adicionar livro: {ex.Message}");
                Console.ResetColor();
            }

        }

        public static void RemoverLivro()
        {
            if (!ExistemLivrosNaLista())
            {
                return;
            }

            Console.Write("Digite o nome ou código do livro que deseja remover: \n"); //input do usuario
            Console.WriteLine(separacao);
            string termoBusca = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(termoBusca)) //verifica se o input do usuario é  válido
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nome/código não pode estar vazio para realizar a busca!\n");
                Console.ResetColor();
                return;
            }

            //busca o livro ignorando caixa alta ou espaços
            Livro livroEncontrado = listaDeLivros.Find(livro => livro._tituloLivro.Equals(termoBusca, StringComparison.OrdinalIgnoreCase) || livro._codigoLivro.Replace(" ", "").Replace("-", "").Equals(termoBusca.Replace(" ", "").Replace("-", ""),StringComparison.OrdinalIgnoreCase));

            if (livroEncontrado != null) //verifica se o livro existe para que possa ser removido
            {
                listaDeLivros.Remove(livroEncontrado);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nLivro {livroEncontrado._tituloLivro}, código{livroEncontrado._codigoLivro} removido com sucesso!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nLivro não encontrado!");
                Console.ResetColor();
            }
        }

        public static void ListarLivros()
        {
            if (!ExistemLivrosNaLista())
            {
                return;
            }
            foreach (Livro livro in listaDeLivros) //percorre a lista de livros
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"""
                    TÍTULO: {livro._tituloLivro}
                    AUTOR: {livro._autorLivro}
                    ANO DA PUBLICAÇÃO: {livro._anoPublicacaoLivro}
                    CÓDIGO DO LIVRO: {livro._codigoLivro}
                    """);
                Console.ResetColor();
                Console.WriteLine(separacao);
            }
        }

        public static void PesquisarLivro()
        {
            Console.Write("Digite o nome ou código do livro que deseja pesquisar: \n"); //input do usuario           
            string termoBusca = Console.ReadLine()?.Trim();
            Console.WriteLine(separacao);

            if (string.IsNullOrWhiteSpace(termoBusca)) //verifica se o input do usuario é  válido
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nome/código não pode estar vazio para realizar a busca!\n");
                Console.ResetColor();
                return;
            }

            //busca o livro ignorando caixa alta ou espaços
            Livro livroEncontrado = listaDeLivros.Find(livro => livro._tituloLivro.Equals(termoBusca, StringComparison.OrdinalIgnoreCase) || livro._codigoLivro.Replace(" ", "").Replace("-", "").Equals(termoBusca.Replace(" ", "").Replace("-", ""), StringComparison.OrdinalIgnoreCase));
            
            //se o livro existir, exibe os dados
            if(livroEncontrado != null) 
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"""
                    TÍTULO: {livroEncontrado._tituloLivro}
                    AUTOR: {livroEncontrado._autorLivro}
                    ANO DA PUBLICAÇÃO: {livroEncontrado._anoPublicacaoLivro}
                    CÓDIGO DO LIVRO: {livroEncontrado._codigoLivro}
                    """);
                Console.ResetColor();
                Console.WriteLine(separacao);
            }
        }

        public static void EmprestarLivro()
        {
        }

        public static void DevolverLivro()
        {
        }
    }
}
