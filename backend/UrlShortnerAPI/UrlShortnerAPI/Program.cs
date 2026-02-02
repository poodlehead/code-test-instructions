using BusinessLogic.DependencyInjection;
using UrlShortnerAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ConfigureLogging();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProblemDetails()
    .AddResultVisitorServices()
    .ConfigureJsonOptions()
    .AddBusinessLogicServices();

if (builder.Environment.IsDevelopment())
{
    builder.ConfigureSwaggerServices();
}

var app = builder.Build();
app.ConfigureErrorResponses();
app.MapUrlShortnerMapperRoutes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.ConfigureSwagger();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
