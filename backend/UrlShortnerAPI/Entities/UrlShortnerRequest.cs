namespace UrlShortnerAPI
{
    public sealed record UrlShortnerRequest
    {
        public required string fullUrl {  get; set; }
        public string customAlias { get; set; }
    }
}
