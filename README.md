# 🎭 Sistema de Faturas para Companhia de Teatro

> Um sistema para calcular faturas de apresentações teatrais, com suporte a múltiplos gêneros de peças e formatos de extrato.

## 📜 Sobre o Projeto

Este projeto implementa um sistema para gerar extratos de faturas para uma companhia de teatro. A companhia é contratada para realizar apresentações e cobra com base no número de linhas de cada peça, tamanho da plateia e gênero da peça.

### ✨ Funcionalidades

- Cálculo de preços para diferentes gêneros de peças (Tragédia, Comédia, Histórica)
- Cálculo de créditos de fidelização para clientes
- Geração de extratos em formato texto
- Geração de extratos em formato XML
- Arquitetura extensível para adicionar novos gêneros e formatos

## 🚀 Como Executar

### Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download) ou superior

### Comandos Básicos

```bash
# Clone o repositório (caso ainda não tenha)
git clone <url-do-repositorio>
cd nome-do-repositorio

# Compilar o projeto
dotnet build

# Executar os testes
dotnet test
```

## 🔍 Estrutura do Projeto

```
src/TheatricalPlayersRefactoringKata/
├── Domain/                    # Camada de domínio
│   ├── Entities/              # Entidades do domínio (Invoice, Performance, Play)
│   └── Interfaces/            # Interfaces do domínio (IPriceCalculator)
├── Services/                  # Serviços da aplicação
│   └── PriceCalculator.cs     # Implementação do cálculo de preços e créditos
├── Infrastructure/            # Infraestrutura
│   └── Formatters/            # Formatadores de extratos (texto, XML)
└── StatementPrinter.cs        # Classe principal de geração de extratos

tests/                         # Testes automáticos
├── FormatterTests.cs          # Testes para formatadores
├── PriceCalculatorTests.cs    # Testes para cálculos de preço
└── StatementPrinterTests.cs   # Testes de integração
```

## 📋 Regras de Negócio

### Cálculo de Preços

- **Base**: linhas da peça ÷ 10, limitado entre 1000 e 4000 linhas
- **Tragédia**: valor base (plateia ≤ 30) + 10.00 por cada espectador adicional
- **Comédia**: valor base + 3.00 por espectador; se plateia > 20, adiciona 100.00 + 5.00 por espectador adicional
- **Histórica**: combina cálculos de tragédia e comédia

### Cálculo de Créditos

- 1 crédito para cada espectador acima de 30
- Bônus de 1/5 da plateia (arredondado para baixo) para peças de comédia

## 🧩 Como Estender o Sistema

### Adicionar Novo Gênero de Peça

1. Adicione o novo gênero no enum `PlayType` em `Domain/Entities/Play.cs`:

```csharp
public enum PlayType
{
    Tragedy,
    Comedy,
    Historical,
    NovoGenero // Adicione aqui
}
```

2. Implemente o cálculo de preço e créditos no `PriceCalculator` em `Services/PriceCalculator.cs`:

```csharp
public decimal CalculatePrice(Performance performance, Play play)
{
    // Código existente...

    switch (play.Type)
    {
        // Casos existentes...
        
        case PlayType.NovoGenero:
            // Implemente o cálculo para o novo gênero
            return /* seu cálculo */;
            
        default:
            throw new ArgumentException($"Tipo de peça desconhecido: {play.Type}");
    }
}

public int CalculateCredits(Performance performance, Play play)
{
    // Código existente...
    
    switch (play.Type)
    {
        // Casos existentes...
        
        case PlayType.NovoGenero:
            // Implemente o cálculo de créditos para o novo gênero
            return /* seu cálculo */;
            
        default:
            throw new ArgumentException($"Tipo de peça desconhecido: {play.Type}");
    }
}
```

### Adicionar Novo Formato de Extrato

1. Crie uma nova classe implementando a interface `IStatementFormatter`:

```csharp
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatters
{
    public class NovoFormatter : IStatementFormatter
    {
        public string FormatStatement(
            Invoice invoice, 
            Dictionary<string, Play> plays, 
            Dictionary<string, decimal> amounts, 
            Dictionary<string, int> credits, 
            decimal totalAmount, 
            int totalCredits)
        {
            // Implemente a formatação para o novo formato
            // ...
            
            return resultado;
        }
    }
}
```

2. Registre o novo formatador no construtor de `StatementPrinter`:

```csharp
public StatementPrinter()
{
    _priceCalculator = new PriceCalculator();
    _formatters = new Dictionary<string, IStatementFormatter>
    {
        { "text", new TextStatementFormatter() },
        { "xml", new XmlStatementFormatter() },
        { "novo", new NovoFormatter() } // Adicione aqui
    };
}
```

## 📝 Testes Automatizados

O projeto inclui testes automatizados para garantir o correto funcionamento:

- **Testes unitários**: Validam o comportamento de componentes individuais
- **Testes de aprovação**: Comparam a saída com resultados aprovados previamente

Para executar todos os testes:
```bash
dotnet test
```

## 📚 Conceitos Aplicados

- **SOLID**: Princípios de design de software
  - **S**: Responsabilidade Única - cada classe tem uma única responsabilidade
  - **O**: Aberto-Fechado - código aberto para extensão, fechado para modificação
  - **D**: Inversão de Dependência - dependemos de abstrações, não de implementações

- **Design Patterns**:
  - **Strategy**: Usado nos formatadores de extrato
  - **Factory**: Usado na criação de calculadoras de preço

## 🤝 Contribuindo

1. Crie testes para sua implementação
2. Siga o estilo de código existente
3. Mantenha a arquitetura limpa e organizada
4. Atualize a documentação quando necessário

---

Projeto desenvolvido como parte de um exercício de refatoração e implementação de novas funcionalidades. 