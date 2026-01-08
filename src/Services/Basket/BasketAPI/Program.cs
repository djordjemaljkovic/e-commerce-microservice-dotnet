using DiscountGRPC;
using MainMessaging.MassTransit;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

//Services Container

//Application Services
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationPipeline<,>)); //centralized validation preprocessor
    cfg.AddOpenBehavior(typeof(LoggerPipeline<,>)); //centralized logging postprocessor
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

//Data Services
builder.Services.AddMarten(opt =>
{
    //Connection to the database
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    //identity for base props
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//Inversion of Control 
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//GRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});

//Async communication services
builder.Services.AddMessageBroker(builder.Configuration);


//Utility Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!).AddRedis(builder.Configuration.GetConnectionString("Redis")!);


var app = builder.Build();

//HTTP Request Pipeline, Middlewares
app.MapCarter();

app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
