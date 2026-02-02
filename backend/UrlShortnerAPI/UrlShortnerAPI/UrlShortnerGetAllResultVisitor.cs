using BusinessLogic.VisitorModel.GetAllUrl;

namespace UrlShortnerAPI
{
    public sealed class UrlShortnerGetAllResultVisitor : IUrlShortnerGetAllResultVisitor<IResult>
    {
        public IResult Visit(SuccessUrlShortnerGetAllResult success)
        {
            return TypedResults.Ok(success);
        }
    }
}
