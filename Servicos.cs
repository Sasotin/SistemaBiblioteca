namespace SistemaBiblioteca
{
    public class Servicos
    {
        private static List<Livro> listaDeLivros = new List<Livro>();
        public static void Separacao()
        {           
            Console.WriteLine("-------------------------------------");
        }
        
        public static void Dialogos (string Dialogo, ConsoleColor cor) //metodo para reduzir a quantidade de codigo 
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(Dialogo);
            Console.ResetColor();
        }
        
        private static bool ExistemLivrosNaLista() //metodo para verificar se existem livros dentro da lista
        {
            if (listaDeLivros.Count == 0)
            {
                Dialogos("\nNão existem livros cadastrados.\n", ConsoleColor.Red);
                return false;
            }
            return true;
        }

        static string GerarCodigoLivro(List<Livro> listaDeLivros) //metodo para gerar o codigo do livro e verificar se o codigo não existe ainda
        {
            Random random = new Random();
            string codigoGerado;
            bool codigoExiste;
            do
            {
                string parte1 = random.Next(1000, 9999).ToString(); //primeira parte aleatória
                string parte2 = random.Next(1000, 9999).ToString(); //segunda parte aleatória
                string parte3 = DateTime.Now.ToString("MMyy"); //terceira parte recebe data e ano do sistema
                codigoGerado = $"{parte1}-{parte2}-{parte3}"; //junção das partes
                codigoExiste = listaDeLivros.Exists(livro => livro._codigoLivro == codigoGerado); //verifica se já existe na lista
            }
            while (codigoExiste);
            
            return codigoGerado;
        }

        public static void AdicionarLivro() //metodo para adicionar livros à lista
        {
            string tituloLivro;
            do
            {
                Console.Write("Titulo do livro: \n");
                tituloLivro = Console.ReadLine();
                if (string.IsNullOrEmpty(tituloLivro)) //verifica valores nulos os vazios
                {
                    Dialogos("O título do livro não pode ser vazio.", ConsoleColor.Red);
                }
            }
            while (string.IsNullOrEmpty(tituloLivro));

            string autorLivro;
            do
            {
                Console.Write("Autor do livro: \n");
                autorLivro = Console.ReadLine();
                if (string.IsNullOrEmpty(autorLivro)) //verifica valores nulos os vazios
                {
                    Dialogos("\nO autor do livro não pode ser vazio.\n", ConsoleColor.Red);
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
                    break; 
                }
                else
                {
                    Dialogos("\nO ano de publicação deve ser um número maior que zero.\n", ConsoleColor.Red);
                }                    
            }

            //atribui o codigo ao livro
            string codigoLivro = GerarCodigoLivro(listaDeLivros);
            Console.WriteLine($"Código do livro: {codigoLivro}");

            //atribui a disponibilidade do livro
            bool disponibilidadeLivro = true;

            try
            {
                Livro novoLivro = new Livro(tituloLivro, autorLivro, anoPublicacaoLivro, codigoLivro, disponibilidadeLivro);
                listaDeLivros.Add(novoLivro);
                Dialogos("\nLivro adicionado com sucesso!\n", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Dialogos($"Erro ao adicionar livro: {ex.Message}", ConsoleColor.Red);
            }

        }

        public static void RemoverLivro() //remove livros especificados pelo usuário
        {
            if (!ExistemLivrosNaLista())
            {
                return;
            }

            Console.Write("Digite o nome ou código do livro que deseja remover: \n"); //input do usuario
            Separacao();
            string termoBusca = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(termoBusca)) //verifica se o input do usuario é  válido
            {
                Dialogos("\nNome/código não pode estar vazio para realizar a busca!\n", ConsoleColor.Red);
                return;
            }

            //busca o livro ignorando caixa alta ou espaços
            Livro livroEncontrado = listaDeLivros.Find(livro => livro._tituloLivro.Equals(termoBusca, StringComparison.OrdinalIgnoreCase) || livro._codigoLivro.Replace(" ", "").Replace("-", "").Equals(termoBusca.Replace(" ", "").Replace("-", ""),StringComparison.OrdinalIgnoreCase));

            if (livroEncontrado != null) //verifica se o livro existe para que possa ser removido
            {
                listaDeLivros.Remove(livroEncontrado);
                Dialogos($"\nLivro {livroEncontrado._tituloLivro}, código{livroEncontrado._codigoLivro} removido com sucesso!", ConsoleColor.Yellow);
            }
            else
            {
                Dialogos("\nLivro não encontrado!\n", ConsoleColor.Red);
            }
        }

        public static void ListarLivros() //exibe a lista completa de livros cadastrados
        {
            if (!ExistemLivrosNaLista())
            {
                return;
            }
            foreach (Livro livro in listaDeLivros) //percorre a lista de livros
            {               
                Dialogos($"""
                    TÍTULO: {livro._tituloLivro}
                    AUTOR: {livro._autorLivro}
                    ANO DA PUBLICAÇÃO: {livro._anoPublicacaoLivro}
                    CÓDIGO DO LIVRO: {livro._codigoLivro}
                    DISPONIBILIDADE: {livro._disponibilidade}
                    """, ConsoleColor.Yellow);
                Separacao();
            }
        }

        public static void PesquisarLivro() //busca por um determinado livro dentro da lista
        {
            Console.Write("\nDigite o nome ou código do livro que deseja pesquisar: ");
            string termoBusca = Console.ReadLine()?.Trim();
            Separacao();

            if (string.IsNullOrWhiteSpace(termoBusca)) //verifica se o input do usuario é  válido
            {
                Dialogos("Nome/código não pode estar vazio para realizar a busca!\n", ConsoleColor.Red);
                return;
            }

            //busca o livro ignorando caixa alta ou espaços
            Livro livroEncontrado = listaDeLivros.Find(livro => livro._tituloLivro.Equals(termoBusca, StringComparison.OrdinalIgnoreCase) || livro._codigoLivro.Replace(" ", "").Replace("-", "").Equals(termoBusca.Replace(" ", "").Replace("-", ""), StringComparison.OrdinalIgnoreCase));
            
            //se o livro existir, exibe os dados
            if(livroEncontrado != null) 
            {
                Dialogos($"""
                    TÍTULO: {livroEncontrado._tituloLivro}
                    AUTOR: {livroEncontrado._autorLivro}
                    ANO DA PUBLICAÇÃO: {livroEncontrado._anoPublicacaoLivro}
                    CÓDIGO DO LIVRO: {livroEncontrado._codigoLivro}
                    DISPONIBILIDADE: {livroEncontrado._disponibilidade}
                    """, ConsoleColor.Yellow);
                Separacao();
            }
            else
            {
                Dialogos("\nLivro não encontrado!\n", ConsoleColor.Red);
                return;
            }
        }

        public static void EmprestarLivro() //metodo para emprestar livros
        {
            if (!ExistemLivrosNaLista())
            {
                return;
            }

            Console.Write("\nDigite o nome ou código do livro que deseja emprestar: "); 
            string termoBusca = Console.ReadLine()?.Trim();
            Separacao();

            if (string.IsNullOrWhiteSpace(termoBusca)) //verifica se o input do usuario é  válido
            {
                Dialogos("Nome/código não pode estar vazio para realizar a busca!", ConsoleColor.Red);
                return;
            }
            //busca o livro ignorando caixa alta ou espaços
            Livro livroEncontrado = listaDeLivros.Find(livro => livro._tituloLivro.Equals(termoBusca, StringComparison.OrdinalIgnoreCase) || livro._codigoLivro.Replace(" ", "").Replace("-", "").Equals(termoBusca.Replace(" ", "").Replace("-", ""), StringComparison.OrdinalIgnoreCase));

            //se o livro existir, exibe os dados
            if (livroEncontrado != null)
            {
                Dialogos($"""
                    TÍTULO: {livroEncontrado._tituloLivro}
                    AUTOR: {livroEncontrado._autorLivro}
                    ANO DA PUBLICAÇÃO: {livroEncontrado._anoPublicacaoLivro}
                    CÓDIGO DO LIVRO: {livroEncontrado._codigoLivro}
                    DISPONIBILIDADE: {(livroEncontrado._disponibilidade ? "Disponível" : "Emprestado")}
                    """, ConsoleColor.Yellow);
                Separacao();
            }
            else
            {
                Dialogos("\nLivro não encontrado!", ConsoleColor.Red);
                return;
            }

            
            
            if (!livroEncontrado._disponibilidade)
            {            
                Dialogos($"Livro {livroEncontrado._tituloLivro}, código {livroEncontrado._codigoLivro} indisponível para empréstimo!", ConsoleColor.Red);
                return;
            }
            else
            {
                Dialogos($"Livro {livroEncontrado._tituloLivro}, código {livroEncontrado._codigoLivro} disponível para emprestimo.", ConsoleColor.Green);
                Console.Write("\nDeseja realizar o empréstimo? (S/N) ");
                char escolha = Console.ReadKey().KeyChar;

                if (char.ToUpper(escolha) == 'S')
                {
                    Dialogos("\nLivro emprestado com sucesso!", ConsoleColor.Green);
                    livroEncontrado._disponibilidade = false;
                }
                else
                {
                    Dialogos("\nEmpréstimo cancelado!", ConsoleColor.Red);
                    return;
                }
            }
        }

        public static void DevolverLivro() //metodo para devolver livros
        {
            if (!ExistemLivrosNaLista())
            {
                return;
            }

            Console.Write("Digite o nome ou código do livro que deseja devolver: ");
            string termoBusca = Console.ReadLine()?.Trim();
            Separacao();

            if (string.IsNullOrWhiteSpace(termoBusca)) //verifica se o input do usuario é  válido
            {
                Dialogos("Nome/código não pode estar vazio para realizar a busca!\n", ConsoleColor.Red);
                return;
            }
            //busca o livro ignorando caixa alta ou espaços
            Livro livroEncontrado = listaDeLivros.Find(livro => livro._tituloLivro.Equals(termoBusca, StringComparison.OrdinalIgnoreCase) || livro._codigoLivro.Replace(" ", "").Replace("-", "").Equals(termoBusca.Replace(" ", "").Replace("-", ""), StringComparison.OrdinalIgnoreCase));

            //se o livro existir, exibe os dados
            if (livroEncontrado != null)
            {
                Dialogos($"""
                    TÍTULO: {livroEncontrado._tituloLivro}
                    AUTOR: {livroEncontrado._autorLivro}
                    ANO DA PUBLICAÇÃO: {livroEncontrado._anoPublicacaoLivro}
                    CÓDIGO DO LIVRO: {livroEncontrado._codigoLivro}
                    DISPONIBILIDADE: {(livroEncontrado._disponibilidade ? "Disponível" : "Emprestado")}
                    """, ConsoleColor.Yellow);
                Separacao();
            }
            else
            {
                Console.WriteLine("\nLivro não encontrado.\n");
                return;
            }



            if (livroEncontrado._disponibilidade)
            {
                Console.WriteLine($"Livro {livroEncontrado._tituloLivro}, código {livroEncontrado._codigoLivro} disponível na biblioteca!");
                return;
            }
                       
            Console.Write("Deseja realizar a devolução? (S/N)");
            char escolha = Console.ReadKey().KeyChar;

            if (char.ToUpper(escolha) == 'S')
            {
                Dialogos("\nLivro devolvido com sucesso!\n", ConsoleColor.Green); 
                livroEncontrado._disponibilidade = true;
            }
            else
            {
                Dialogos("\nEmpréstimo canecelado\n", ConsoleColor.Red);
                return;
            }            
        }
    }
}
