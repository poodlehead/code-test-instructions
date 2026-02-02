using BusinessLogic.VisitorModel;
using BusinessLogic.VisitorModel.GetUrl;

namespace UrlShortnerAPI
{
    public sealed class UrlShortnerGetResultVisitor : IUrlShortnerGetResultVisitor<IResult>
    {
        public IResult Visit(SuccessUrlShortnerGetResult success)
        {
            return TypedResults.Redirect(success.OriginalUrl.Url);
        }

        public IResult Visit(FailedShortnerGetResult failedShortnerCreateResult)
        {
            return TypedResults.NotFound(failedShortnerCreateResult.ErrorMessage);
        }
    }
}
