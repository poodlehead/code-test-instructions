using BusinessLogic;
using BusinessLogic.VisitorModel.CreateUrl;
using BusinessLogic.VisitorModel.DeleteUrl;
using BusinessLogic.VisitorModel.GetAllUrl;
using BusinessLogic.VisitorModel.GetUrl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortnerAPI
{
    internal static partial class UrlShortnerRouteMapping
    {
        internal const string UrlShortnerApi = "UrlShortnerApi";

        internal static WebApplication MapUrlShortnerMapperRoutes(this WebApplication app)
        {
            string? frontEndUrl = app.Configuration.GetRequiredSection("FrontEndUrl").Value?.ToString();

            app.MapPost("/shorten", async Task<IResult> (
                [FromBody] UrlShortnerRequest requestBody,
                [FromServices] IUrlShortnerService service,
                [FromServices] IUrlShortnerCreateResultVisitor<IResult> urlShortnerCreateResultVisitor) =>
            {
                var result = await service.CreateUrlShortner(requestBody, frontEndUrl);
                return result.Accept(urlShortnerCreateResultVisitor);
            })
            .WithName("Shorten")
            .WithGroupName(UrlShortnerApi);
            
            app.MapGet("/{alias}", async Task<IResult> (
                [FromRoute] string alias,
                [FromServices] IUrlShortnerService service,
                [FromServices] IUrlShortnerGetResultVisitor<IResult> urlShortnerGetResultVisitor) =>
            {
                var result = await service.GetUrlShortner(alias);
                return result.Accept(urlShortnerGetResultVisitor);
            })
            .WithName("GetAlias")
            .WithGroupName(UrlShortnerApi);
            
            app.MapDelete("/{alias}", async Task<IResult> (
                [FromRoute] string alias,
                [FromServices] IUrlShortnerService service,
                [FromServices] IUrlDeleteVisitor<IResult> urlDeleteVisitor) =>
            {
                var result = await service.DeleteUrlShortner(alias);
                return result.Accept(urlDeleteVisitor);
            })
            .WithName("DeleteAlias")
            .WithGroupName(UrlShortnerApi);
            
            app.MapGet("/urls", async Task<IResult> (
                [FromServices] IUrlShortnerService service,
                [FromServices] IUrlShortnerGetAllResultVisitor<IResult> urlGetAll) =>
            {
                var result = await service.GetAllUrls(frontEndUrl);
                return result.Accept(urlGetAll);
            })
            .WithName("GetAllUrls")
            .WithGroupName(UrlShortnerApi);

            return app;
        }
    }
}
