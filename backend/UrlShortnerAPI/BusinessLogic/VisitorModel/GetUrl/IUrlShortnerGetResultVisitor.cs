namespace BusinessLogic.VisitorModel.GetUrl
{
    public interface IUrlShortnerGetResultVisitor<out T>
    {
        T Visit(SuccessUrlShortnerGetResult success);

        T Visit(FailedShortnerGetResult failedShortnerGetResult);
    }
}