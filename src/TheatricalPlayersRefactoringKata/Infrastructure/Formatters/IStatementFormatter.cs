using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatters
{
    public interface IStatementFormatter
    {
        string FormatStatement(
            Invoice invoice, 
            Dictionary<string, Play> plays,
            Dictionary<string, decimal> amounts, 
            Dictionary<string, int> credits,
            decimal totalAmount, 
            int totalCredits);
    }
} 