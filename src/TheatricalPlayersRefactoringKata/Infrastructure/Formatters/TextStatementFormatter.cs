using System.Collections.Generic;
using System.Text;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatters
{
    public class TextStatementFormatter : IStatementFormatter
    {
        public string FormatStatement(
            Invoice invoice, 
            Dictionary<string, Play> plays, 
            Dictionary<string, decimal> amounts, 
            Dictionary<string, int> credits, 
            decimal totalAmount, 
            int totalCredits)
        {
            // Definir a cultura para formatação de valores monetários
            CultureInfo culture = new CultureInfo("pt-BR");
            
            var result = new StringBuilder($"Extracto para {invoice.Customer}\n");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                // Formato: "  Nome da Peça: R$ XXX,XX (YY assentos)"
                result.AppendLine($"  {play.Name}: {amounts[perf.PlayID].ToString("C", culture)} ({perf.Audience} assentos)");
            }

            // Formato: "Valor total: R$ X.XXX,XX"
            result.AppendLine($"Valor total: {totalAmount.ToString("C", culture)}");
            
            // Formato: "Créditos acumulados: ZZ créditos"
            result.AppendLine($"Créditos acumulados: {totalCredits} créditos");

            return result.ToString();
        }
    }
}
