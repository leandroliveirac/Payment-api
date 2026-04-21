using Payment_api.Infra.IOC;
using Payment_api.WebAPI.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerDocs();
builder.Services.AddScalar();

var app = builder.Build();

app.UseSwaggerDocs();
app.UseScalarDocs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
