using MenorPreco.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MenorPreco
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
            services.AddRazorPages();

            //Adicionando o uso de MVC (Adicionei a opção para que seja aceito o uso do app.UseMvcWithDefaultRoute();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddDbContext<DatabaseContext>(options => {
                options.UseSqlite("Filename=MenorPreco.db");
            });

            //Adicionando o trabalho de BD com SQLite
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //Adicionando uso de MVC com rotas padrões
            app.UseMvcWithDefaultRoute();

            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            */
        }
    }
}
