using BusinessLogic.VisitorModel.GetUrl;

namespace BusinessLogic.VisitorModel.GetAllUrl
{
    public class SuccessUrlShortnerGetAllResult : UrlShortnerGetAllResult
    {
        public SuccessUrlShortnerGetAllResult(List<UrlShortenedDTO> urlList)
        {
            UrlList = urlList;
        }

        public List<UrlShortenedDTO> UrlList { get; }

        public override T Accept<T>(IUrlShortnerGetAllResultVisitor<T> visitor) => visitor.Visit(this);
    }
}