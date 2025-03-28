namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    using TheatricalPlayersRefactoringKata.Domain.Entities;

    public interface IPriceCalculator
    {
        decimal CalculatePrice(Performance performance, Play play);
        int CalculateCredits(Performance performance, Play play); // ✅ Adicionando esse método
    }
}
