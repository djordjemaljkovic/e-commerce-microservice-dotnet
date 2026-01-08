

using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Services Container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationPipeline<,>)); //centralized validation preprocessor
    cfg.AddOpenBehavior(typeof(LoggerPipeline<,>)); //centralized logging postprocessor
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{
    //Connection to the database
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();


//HTTP Request Pipeline, Middlewares
app.MapCarter();

//app.UseExceptionHandler(excHandlerApp =>
//{
//    excHandlerApp.Run(async context =>
//    {
//        var exc = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (exc == null) return;

//        var problemDetails = new ProblemDetails
//        {
//            Title = exc.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exc.StackTrace
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exc, exc.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
