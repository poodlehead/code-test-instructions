using System.ComponentModel.DataAnnotations;

namespace Data.Data
{
    public class ShortenedUrl
    {
        [Key]
        public required string shortString { get; set; }
        public required string Url { get; set; }
        public string? Alias { get; set; }
    }
}
