using System;
using System.Collections.Generic;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatters;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class FormatterTests
    {
        [Fact]
        public void TextStatementFormatter_ReturnsCorrectlyFormattedText()
        {
            // Arrange
            var formatter = new TextStatementFormatter();
            var invoice = new Invoice("Test Customer", new List<Performance>
            {
                new Performance("play1", 30),
                new Performance("play2", 40)
            });
            
            var plays = new Dictionary<string, Play>
            {
                { "play1", new Play("Hamlet", 3000, PlayType.Tragedy) },
                { "play2", new Play("Comedy", 2000, PlayType.Comedy) }
            };
            
            var amounts = new Dictionary<string, decimal>
            {
                { "play1", 300.00m },
                { "play2", 500.00m }
            };
            
            var credits = new Dictionary<string, int>
            {
                { "play1", 0 },
                { "play2", 18 }
            };
            
            // Act
            var result = formatter.FormatStatement(invoice, plays, amounts, credits, 800.00m, 18);
            
            // Assert
            Assert.Contains("Extracto para Test Customer", result);
            Assert.Contains("Hamlet: R$ 300,00 (30 assentos)", result);
            Assert.Contains("Comedy: R$ 500,00 (40 assentos)", result);
            Assert.Contains("Valor total: R$ 800,00", result);
            Assert.Contains("Créditos acumulados: 18 créditos", result);
        }
        
        [Fact]
        public void XmlStatementFormatter_ReturnsCorrectlyFormattedXml()
        {
            // Arrange
            var formatter = new XmlStatementFormatter();
            var invoice = new Invoice("Test Customer", new List<Performance>
            {
                new Performance("play1", 30),
                new Performance("play2", 40)
            });
            
            var plays = new Dictionary<string, Play>
            {
                { "play1", new Play("Hamlet", 3000, PlayType.Tragedy) },
                { "play2", new Play("Comedy", 2000, PlayType.Comedy) }
            };
            
            var amounts = new Dictionary<string, decimal>
            {
                { "play1", 300.00m },
                { "play2", 500.00m }
            };
            
            var credits = new Dictionary<string, int>
            {
                { "play1", 0 },
                { "play2", 18 }
            };
            
            // Act
            var result = formatter.FormatStatement(invoice, plays, amounts, credits, 800.00m, 18);
            
            // Assert
            Assert.Contains("<statement customer=\"Test Customer\">", result);
            Assert.Contains("  <performances>", result);
            Assert.Contains("    <performance play=\"Hamlet\" audience=\"30\" amount=\"300.00\" credits=\"0\" />", result);
            Assert.Contains("    <performance play=\"Comedy\" audience=\"40\" amount=\"500.00\" credits=\"18\" />", result);
            Assert.Contains("  <total_amount>800.00</total_amount>", result);
            Assert.Contains("  <total_credits>18</total_credits>", result);
        }
    }
} 