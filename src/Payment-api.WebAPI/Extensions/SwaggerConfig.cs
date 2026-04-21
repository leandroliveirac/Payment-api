using Microsoft.OpenApi;

namespace Payment_api.WebAPI.Extensions;

public static class SwaggerConfig
{
    public static void AddSwaggerDocs(this IServiceCollection services)
    {
        // Configuração Robusta do Swagger/OpenAPI
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ecommerce API",
                Version = "v1.0",
                Description = "API de alta performance para Ecommerce.",
                Contact = new OpenApiContact
                {
                    Name = "Suporte Técnico",
                    Email = "tech-support@suaempresa.com.br",
                    Url = new Uri("https://www.suaempresa.com.br"),
                }
            });

            // Configuração de Segurança (Fundamentais para credibilidade)
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insira o token JWT desta forma: Bearer {seu_token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });

            // Carrega os comentários XML para o Scalar ler
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        });
    }

    public static void UseSwaggerDocs(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        });        
    }
}
