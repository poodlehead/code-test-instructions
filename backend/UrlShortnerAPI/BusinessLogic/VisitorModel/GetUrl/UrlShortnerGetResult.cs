namespace BusinessLogic.VisitorModel.GetUrl
{
    public abstract class UrlShortnerGetResult
    {
        public abstract T Accept<T>(IUrlShortnerGetResultVisitor<T> visitor);
    }
}