using System;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class PriceCalculatorTests
    {
        private readonly PriceCalculator _calculator;

        public PriceCalculatorTests()
        {
            _calculator = new PriceCalculator();
        }

        [Theory]
        [InlineData(1000, 25, 100.00)] // Mínimo de linhas, abaixo de 30 espectadores
        [InlineData(4000, 25, 400.00)] // Máximo de linhas, abaixo de 30 espectadores
        [InlineData(2500, 40, 500.00)] // Linhas intermediárias, acima de 30 espectadores: 250 + (40-30)*10 = 350
        [InlineData(5000, 35, 450.00)] // Acima do máximo de linhas (deve ser limitado a 4000), acima de 30
        [InlineData(900, 20, 100.00)]  // Abaixo do mínimo de linhas (deve ser limitado a 1000), abaixo de 30
        public void CalculatePrice_ForTragedy_ReturnsCorrectValue(int lineCount, int audience, decimal expectedPrice)
        {
            // Arrange
            var play = new Play("Test Tragedy", lineCount, PlayType.Tragedy);
            var performance = new Performance("test", audience);
            performance.Play = play;

            // Act
            var result = _calculator.CalculatePrice(performance, play);

            // Assert
            Assert.Equal(expectedPrice, result);
        }

        [Theory]
        [InlineData(1000, 15, 145.00)] // Mínimo de linhas, abaixo de 20 espectadores
        [InlineData(4000, 15, 445.00)] // Máximo de linhas, abaixo de 20 espectadores
        [InlineData(2500, 25, 475.00)] // Linhas intermediárias, acima de 20 espectadores: 250 + 25*3 + 100 + (25-20)*5 = 250 + 75 + 100 + 25 = 450
        [InlineData(2000, 40, 600.00)] // Linhas intermediárias, bem acima de 20 espectadores: 200 + 40*3 + 100 + (40-20)*5 = 200 + 120 + 100 + 100 = 520
        public void CalculatePrice_ForComedy_ReturnsCorrectValue(int lineCount, int audience, decimal expectedPrice)
        {
            // Arrange
            var play = new Play("Test Comedy", lineCount, PlayType.Comedy);
            var performance = new Performance("test", audience);
            performance.Play = play;

            // Act
            var result = _calculator.CalculatePrice(performance, play);

            // Assert
            Assert.Equal(expectedPrice, result);
        }

        [Theory]
        [InlineData(2000, 25, 420.00)] // História = Tragédia (200) + Comédia (275) = 475
        [InlineData(3000, 35, 880.00)] // História = Tragédia (350) + Comédia (530) = 880
        public void CalculatePrice_ForHistory_ReturnsCorrectValue(int lineCount, int audience, decimal expectedPrice)
        {
            // Arrange
            var play = new Play("Test History", lineCount, PlayType.History);
            var performance = new Performance("test", audience);
            performance.Play = play;

            // Act
            var result = _calculator.CalculatePrice(performance, play);

            // Assert
            Assert.Equal(expectedPrice, result);
        }

        [Theory]
        [InlineData(25, 0)]  // Abaixo de 30 espectadores, sem créditos
        [InlineData(35, 5)]  // 5 espectadores acima de 30, 5 créditos
        [InlineData(50, 20)] // 20 espectadores acima de 30, 20 créditos
        public void CalculateCredits_ForTragedy_ReturnsCorrectValue(int audience, int expectedCredits)
        {
            // Arrange
            var play = new Play("Test Tragedy", 2000, PlayType.Tragedy);
            var performance = new Performance("test", audience);
            performance.Play = play;

            // Act
            var result = _calculator.CalculateCredits(performance, play);

            // Assert
            Assert.Equal(expectedCredits, result);
        }

        [Theory]
        [InlineData(15, 3)]  // Abaixo de 30 espectadores, só créditos de bônus (15/5 = 3)
        [InlineData(35, 12)] // Acima de 30: (35-30) + 35/5 = 5 + 7 = 12 créditos
        [InlineData(50, 30)] // Bastante acima de 30: (50-30) + 50/5 = 20 + 10 = 30 créditos
        public void CalculateCredits_ForComedy_ReturnsCorrectValue(int audience, int expectedCredits)
        {
            // Arrange
            var play = new Play("Test Comedy", 2000, PlayType.Comedy);
            var performance = new Performance("test", audience);
            performance.Play = play;

            // Act
            var result = _calculator.CalculateCredits(performance, play);

            // Assert
            Assert.Equal(expectedCredits, result);
        }

        [Theory]
        [InlineData(25, 0)]  // Abaixo de 30 espectadores, sem créditos
        [InlineData(35, 5)]  // 5 espectadores acima de 30, 5 créditos
        [InlineData(50, 20)] // 20 espectadores acima de 30, 20 créditos
        public void CalculateCredits_ForHistory_ReturnsCorrectValue(int audience, int expectedCredits)
        {
            // Arrange
            var play = new Play("Test History", 2000, PlayType.History);
            var performance = new Performance("test", audience);
            performance.Play = play;

            // Act
            var result = _calculator.CalculateCredits(performance, play);

            // Assert
            Assert.Equal(expectedCredits, result);
        }
    }
} 