using ApiCommunication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using ConfigClient;

namespace RestApi
{
    public class Startup
    {
        private readonly IApiCall _ApiCall;
        private readonly IConfiguration _Configuration;
        private readonly IConfigProvider _ConfigProvider;

        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;

            var appName = _Configuration.GetValue<string>("ApplicationName");
            var metricsUrl = _Configuration.GetValue<string>("Metrics:UrlEndpoint");
            var apiKey = _Configuration.GetValue<string>("Metrics:ApiKey");
            var apiCallTimeoutinMs = _Configuration.GetValue<int>("ApiCall:TimeoutInMilliseconds");

            // Set up ApiCall object
            _ApiCall = new ApiCall(apiCallTimeoutinMs);

            // populate config
            _ConfigProvider = new ConfigProvider();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appName = _Configuration.GetValue<string>("ApplicationName");
            var version = _Configuration.GetValue<string>("Swagger:Version");

            if (_Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{appName} Documentation {version}",
                    });
                });
            }

            services.AddControllers();
            services.AddSingleton(_ApiCall);
            services.AddSingleton(_Configuration);
            services.AddSingleton(_ConfigProvider);

            services.AddMvc(options =>
            {}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            if (_Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
        }
    }
}
