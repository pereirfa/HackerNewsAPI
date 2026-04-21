namespace HackerNewsApi.Models
{
    public class Updates
    {
        public List<int> Items { get; set; }     // IDs de itens atualizados
        public List<string> Profiles { get; set; } // IDs de usuários atualizados
    }
}

