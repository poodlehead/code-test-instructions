namespace BusinessLogic.VisitorModel.DeleteUrl
{
    public abstract class UrlDeleteResult
    {
        public abstract T Accept<T>(IUrlDeleteVisitor<T> visitor);
    }
}