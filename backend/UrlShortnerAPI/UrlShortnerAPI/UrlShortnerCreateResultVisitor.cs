using BusinessLogic.VisitorModel.CreateUrl;

namespace UrlShortnerAPI
{
    public sealed class UrlShortnerCreateResultVisitor : IUrlShortnerCreateResultVisitor<IResult>
    {
        public IResult Visit(SuccessUrlShortnerCreateResult success)
        {
            return TypedResults.Ok(success.ReturnedUrl);
        }

        public IResult Visit(FailedShortnerCreateResult failedShortnerCreateResult)
        {
            return TypedResults.BadRequest(failedShortnerCreateResult.ErrorMessage);
        }
    }
}
