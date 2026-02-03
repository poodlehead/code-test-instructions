using BusinessLogic.VisitorModel.CreateUrl;
using BusinessLogic.VisitorModel.DeleteUrl;
using BusinessLogic.VisitorModel.GetAllUrl;
using BusinessLogic.VisitorModel.GetUrl;
using UrlShortnerAPI;

namespace BusinessLogic
{
    public interface IUrlShortnerService
    {
        Task<UrlShortnerCreateResult> CreateUrlShortner(UrlShortnerRequest request, string? frontEndUrl);
        Task<UrlShortnerGetResult> GetUrlShortner(string alias);
        Task<UrlDeleteResult> DeleteUrlShortner(string alias);
        Task<UrlShortnerGetAllResult> GetAllUrls();
    }
}