using System.IdentityModel.Tokens.Jwt;

namespace WebClient
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureApplication(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            builder.Services.AddOptions<IdentityServerSettingOptions>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = config[$"{IdentityServerSettings.SectionName}:Authority"]; ;

                options.ClientId = config[$"{IdentityServerSettings.SectionName}:ClientId"]; ;
                options.ClientSecret = config[$"{IdentityServerSettings.SectionName}:ClientSecret"];
                options.ResponseType = "code";
                
                
                //, ASP.NET Core will automatically store the id, access, and refresh tokens in the properties of the authentication cookie
                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("api1");
                options.Scope.Add("offline_access");
                options.GetClaimsFromUserInfoEndpoint = true;
            });
            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapRazorPages().RequireAuthorization();  
            return app;
        }
    }
}
