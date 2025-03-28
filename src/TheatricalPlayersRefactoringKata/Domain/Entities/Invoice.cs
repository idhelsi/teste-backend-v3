namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Invoice
    {
        public string Customer { get; }
        public List<Performance> Performances { get; }

        public Invoice(string customer, List<Performance> performances)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Performances = performances ?? throw new ArgumentNullException(nameof(performances));
        }
    }
} 