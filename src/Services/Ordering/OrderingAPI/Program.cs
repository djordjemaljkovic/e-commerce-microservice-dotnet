using OrderingAPI;
using OrderingApplication;
using OrderingInfrastructure;
using OrderingInfrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Services 
builder.Services.AddApplicationServices(builder.Configuration).AddInfrastructureServices(builder.Configuration).AddApiServices(builder.Configuration);  
var app = builder.Build();

//Http Pipeline and Middleware
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitDatabaseAsync();
}

app.Run();
