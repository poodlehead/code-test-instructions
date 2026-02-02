using BusinessLogic.VisitorModel;
using BusinessLogic.VisitorModel.CreateUrl;
using BusinessLogic.VisitorModel.DeleteUrl;
using BusinessLogic.VisitorModel.GetAllUrl;
using BusinessLogic.VisitorModel.GetUrl;
using Data;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UrlShortnerAPI;

namespace BusinessLogic
{
    internal sealed partial class UrlShortnerService : IUrlShortnerService
    {
        private readonly UrlShortnerContext _context;
        public UrlShortnerService(
            UrlShortnerContext context)
        {
            _context = context;
        }
        public async Task<UrlShortnerCreateResult> CreateUrlShortner(UrlShortnerRequest request)
        {
            string shortenedUrl = string.IsNullOrEmpty(request.customAlias) ? Guid.NewGuid().ToString("N") : request.customAlias;
            var data = new Data.Data.ShortenedUrl
            {
                shortString = shortenedUrl,
                Url = request.fullUrl,
            };
            try
            {
                _context.ShortenedUrls.Add(data);
                await _context.SaveChangesAsync();
                return new SuccessUrlShortnerCreateResult(new ShortenedReturnUrl { Alias = shortenedUrl });
            }
            catch
            {
                return new FailedShortnerCreateResult("Invalid input or alias already taken");
            }
        }

        public async Task<UrlShortnerGetResult> GetUrlShortner(string alias)
        {
            try
            {
                var result = await _context.ShortenedUrls
                                    .SingleAsync(x => x.shortString == alias);

                return new SuccessUrlShortnerGetResult(new OriginalUrl { Url = result.Url });
            }
            catch
            {
                return new FailedShortnerGetResult("Alias not found");
            }
        }
        public async Task<UrlDeleteResult> DeleteUrlShortner(string alias)
        {
            try
            {
                var removedItem = await _context.ShortenedUrls.SingleAsync(x => x.shortString == alias);
                _context.ShortenedUrls.Remove(removedItem);
                await _context.SaveChangesAsync();

                return new SuccessDeleteResult();
            }
            catch
            {
                return new FailedDeleteResult("Alias wasn't found");
            }
        }

        public async Task<UrlShortnerGetAllResult> GetAllUrls(string baseUrl)
        {
            List<ShortenedUrl> UrlList = await _context.ShortenedUrls.ToListAsync();
            var result = UrlList.Select(x => new UrlShortenedDTO()
            {
                fullUrl = x.Url,
                shortUrl = baseUrl + x.shortString
            }).ToList();
            return new SuccessUrlShortnerGetAllResult(result);
        }
    }
}
