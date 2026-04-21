namespace HackerNewsApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public bool? Deleted { get; set; }
        public string Type { get; set; }
        public string By { get; set; }
        public long Time { get; set; }   // <-- Unix timestamp
        public string Text { get; set; }
        public bool? Dead { get; set; }
        public int? Parent { get; set; }
        public int? Poll { get; set; }
        public List<int> Kids { get; set; }
        public string Url { get; set; }
        public int? Score { get; set; }
        public string Title { get; set; }
        public List<int> Parts { get; set; }
        public int? Descendants { get; set; }

        // Propriedade calculada para expor como DateTime
        public DateTime TimeAsDate =>
            DateTimeOffset.FromUnixTimeSeconds(Time).UtcDateTime;
    }
}
