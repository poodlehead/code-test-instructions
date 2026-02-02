namespace BusinessLogic.VisitorModel.GetAllUrl
{
    public abstract class UrlShortnerGetAllResult
    {
        public abstract T Accept<T>(IUrlShortnerGetAllResultVisitor<T> visitor);
    }
}