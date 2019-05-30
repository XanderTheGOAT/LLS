using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightLinkLibrary.Data_Access;
using LightLinkLibrary.Data_Access.Implementations;
using LightLinkLibrary.Data_Access.Implementations.Decorators;
using LightLinkModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace LightLinkAPI
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
            services.AddControllers()
                .AddNewtonsoftJson();
            var service = new DummySuperService(); // new MongoSuperService("69.27.22.253");
            var userService = new HashingUserService(service);
            services.AddSingleton<IUserService>((c) => userService);
            services.AddSingleton<IProfileService>((c) => service);
            services.AddSingleton<IComputerService>((c) => service);
            SeedUsers(userService);
        }

        private void SeedUsers(IUserService userService)
        {
            var nullUser = new User { Id = ObjectId.GenerateNewId(), UserName = "Null", Password = "Null" };
            if (!userService.Exists(nullUser.UserName))
            {
                userService.AddUser(nullUser);
            }
            var me = new User { Id = ObjectId.GenerateNewId(), UserName = "gxldcptrick", Password = "Not A Secure Password" };
            if (!userService.Exists(me.UserName))
            {
                userService.AddUser(me);
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
