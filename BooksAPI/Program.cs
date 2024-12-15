using Microsoft.EntityFrameworkCore;
using BooksAPI.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-9.0#cors-with-named-policy-and-middleware
var AllowedSpecificOrigins = "AllowedSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: AllowedSpecificOrigins,
    policy =>
    {
      policy.WithOrigins("http://localhost:5173", "https://books-react-three.vercel.app")
      .AllowAnyHeader()
      .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.Configure<RouteOptions>(options =>
{
  options.LowercaseUrls = true;
  options.AppendTrailingSlash = false;
});

// Database Connection
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
  builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
  connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
  connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<ReviewContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors(AllowedSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
