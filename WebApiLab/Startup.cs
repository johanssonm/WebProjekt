using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Services;
using WebApiLab.Controllers;

namespace WebApiLab
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            using (var client = new NewsContext())
            {
                client.Database.EnsureDeleted();
                client.Database.EnsureCreated();

                NewsController.SeedTheCategories();

                // NewsController.SeedTheAuthors();

            }

            WeatherCodes.Init(); //Initializing the weathercodes dictionary.
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<NewsContext>();
            services.AddSingleton<IWeatherService, OpenWeatherMapService>();
            services.AddMvc();
        }

        //public void ConfigureServices(IServiceCollection service)
        //{
        //    var container = new ServiceContainer();

        //    container.Register<IWeatherService, OpenWeatherMapService>();
        //    container.Register<IServiceCollection, ServiceCollection>();
        //    container.r

        //    service.AddMvc();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseStatusCodePages();
                app.UseDirectoryBrowser();
            }

            app.UseMvc();
        }
    }
}
