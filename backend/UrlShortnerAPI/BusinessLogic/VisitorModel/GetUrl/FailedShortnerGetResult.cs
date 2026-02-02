namespace BusinessLogic.VisitorModel.GetUrl
{
    public class FailedShortnerGetResult : UrlShortnerGetResult
    {
        public FailedShortnerGetResult(string message) 
        {
            ErrorMessage = message;
        }

        public string ErrorMessage { get; set; }

        public override T Accept<T>(IUrlShortnerGetResultVisitor<T> visitor) => visitor.Visit(this);
    }
}
