using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit;
using NUnit.Framework;
using Data;
using Data.Data;
using BusinessLogic.VisitorModel.CreateUrl;
using BusinessLogic.VisitorModel.GetUrl;
using BusinessLogic.VisitorModel.GetAllUrl;
using BusinessLogic.VisitorModel.DeleteUrl;
using BusinessLogic;
using UrlShortnerAPI;

namespace BusinessLogic.Tests
{
    [TestFixture]
    public class UrlShortnerServiceTests
    {
        private UrlShortnerContext CreateContext(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<UrlShortnerContext>()
                .UseSqlite(connection)
                .Options;

            var context = new UrlShortnerContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        private UrlShortnerService CreateService(UrlShortnerContext context) =>
            new UrlShortnerService(context);

        [Test]
        public async Task CreateUrlShortner_WhenFrontEndUrlMissing_ReturnsFailed()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);

            var service = CreateService(context);

            var request = new UrlShortnerRequest { fullUrl = "http://example.com", customAlias = null };

            //Act
            var result = await service.CreateUrlShortner(request, null);

            //Assert
            Assert.That(result, Is.TypeOf<FailedShortnerCreateResult>());
        }

        [Test]
        public async Task CreateUrlShortner_WithoutCustomAlias_SavesAndReturnsShortenedUrl()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);
            var service = CreateService(context);
            var request = new UrlShortnerRequest { fullUrl = "http://example.com", customAlias = null };
            var frontEnd = "https://short.ly/";
            
            //Act
            var result = await service.CreateUrlShortner(request, frontEnd);
            
            //Assert
            Assert.That(result, Is.InstanceOf<SuccessUrlShortnerCreateResult>());
            var success = (SuccessUrlShortnerCreateResult)result!;
            Assert.That(success.ReturnedUrl.Alias.StartsWith(frontEnd));
            var alias = success.ReturnedUrl.Alias.Substring(frontEnd.Length);
            var persisted = await context.ShortenedUrls.FindAsync(alias);
            Assert.That(persisted, Is.Not.Null);
            Assert.That(persisted!.Url, Is.EqualTo("http://example.com"));
        }

        [Test]
        public async Task CreateUrlShortner_WithCustomAlias_SavesAndReturnsShortenedUrl()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);

            var service = CreateService(context);

            var request = new UrlShortnerRequest { fullUrl = "http://example.com", customAlias = "aliashort1" };
            var frontEnd = "https://short.ly/";

            //Act
            var result = await service.CreateUrlShortner(request, frontEnd);

            //Assert
            Assert.That(result, Is.InstanceOf<SuccessUrlShortnerCreateResult>());
            var success = (SuccessUrlShortnerCreateResult)result!;
            Assert.That(success.ReturnedUrl.Alias, Is.EqualTo(frontEnd + "aliashort1"));

            var persisted = await context.ShortenedUrls.FindAsync("aliashort1");
            Assert.That(persisted, Is.Not.Null);
            Assert.That(persisted!.Url, Is.EqualTo("http://example.com"));
        }

        [Test]
        public async Task CreateUrlShortner_WhenAliasTaken_ReturnsFailed()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);

            // insert existing item that has the same alias
            context.ShortenedUrls.Add(new ShortenedUrl { shortString = "dup", Url = "http://orig.com", Alias = "dup" });
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var request = new UrlShortnerRequest { fullUrl = "http://example.com", customAlias = "dup" };
            var frontEnd = "https://short.ly/";

            //Act
            var result = await service.CreateUrlShortner(request, frontEnd);

            //Assert
            Assert.That(result, Is.InstanceOf<FailedShortnerCreateResult>());
        }

        [Test]
        public async Task GetUrlShortner_ReturnsOriginal()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);

            context.ShortenedUrls.Add(new ShortenedUrl { shortString = "abc123", Url = "http://original.com", Alias = "orig" });
            
            await context.SaveChangesAsync();

            var service = CreateService(context);

            //Act
            var result = await service.GetUrlShortner("abc123");

            //Assert
            Assert.That(result, Is.InstanceOf<SuccessUrlShortnerGetResult>());
            var success = (SuccessUrlShortnerGetResult)result!;
            Assert.That(success.OriginalUrl.Url, Is.EqualTo("http://original.com"));
        }

        [Test]
        public async Task DeleteUrlShortner_RemovesEntity()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);

            context.ShortenedUrls.Add(new ShortenedUrl { shortString = "deleteMe", Url = "http://delete.me", Alias = "del" });
            await context.SaveChangesAsync();

            var service = CreateService(context);

            //Act
            var result = await service.DeleteUrlShortner("deleteMe");

            //Assert
            Assert.That(result, Is.InstanceOf<SuccessDeleteResult>());

            var existing = await context.ShortenedUrls.FindAsync("deleteMe");
            Assert.That(existing, Is.Null);
        }

        [Test]
        public async Task GetAllUrls_ReturnsListWithPrefixedShortUrls()
        {
            //Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using var context = CreateContext(connection);

            context.ShortenedUrls.Add(new ShortenedUrl { shortString = "short1", Url = "http://a", Alias = "A" });
            context.ShortenedUrls.Add(new ShortenedUrl { shortString = "short2", Url = "http://b", Alias = "B" });
            await context.SaveChangesAsync();

            var service = CreateService(context);

            //Act
            var result = await service.GetAllUrls("https://short.ly/");

            //Assert
            Assert.That(result, Is.InstanceOf<SuccessUrlShortnerGetAllResult>());
            var success = (SuccessUrlShortnerGetAllResult)result!;
            Assert.That(success.UrlList.Count, Is.EqualTo(2));
            Assert.That(success.UrlList.Any(x => x.shortUrl == "https://short.ly/short1"), Is.True);
            Assert.That(success.UrlList.Any(x => x.shortUrl == "https://short.ly/short2"), Is.True);
        }
    }
}