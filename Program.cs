using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    internal class Program
    {     
        static bool executando = true;
        static async Task Main(string[] args)
        {
            while (executando)
            {
                Console.WriteLine("=====Sistema de Biblioteca=====");
                Servicos.Dialogos("""
                    :::Escolha uma opção::: 
                    1. Adicionar Livro
                    2. Remover Livro
                    3. Listar Livros
                    4. Pesquisar Livro
                    5. Emprestar Livro
                    6. Devolver Livro
                    7. Sair
                    """, ConsoleColor.DarkCyan);
                Servicos.Separacao();

                var escolha  = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        Servicos.Separacao();
                        Servicos.AdicionarLivro();
                        break;
                    case "2":
                        Servicos.Separacao();
                        Servicos.RemoverLivro();
                        break;
                    case "3":
                        Servicos.Separacao();
                        Servicos.ListarLivros();
                        break;
                    case "4":
                        Servicos.Separacao();
                        Servicos.PesquisarLivro();
                        break;
                    case "5":
                        Servicos.Separacao();
                        Servicos.EmprestarLivro();
                        break;
                    case "6":
                        Servicos.Separacao();
                        Servicos.DevolverLivro();
                        break;
                    case "7":
                        Servicos.Separacao();
                        Servicos.Dialogos("\nSaindo do sistema em 3s...\n", ConsoleColor.Magenta);
                        await Task.Delay(3000);
                        executando = false;
                        break;
                    default:
                        Servicos.Dialogos("\nOpção inválida! Tente Novamente.\n", ConsoleColor.Magenta);
                        break;
                }               
            }
        }
    }
}
