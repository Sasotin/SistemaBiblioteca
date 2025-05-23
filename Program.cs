using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    internal class Program
    {
        static string separacao = "-------------------------------------";
        static bool executando = true;
        static async Task Main(string[] args)
        {
            while (executando)
            {
                Console.WriteLine("=====Sistema de Biblioteca=====");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("""
                    :::Escolha uma opção::: 
                    1. Adicionar Livro
                    2. Remover Livro
                    3. Listar Livros
                    4. Pesquisar Livro
                    5. Emprestar Livro
                    6. Devolver Livro
                    7. Sair
                    """);
                Console.ResetColor();
                Console.WriteLine(separacao);

                int escolha  = Convert.ToInt32(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        Console.WriteLine(separacao);
                        Servicos.AdicionarLivro();
                        break;
                    case 2:
                        Console.WriteLine(separacao);
                        Servicos.RemoverLivro();
                        break;
                    case 3:
                        Console.WriteLine(separacao);
                        Servicos.ListarLivros();
                        break;
                    case 4:
                        Console.WriteLine(separacao);
                        Servicos.PesquisarLivro();
                        break;
                    case 5:
                        Console.WriteLine(separacao);
                        Servicos.EmprestarLivro();
                        break;
                    case 6:
                        Console.WriteLine(separacao);
                        Servicos.DevolverLivro();
                        break;
                    case 7:
                        Console.WriteLine(separacao);
                        Console.WriteLine("Saindo do sistema em 3s...");
                        await Task.Delay(3000);
                        executando = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção inválida! Tente Novamente.\n");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
