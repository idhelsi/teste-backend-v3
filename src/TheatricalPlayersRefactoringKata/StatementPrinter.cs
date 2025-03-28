using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatters;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly IPriceCalculator _priceCalculator;
        private readonly Dictionary<string, IStatementFormatter> _formatters;

        public StatementPrinter()
        {
            _priceCalculator = new PriceCalculator();
            _formatters = new Dictionary<string, IStatementFormatter>
            {
                { "text", new TextStatementFormatter() },
                { "xml", new XmlStatementFormatter() }
            };
        }

        public StatementPrinter(IPriceCalculator priceCalculator, Dictionary<string, IStatementFormatter> formatters)
        {
            _priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
            _formatters = formatters ?? throw new ArgumentNullException(nameof(formatters));
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays, string format = "text")
        {
            if (!_formatters.ContainsKey(format))
            {
                throw new ArgumentException($"Formato não suportado: {format}");
            }

            // Calcula valores e créditos para cada performance
            var amounts = new Dictionary<string, decimal>();
            var credits = new Dictionary<string, int>();
            decimal totalAmount = 0;
            int totalCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                perf.Play = play; // Associar o objeto Play à performance

                // Calcular preço e créditos
                decimal amount = _priceCalculator.CalculatePrice(perf, play);
                int creditsEarned = _priceCalculator.CalculateCredits(perf, play);
                
                // Armazenar resultados
                amounts[perf.PlayID] = amount;
                credits[perf.PlayID] = creditsEarned;
                
                // Atualizar totais
                totalAmount += amount;
                totalCredits += creditsEarned;
            }

            // Usar o formatador apropriado
            return _formatters[format].FormatStatement(invoice, plays, amounts, credits, totalAmount, totalCredits);
        }
    }
} 