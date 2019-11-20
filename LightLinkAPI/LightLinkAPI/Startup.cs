using LightLinkLibrary.Data_Access;
using LightLinkLibrary.Data_Access.Implementations;
using LightLinkLibrary.Data_Access.Implementations.Decorators;
using LightLinkModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

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
            var service = new DummySuperService();
            services.AddSingleton<IUserService>((c) => service);
            services.AddSingleton<IProfileService>((c) => new FileProfileService());
            services.AddSingleton<IComputerService>((c) => service);
            services.AddSingleton<ILoginAuthenticator>((c) => service);
            SeedUsers(service);
            services.AddAuthorization((config) =>
            {
                config.AddPolicy("UserPolicy", (builder) =>
                {
                    builder.RequireRole("User");
                });
            });
            services.AddAuthentication((config) =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((config) =>
            {
                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = context.Principal.Identity.Name;
                        var user = userService.GetUserById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ITS A FUCKING SECRET ALEX GOD GIT BETTER")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        }

        private void SeedUsers(IUserService userService)
        {
            var nullUser = new User { Id = ObjectId.GenerateNewId(), UserName = "Null", Password = "Null" };
            if (!userService.Exists(nullUser.UserName))
            {
                userService.AddUser(nullUser);
            }
            var me = new User
            {
                Id = ObjectId.GenerateNewId(),
                UserName = "gxldcptrick",
                Password = "Not A Secure Password",
                Profiles = new List<Profile>() {
                GenerateProfile()
            }
            };

            var otherme = new User
            {
                Id = ObjectId.GenerateNewId(),
                UserName = "user",
                Password = "pass",
                Profiles = new List<Profile>() {
                GenerateProfile()
            }
            };
            if (!userService.Exists(me.UserName))
            {
                userService.AddUser(me);
            }
            if (!userService.Exists(otherme.UserName))
            {
                userService.AddUser(otherme);
            }
        }


        private static int count = 1;
        private Profile GenerateProfile()
        {
            string name = "account " + count;
            var profile = new Profile();
            profile.IsActive = true;
            profile.Name = name;
            profile.Configurations = new Dictionary<string, dynamic>();
            return profile;
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

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
