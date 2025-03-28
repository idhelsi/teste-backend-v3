public class PlayFeeCalculator : IPlayFeeCalculator
{
    public decimal CalculateTragedyPlayFee(Play play)
    {
        var baseAmount = CalculateBaseAmount(play.Lines);
        var additionalFee = play.Audience > 30 ? (play.Audience - 30) * 10 : 0;
        return baseAmount + additionalFee;
    }

    public decimal CalculateComedyPlayFee(Play play)
    {
        var baseAmount = CalculateBaseAmount(play.Lines);
        var additionalFee = play.Audience * 3;
        if (play.Audience > 20)
        {
            additionalFee += 100 + (play.Audience - 20) * 5;
        }
        return baseAmount + additionalFee;
    }

    public decimal CalculateHistoricalPlayFee(Play play)
    {
        var tragedyFee = CalculateTragedyPlayFee(play);
        var comedyFee = CalculateComedyPlayFee(play);
        return tragedyFee + comedyFee;
    }

    public int CalculatePlayCredits(Play play)
    {
        if (play.Audience <= 30)
            return 0;
        var credits = play.Audience - 30;
        if (play.Type == "comedy")
            credits += (int)Math.Floor(play.Audience * 0.2m);
        return credits;
    }

    private decimal CalculateBaseAmount(int lines)
    {
        lines = Math.Max(1000, Math.Min(4000, lines));
        return lines / 10m;
    }
}