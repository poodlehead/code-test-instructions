using BusinessLogic.VisitorModel;
using BusinessLogic.VisitorModel.DeleteUrl;
using BusinessLogic.VisitorModel.GetUrl;

namespace UrlShortnerAPI
{
    public sealed class UrlShortnerDeleteResultVisitor : IUrlDeleteVisitor<IResult>
    {
        public IResult Visit(SuccessDeleteResult success)
        {
            return TypedResults.NoContent();
        }

        public IResult Visit(FailedDeleteResult failedShortnerCreateResult)
        {
            return TypedResults.NotFound();
        }
    }
}
