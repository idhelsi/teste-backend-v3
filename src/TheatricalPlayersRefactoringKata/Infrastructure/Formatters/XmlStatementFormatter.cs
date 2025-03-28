using System.Collections.Generic;
using System.Text;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        public string FormatStatement(
            Invoice invoice, 
            Dictionary<string, Play> plays, 
            Dictionary<string, decimal> amounts, 
            Dictionary<string, int> credits, 
            decimal totalAmount, 
            int totalCredits)
        {
            // Usar cultura inglesa para garantir ponto como separador decimal
            CultureInfo culture = new CultureInfo("en-US");
            
            // Criar o XML formatado com indentação
            var xml = new StringBuilder();
            
            xml.AppendLine("<statement customer=\"" + invoice.Customer + "\">");
            xml.AppendLine("  <performances>");
            
            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var amount = amounts[perf.PlayID].ToString("0.00", culture);
                var creditCount = credits[perf.PlayID];
                
                xml.AppendLine($"    <performance play=\"{play.Name}\" audience=\"{perf.Audience}\" amount=\"{amount}\" credits=\"{creditCount}\" />");
            }
            
            xml.AppendLine("  </performances>");
            xml.AppendLine($"  <total_amount>{totalAmount.ToString("0.00", culture)}</total_amount>");
            xml.AppendLine($"  <total_credits>{totalCredits}</total_credits>");
            xml.Append("</statement>");
            
            return xml.ToString();
        }
    }
}
