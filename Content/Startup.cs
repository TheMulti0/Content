using System.Text.Json.Serialization;
using Calcalist.News;
using Calcalist.Reports;
using Content.Api;
using Content.Services;
using Haaretz.News;
using Kan.News;
using Mako.N12Reports;
using Mako.News;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheMarker.News;
using Walla.News;
using Walla.Reports;
using Ynet.News;
using Ynet.Reports;

namespace Content
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options
                        .JsonSerializerOptions
                        .Converters
                        .Add(
                            new JsonStringEnumConverter());
                });

            services.AddSingleton<ILatestNewsProvider, MakoProvider>();
            services.AddSingleton<ILatestNewsProvider, YnetProvider>();
            services.AddSingleton<ILatestNewsProvider, YnetReportsProvider>();
            services.AddSingleton<ILatestNewsProvider, CalcalistProvider>();
            services.AddSingleton<ILatestNewsProvider, CalcalistReportsProvider>();
            services.AddSingleton<ILatestNewsProvider, WallaProvider>();
            services.AddSingleton<ILatestNewsProvider, WallaReportsProvider>();
            services.AddSingleton<ILatestNewsProvider, HaaretzProvider>();
            services.AddSingleton<ILatestNewsProvider, TheMarkerProvider>();

            services.AddSingleton<IPagedNewsProvider, N12ReportsProvider>();
            services.AddSingleton<IPagedNewsProvider, KanNewsProvider>();
            services.AddSingleton<NewsService>();
            
            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
