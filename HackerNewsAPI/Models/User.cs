namespace HackerNewsApi.Models
{
    public class User
    {
        public string Id { get; set; }          // username (case-sensitive)
        public long Created { get; set; }       // Unix timestamp
        public int Karma { get; set; }          // karma score
        public string About { get; set; }       // optional description (HTML)
        public List<int> Submitted { get; set; } // list of item IDs

        // Propriedade calculada para expor a data de criação como DateTime
        public DateTime CreatedAsDate =>
            DateTimeOffset.FromUnixTimeSeconds(Created).UtcDateTime;
    }
}
