namespace BusinessLogic.VisitorModel.DeleteUrl
{
    public interface IUrlDeleteVisitor<out T>
    {
        T Visit(SuccessDeleteResult success);

        T Visit(FailedDeleteResult failedShortnerCreateResult);
    }
}