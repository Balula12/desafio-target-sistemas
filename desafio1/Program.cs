using System.Text.Json;

// Classe para representar uma venda
public class Venda
{
    public string Vendedor { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}

// Classe para representar o JSON completo
public class DadosVendas
{
    public List<Venda> Vendas { get; set; } = new List<Venda>();
}

class Program
{
    static void Main(string[] args)
    {
        // Lê o arquivo JSON
        string jsonPath = "vendas.json";
        string jsonContent = File.ReadAllText(jsonPath);

        // Configuração para deserialização (case-insensitive)
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Deserializa o JSON
        var dados = JsonSerializer.Deserialize<DadosVendas>(jsonContent, options);

        if (dados?.Vendas == null)
        {
            Console.WriteLine("Erro ao ler os dados de vendas.");
            return;
        }

        // Agrupa as vendas por vendedor e calcula a comissão
        var comissoesPorVendedor = dados.Vendas
            .GroupBy(v => v.Vendedor)
            .Select(grupo => new
            {
                Vendedor = grupo.Key,
                TotalVendas = grupo.Sum(v => v.Valor),
                TotalComissao = grupo.Sum(v => CalcularComissao(v.Valor)),
                QuantidadeVendas = grupo.Count()
            })
            .OrderByDescending(x => x.TotalComissao);

        // Exibe os resultados
        Console.WriteLine("========================================");
        Console.WriteLine("     RELATÓRIO DE COMISSÕES");
        Console.WriteLine("========================================\n");

        foreach (var vendedor in comissoesPorVendedor)
        {
            Console.WriteLine($"Vendedor: {vendedor.Vendedor}");
            Console.WriteLine($"  Quantidade de vendas: {vendedor.QuantidadeVendas}");
            Console.WriteLine($"  Total em vendas: R$ {vendedor.TotalVendas:N2}");
            Console.WriteLine($"  Total em comissão: R$ {vendedor.TotalComissao:N2}");
            Console.WriteLine();
        }

        // Total geral
        var totalGeralComissao = comissoesPorVendedor.Sum(v => v.TotalComissao);
        Console.WriteLine("========================================");
        Console.WriteLine($"TOTAL GERAL EM COMISSÕES: R$ {totalGeralComissao:N2}");
        Console.WriteLine("========================================");
    }

    /// <summary>
    /// Calcula a comissão de uma venda baseado nas regras:
    /// - Vendas abaixo de R$100,00: não gera comissão
    /// - Vendas de R$100,00 até R$499,99: 1% de comissão
    /// - Vendas a partir de R$500,00: 5% de comissão
    /// </summary>
    static decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 100)
            return 0;
        else if (valorVenda < 500)
            return valorVenda * 0.01m;
        else
            return valorVenda * 0.05m;
    }
}
