namespace BusinessLogic.VisitorModel.DeleteUrl
{
    public class FailedDeleteResult : UrlDeleteResult
    {
        public FailedDeleteResult(string message)
        {
            ErrorMessage = message;
        }
        public string ErrorMessage { get; }
        public override T Accept<T>(IUrlDeleteVisitor<T> visitor) => visitor.Visit(this);
    }
}
