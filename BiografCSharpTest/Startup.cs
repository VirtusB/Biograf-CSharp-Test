using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Data.Seeding;
using BiografCSharpTest.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BiografCSharpTest.Services;
using BiografCSharpTest.Hubs;

namespace BiografCSharpTest
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
            

            services.AddSignalR();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => {
                        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            
            services.AddDbContext<BioContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddCors(); 
            services.AddAutoMapper();

            services.AddTransient<Seed>();

            services.AddCors();

            Mapper.Reset(); // skal ikke bruges i produktion, nødvendig for at kunne droppe databasen, bug i dotnet cli eller automapper
            services.AddAutoMapper();


            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            services.AddScoped<IUserService, UserService>(); // er det sådan man skal tilføje services? er dette overhovedet en service?? <-- er det "domain logic"? hvordan og hvor skal det laves??

            services.AddScoped<LogUserActivity>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();

                app.UseExceptionHandler(builder => { // global exception handling.
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null) {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            
            app.UseCors(x => x.WithOrigins("http://localhost:4200", "http://127.0.0.1").AllowAnyMethod().AllowAnyHeader().AllowCredentials()); 
           // app.UseHttpsRedirection();
           app.UseAuthentication();

           app.UseSignalR(routes => {
                routes.MapHub<StaffChatHub>("/staffchat");
            });
           
           seeder.SeedRoles();
           seeder.SeedUsers();
           seeder.SeedDiscounts();
           seeder.SeedMovies();
           seeder.SeedFavorites();
           seeder.SeedShows();
           seeder.SeedReservations();
            // seeder.CleanDatabase();

            app.UseMvc();
        }
    }
}
