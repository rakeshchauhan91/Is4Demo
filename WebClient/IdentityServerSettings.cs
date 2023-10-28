﻿using Microsoft.Extensions.Options;

namespace WebClient
{
    public class IdentityServerSettings
    {
        public const string SectionName = "IdentityServerSettings";
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        
    }

    public class IdentityServerSettingOptions : IConfigureOptions<IdentityServerSettings>
    {
        private readonly IConfiguration _configuration;
        public IdentityServerSettingOptions(IConfiguration config)
        {
            _configuration = config;
        }
        public void Configure(IdentityServerSettings options)
        {
            _configuration.GetSection(IdentityServerSettings.SectionName).Bind(options);
        }
    }
}