using AutoMapper;
using masa_backend.ModelViews;
using masa_backend.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddDbContext<masa_backend.MasaDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper); 
builder.Services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<masa_backend.MasaDbContext>();
    context.Database.Migrate();
    context.Database.EnsureCreated();
}
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(),
                                   "wwwroot", "Files")),
        RequestPath = "/Files"
    });
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers()
             .RequireCors(MyAllowSpecificOrigins);
});
app.MapGet("/", () => "Masa Bank!");

app.Run();
