using EasyLoginBase.CrossCutting.DependencyInjection;
using EasyLoginBase.Extensions;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.ConfigureDependenciesRepository(builder.Configuration);

//configurar json
builder.Services.AddControllers()
.AddJsonOptions(options =>
{

    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
})
.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

WebApplication app = builder.Build();

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors(builder => builder
   .SetIsOriginAllowed(orign => true)
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowCredentials());
// Map controllers
app.MapControllers();

// Ensure the Resources directory exists and serve static files from it
string resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
if (!Directory.Exists(resourcesPath))
    Directory.CreateDirectory(resourcesPath);

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});

app.Run();
