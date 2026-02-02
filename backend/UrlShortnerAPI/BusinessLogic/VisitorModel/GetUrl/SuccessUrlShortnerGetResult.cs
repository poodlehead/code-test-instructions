namespace BusinessLogic.VisitorModel.GetUrl
{
    public class SuccessUrlShortnerGetResult : UrlShortnerGetResult
    {
        public SuccessUrlShortnerGetResult(OriginalUrl originalUrl)
        {
            OriginalUrl = originalUrl;
        }

        public OriginalUrl OriginalUrl { get; }

        public override T Accept<T>(IUrlShortnerGetResultVisitor<T> visitor) => visitor.Visit(this);
    }
}