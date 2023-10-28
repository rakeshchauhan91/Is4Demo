using Microsoft.IdentityModel.Tokens;
using Serilog;
using static Api.IdentityServerSettings;

namespace Api
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddOptions<IdentityServerSettingOptions>();

            builder.Services.AddAuthentication("Bearer")
                 .AddJwtBearer("Bearer", options =>
            {
                options.Authority = config[$"{IdentityServerSettings.SectionName}:Authority"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });
            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers().RequireAuthorization("ApiScope"); ;

            return app;
        }
        public static IServiceCollection ConfigureAPI(this IServiceCollection services, IConfiguration config)
        {

            return services;

        }
    }
}
