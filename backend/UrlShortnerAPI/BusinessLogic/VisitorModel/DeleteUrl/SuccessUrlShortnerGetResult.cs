namespace BusinessLogic.VisitorModel.DeleteUrl
{
    public class SuccessDeleteResult : UrlDeleteResult
    {
        public SuccessDeleteResult()
        {
            
        }

        public override T Accept<T>(IUrlDeleteVisitor<T> visitor) => visitor.Visit(this);
    }
}