using System.Security.Principal;

namespace ProniaPrototype.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string CollectionTitle { get; set; }
        public string MainTitle { get; set; }
        public string? ImageUrl { get; set; }
    }
}
