using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TuristickaAgencija.EF;
using TuristickaAgencija.Repository;

namespace TuristickaAgencija
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
            services.AddDbContext<MojContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("cs1")));

            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            services.AddTransient<IPutovanjaRepository, PutovanjaRepository>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSignalR(config =>
            {
                config.MapHub<SignalServer>("/signalServer");
            });

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    "default",
                     "{controller=Autentifikacija}/{action=Index}/{id?}"
                //"{controller=Home}/{action=Index}/{id?}"
                );
            });
        }


    }
}
