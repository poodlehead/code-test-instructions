namespace BusinessLogic.VisitorModel.CreateUrl
{
    public interface IUrlShortnerCreateResultVisitor<out T>
    {
        T Visit(SuccessUrlShortnerCreateResult success);

        T Visit(FailedShortnerCreateResult failedShortnerCreateResult);
    }
}