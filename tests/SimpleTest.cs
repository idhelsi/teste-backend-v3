using System;
using System.Collections.Generic;
using Xunit;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class SimpleTest
    {
        [Fact]
        public void TestStatementPrinter()
        {
            // Arrange
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.Tragedy));
            
            var performances = new List<Performance>
            {
                new Performance("hamlet", 55)
            };
            var invoice = new Invoice("Test Customer", performances);
            
            var statementPrinter = new StatementPrinter();
            
            // Act
            var result = statementPrinter.Print(invoice, plays);
            
            // Assert
            Assert.Contains("Hamlet", result);
            Assert.Contains("Test Customer", result);
            Assert.Contains("55", result);
        }
    }
} 