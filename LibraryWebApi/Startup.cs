using Library.Database;
using LibraryWebApi.Services.Books;
using LibraryWebApi.Services.Books.Searchers;
using LibraryWebApi.Services.DVDs;
using LibraryWebApi.Services.DVDs.Searchers;
using LibraryWebApi.Services.Magazines;
using LibraryWebApi.Services.Magazines.Searchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LibraryWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version()));
            });

            ConfigureBookServices(services);
            ConfigureDVDServices(services);
            ConfigureMagazineServices(services);
            services.AddControllers();
        }

        private void ConfigureDVDServices(IServiceCollection services)
        {
            services.AddScoped<IDVDSearchServices, DVDSearchServices>();
            services.AddScoped<IDVDServies, DVDServices>();
        }

        private void ConfigureMagazineServices(IServiceCollection services)
        {
            services.AddScoped<IMagazineServices, MagazineServices>();
            services.AddScoped<IMagazineSearchServices, MagazineSearchServices>();
        }

        public void ConfigureBookServices(IServiceCollection services)
        {
            services.AddScoped<IBookSearchServices, BookSearchServices>();
            services.AddScoped<IAuthorSearchServices, AuthorSearchServices>();
            services.AddScoped<IBookServices, BookServices>();

            services.AddScoped<IAuthorServices, AuthorServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}