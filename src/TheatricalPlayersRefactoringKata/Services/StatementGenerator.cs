public class StatementGenerator
{
    private readonly IPlayFeeCalculator _playFeeCalculator;

    public StatementGenerator(IPlayFeeCalculator playFeeCalculator)
    {
        _playFeeCalculator = playFeeCalculator;
    }

    public string GenerateTextStatement(IEnumerable<Play> plays)
    {
        var statement = new StringBuilder();
        var totalAmount = 0m;
        var totalCredits = 0;

        foreach (var play in plays)
        {
            var fee = GetPlayFee(play);
            var credits = _playFeeCalculator.CalculatePlayCredits(play);
            statement.AppendLine($"You rented a {play.Type} play; your bill is {fee}");
            totalAmount += fee;
            totalCredits += credits;
        }

        statement.AppendLine($"Amount owed is {totalAmount}");
        statement.AppendLine($"You earned {totalCredits} credits");
        return statement.ToString();
    }

    public string GenerateXmlStatement(IEnumerable<Play> plays)
    {
        var xmlStatement = new XmlDocument();
        var rootNode = xmlStatement.CreateElement("statement");
        xmlStatement.AppendChild(rootNode);

        var totalAmount = 0m;
        var totalCredits = 0;

        foreach (var play in plays)
        {
            var fee = GetPlayFee(play);
            var credits = _playFeeCalculator.CalculatePlayCredits(play);
            var playNode = xmlStatement.CreateElement("play");
            playNode.SetAttribute("type", play.Type);
            playNode.SetAttribute("fee", fee.ToString());
            playNode.SetAttribute("credits", credits.ToString());
            rootNode.AppendChild(playNode);
            totalAmount += fee;
            totalCredits += credits;
        }

        var totalNode = xmlStatement.CreateElement("total");
        totalNode.SetAttribute("amount", totalAmount.ToString());
        totalNode.SetAttribute("credits", totalCredits.ToString());
        rootNode.AppendChild(totalNode);

        var xmlWriter = new XmlTextWriter(new StringWriter());
        xmlStatement.WriteTo(xmlWriter);
        return xmlWriter.ToString();
    }

    private decimal GetPlayFee(Play play)
    {
        switch (play.Type)
        {
            case "tragedy":
                return _playFeeCalculator.CalculateTragedyPlayFee(play);
            case "comedy":
                return _playFeeCalculator.CalculateComedyPlayFee(play);
            case "history":
                return _playFeeCalculator.CalculateHistoricalPlayFee(play);
            default:
                throw new ArgumentException($"Invalid play type: {play.Type}");
        }
    }
}