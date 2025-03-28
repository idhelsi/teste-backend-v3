namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Performance
    {
        public string PlayID { get; }
        public int Audience { get; }
        public Play? Play { get; set; } // Pode ser nulo inicialmente

        // Construtor para performance com ID da peça e audiência (usado nos testes)
        public Performance(string playID, int audience)
        {
            PlayID = playID ?? throw new ArgumentNullException(nameof(playID));
            Audience = audience >= 0 ? audience : throw new ArgumentException("Número de espectadores não pode ser negativo.");
        }

        // Construtor para performance com objeto Play e audiência
        public Performance(Play play, int audience)
        {
            Play = play ?? throw new ArgumentNullException(nameof(play));
            PlayID = "custom"; // Um ID personalizado para esse tipo de construção
            Audience = audience >= 0 ? audience : throw new ArgumentException("Número de espectadores não pode ser negativo.");
        }
    }
}
