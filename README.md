# Desafio Target Sistemas

Soluções dos desafios técnicos para a vaga de Desenvolvedor Júnior, desenvolvidos em **C#** (.NET 8).

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Estrutura do Projeto

```
desafio-target/
├── desafio1/    # Cálculo de Comissões
├── desafio2/    # Controle de Estoque
└── desafio3/    # Cálculo de Juros
```

---

## Desafio 1 - Cálculo de Comissões

Programa que lê dados de vendas de um JSON e calcula a comissão de cada vendedor.

### Regras de Comissão

| Valor da Venda | Comissão |
|----------------|----------|
| Abaixo de R$ 100,00 | 0% |
| De R$ 100,00 a R$ 499,99 | 1% |
| A partir de R$ 500,00 | 5% |

### Como executar

```bash
cd desafio1
dotnet run
```

### Saída esperada

O programa exibe um relatório com o total de vendas e comissões de cada vendedor, ordenado por maior comissão.

---

## Desafio 2 - Controle de Estoque

Sistema interativo para gerenciar movimentações de estoque (entrada e saída de mercadorias).

### Funcionalidades

- Listar produtos em estoque
- Registrar entrada de mercadoria
- Registrar saída de mercadoria
- Visualizar histórico de movimentações

### Cada movimentação contém

- ID único (gerado automaticamente)
- Tipo (ENTRADA ou SAÍDA)
- Descrição informada pelo usuário
- Data/hora do registro
- Quantidade final do estoque

### Como executar

```bash
cd desafio2
dotnet run
```

### Uso

1. Escolha uma opção no menu
2. Para entrada/saída: informe o código do produto, quantidade e descrição
3. O sistema exibe o estoque final após cada movimentação

---

## Desafio 3 - Cálculo de Juros

Programa que calcula o valor dos juros de um boleto em atraso, considerando multa de **2,5% ao dia**.

### Como executar

```bash
cd desafio3
dotnet run
```

### Uso

1. Informe o valor original
2. Informe a data de vencimento (formato: dd/MM/yyyy)
3. O programa calcula os dias de atraso e o valor final com juros

### Exemplo

```
Valor: R$ 1.000,00
Vencimento: 17/11/2024
Data atual: 27/11/2024
Dias em atraso: 10
Juros (25%): R$ 250,00
Valor final: R$ 1.250,00
```

---

## Tecnologias Utilizadas

- C# / .NET 8
- System.Text.Json (leitura de arquivos JSON)
- LINQ (manipulação de dados)
