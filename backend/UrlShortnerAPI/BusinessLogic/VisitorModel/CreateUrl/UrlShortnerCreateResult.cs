namespace BusinessLogic.VisitorModel.CreateUrl
{
    public abstract class UrlShortnerCreateResult
    {
        public abstract T Accept<T>(IUrlShortnerCreateResultVisitor<T> visitor);
    }
}