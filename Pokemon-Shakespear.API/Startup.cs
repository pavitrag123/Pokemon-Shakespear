using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pokemon_Shakespear.Business.Interfaces;
using Pokemon_Shakespear.Business.Services;
using Pokemon_Shakespear.Business.Wrappers;
using Pokemon_Shakespear.Models.Domain;
using System;

namespace Pokemon_Shakespear.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private AppSettings _appSettings { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _appSettings = Configuration.Get<AppSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins(_appSettings.AllowedOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddSingleton(_appSettings);
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddMemoryCache();

            services.AddSingleton<IPokemonTranslationService, PokemonTranslationService>();
            services.AddSingleton<IPokemonApiService, PokemonApiService>();
            services.AddSingleton<IShakespeareApiService, ShakespeareApiService>();

            services.AddSingleton<IPokemonApiClientWrapper, PokemonAPIClientWrapper>();
            services.AddHttpClient().AddSingleton<IHttpClientWrapper, HttpClientWrapper>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
