namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    // Enum para tipos de peça, substituindo a comparação de strings
    public enum PlayType
    {
        Comedy,
        Tragedy,
        History
    }

    public class Play
    {
        public string Name { get; }
        public PlayType Type { get; }
        public int LineCount { get; }

        // Construtor para instâncias com nome, contagem de linhas e tipo enumerado
        public Play(string name, int lineCount, PlayType type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LineCount = lineCount;
            Type = type;
        }

        // Construtor para instâncias com nome, contagem de linhas e tipo como string
        public Play(string name, int lineCount, string type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LineCount = lineCount;
            
            // Converter string para enum
            switch (type.ToLower())
            {
                case "comedy":
                    Type = PlayType.Comedy;
                    break;
                case "tragedy":
                    Type = PlayType.Tragedy;
                    break;
                case "history":
                    Type = PlayType.History;
                    break;
                default:
                    throw new ArgumentException($"Tipo de peça desconhecido: {type}");
            }
        }
    }
}
