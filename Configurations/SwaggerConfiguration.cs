using Microsoft.OpenApi.Models;

namespace zintersid.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ZProject API",
                    Description = "An example API for demonstration purposes"
                });

                // Additional Swagger configuration can be added here
                // Customize the Swagger document to use kebab-case paths
                c.DocumentFilter<KebabCaseDocumentFilter>();
            });
        }
    }
}