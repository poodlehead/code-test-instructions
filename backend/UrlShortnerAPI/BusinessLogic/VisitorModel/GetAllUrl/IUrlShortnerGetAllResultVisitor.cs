namespace BusinessLogic.VisitorModel.GetAllUrl
{
    public interface IUrlShortnerGetAllResultVisitor<out T>
    {
        T Visit(SuccessUrlShortnerGetAllResult success);
    }
}