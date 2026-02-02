namespace BusinessLogic.VisitorModel.CreateUrl
{
    public class FailedShortnerCreateResult : UrlShortnerCreateResult
    {
        public FailedShortnerCreateResult(string message)
        {
            ErrorMessage = message;
        }
        public string ErrorMessage { get; }
        public override T Accept<T>(IUrlShortnerCreateResultVisitor<T> visitor) => visitor.Visit(this);
    }
}
