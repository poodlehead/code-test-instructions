using Data.Data;

namespace BusinessLogic.VisitorModel.CreateUrl
{
    public class SuccessUrlShortnerCreateResult : UrlShortnerCreateResult
    {
        public SuccessUrlShortnerCreateResult(ShortenedReturnUrl shortenedUrl)
        {
            ReturnedUrl = shortenedUrl;
        }

        public ShortenedReturnUrl ReturnedUrl { get; }

        public override T Accept<T>(IUrlShortnerCreateResultVisitor<T> visitor) => visitor.Visit(this);
    }
}