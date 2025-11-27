using System.Globalization;

class Program
{
    const decimal MULTA_DIARIA = 0.025m; // 2,5% ao dia

    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("     CÁLCULO DE JUROS POR ATRASO");
        Console.WriteLine("========================================\n");

        // Solicita o valor
        Console.Write("Digite o valor original (R$): ");
        if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.CurrentCulture, out decimal valorOriginal) || valorOriginal <= 0)
        {
            Console.WriteLine("Valor inválido!");
            return;
        }

        // Solicita a data de vencimento
        Console.Write("Digite a data de vencimento (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dataVencimento))
        {
            Console.WriteLine("Data inválida! Use o formato dd/MM/yyyy");
            return;
        }

        // Calcula os juros
        CalcularJuros(valorOriginal, dataVencimento);
    }

    static void CalcularJuros(decimal valorOriginal, DateTime dataVencimento)
    {
        DateTime dataHoje = DateTime.Today;

        // Calcula dias de atraso
        int diasAtraso = (dataHoje - dataVencimento).Days;

        Console.WriteLine("\n========================================");
        Console.WriteLine("           RESULTADO");
        Console.WriteLine("========================================");
        Console.WriteLine($"Data de vencimento: {dataVencimento:dd/MM/yyyy}");
        Console.WriteLine($"Data de hoje: {dataHoje:dd/MM/yyyy}");
        Console.WriteLine($"Valor original: R$ {valorOriginal:N2}");
        Console.WriteLine("----------------------------------------");

        if (diasAtraso <= 0)
        {
            Console.WriteLine("Status: EM DIA (sem juros)");
            Console.WriteLine($"Valor a pagar: R$ {valorOriginal:N2}");
        }
        else
        {
            // Calcula o valor dos juros
            decimal percentualTotal = diasAtraso * MULTA_DIARIA;
            decimal valorJuros = valorOriginal * percentualTotal;
            decimal valorFinal = valorOriginal + valorJuros;

            Console.WriteLine($"Dias em atraso: {diasAtraso}");
            Console.WriteLine($"Multa por dia: {MULTA_DIARIA:P1}");
            Console.WriteLine($"Percentual total de multa: {percentualTotal:P1}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Valor dos juros: R$ {valorJuros:N2}");
            Console.WriteLine($"VALOR FINAL A PAGAR: R$ {valorFinal:N2}");
        }

        Console.WriteLine("========================================");
    }
}
