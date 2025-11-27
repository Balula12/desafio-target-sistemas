using System.Text.Json;

// Classes para representar os dados
public class Produto
{
    public int CodigoProduto { get; set; }
    public string DescricaoProduto { get; set; } = string.Empty;
    public int Estoque { get; set; }
}

public class DadosEstoque
{
    public List<Produto> Estoque { get; set; } = new List<Produto>();
}

public class Movimentacao
{
    public int Id { get; set; }
    public int CodigoProduto { get; set; }
    public string Tipo { get; set; } = string.Empty; // "ENTRADA" ou "SAIDA"
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public DateTime DataHora { get; set; }
}

class Program
{
    static List<Produto> produtos = new List<Produto>();
    static List<Movimentacao> movimentacoes = new List<Movimentacao>();
    static int proximoIdMovimentacao = 1;

    static void Main(string[] args)
    {
        CarregarEstoque();

        bool continuar = true;
        while (continuar)
        {
            ExibirMenu();
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarProdutos();
                    break;
                case "2":
                    RegistrarMovimentacao("ENTRADA");
                    break;
                case "3":
                    RegistrarMovimentacao("SAIDA");
                    break;
                case "4":
                    ListarMovimentacoes();
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("\nPrograma encerrado.");
                    break;
                default:
                    Console.WriteLine("\nOpção inválida!");
                    break;
            }
        }
    }

    static void CarregarEstoque()
    {
        string jsonPath = "estoque.json";
        string jsonContent = File.ReadAllText(jsonPath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var dados = JsonSerializer.Deserialize<DadosEstoque>(jsonContent, options);

        if (dados?.Estoque != null)
        {
            produtos = dados.Estoque;
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("     CONTROLE DE ESTOQUE");
        Console.WriteLine("========================================");
        Console.WriteLine("1 - Listar produtos");
        Console.WriteLine("2 - Entrada de mercadoria");
        Console.WriteLine("3 - Saída de mercadoria");
        Console.WriteLine("4 - Ver histórico de movimentações");
        Console.WriteLine("0 - Sair");
        Console.WriteLine("========================================");
        Console.Write("Escolha uma opção: ");
    }

    static void ListarProdutos()
    {
        Console.WriteLine("\n--- PRODUTOS EM ESTOQUE ---");
        Console.WriteLine($"{"Código",-10} {"Descrição",-30} {"Estoque",10}");
        Console.WriteLine(new string('-', 52));

        foreach (var produto in produtos)
        {
            Console.WriteLine($"{produto.CodigoProduto,-10} {produto.DescricaoProduto,-30} {produto.Estoque,10}");
        }
    }

    static void RegistrarMovimentacao(string tipo)
    {
        Console.WriteLine($"\n--- {(tipo == "ENTRADA" ? "ENTRADA" : "SAÍDA")} DE MERCADORIA ---");

        ListarProdutos();

        Console.Write("\nDigite o código do produto: ");
        if (!int.TryParse(Console.ReadLine(), out int codigoProduto))
        {
            Console.WriteLine("Código inválido!");
            return;
        }

        var produto = produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        Console.Write("Digite a quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
        {
            Console.WriteLine("Quantidade inválida!");
            return;
        }

        // Verifica se há estoque suficiente para saída
        if (tipo == "SAIDA" && quantidade > produto.Estoque)
        {
            Console.WriteLine($"Estoque insuficiente! Disponível: {produto.Estoque}");
            return;
        }

        Console.Write("Digite a descrição da movimentação: ");
        string descricao = Console.ReadLine() ?? "";

        // Atualiza o estoque
        if (tipo == "ENTRADA")
            produto.Estoque += quantidade;
        else
            produto.Estoque -= quantidade;

        // Registra a movimentação
        var movimentacao = new Movimentacao
        {
            Id = proximoIdMovimentacao++,
            CodigoProduto = codigoProduto,
            Tipo = tipo,
            Descricao = descricao,
            Quantidade = quantidade,
            DataHora = DateTime.Now
        };
        movimentacoes.Add(movimentacao);

        // Exibe o resultado
        Console.WriteLine("\n========================================");
        Console.WriteLine("     MOVIMENTAÇÃO REGISTRADA");
        Console.WriteLine("========================================");
        Console.WriteLine($"ID Movimentação: {movimentacao.Id}");
        Console.WriteLine($"Tipo: {movimentacao.Tipo}");
        Console.WriteLine($"Descrição: {movimentacao.Descricao}");
        Console.WriteLine($"Produto: {produto.DescricaoProduto}");
        Console.WriteLine($"Quantidade: {quantidade}");
        Console.WriteLine($"Data/Hora: {movimentacao.DataHora:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"ESTOQUE FINAL: {produto.Estoque} unidades");
        Console.WriteLine("========================================");
    }

    static void ListarMovimentacoes()
    {
        Console.WriteLine("\n--- HISTÓRICO DE MOVIMENTAÇÕES ---");

        if (movimentacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma movimentação registrada.");
            return;
        }

        foreach (var mov in movimentacoes)
        {
            var produto = produtos.FirstOrDefault(p => p.CodigoProduto == mov.CodigoProduto);
            Console.WriteLine($"\nID: {mov.Id} | {mov.Tipo} | {mov.DataHora:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Produto: {produto?.DescricaoProduto}");
            Console.WriteLine($"Quantidade: {mov.Quantidade}");
            Console.WriteLine($"Descrição: {mov.Descricao}");
        }
    }
}
