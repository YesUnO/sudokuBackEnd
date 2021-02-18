using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using sudokuBackEnd.Services;
using sudokuBackEnd.Services.ServiceInterfaces;

namespace sudokuBackEnd
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
            //cors
            services.AddCors(x => x.AddPolicy("MyPolicy", builder =>
             {
                 builder.AllowAnyOrigin()
                        .WithOrigins("http://saras-sudoku.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
             }));
            services.AddDbContext<SudokuContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection"),
                b=>b.MigrationsAssembly(typeof(SudokuContext).Assembly.FullName)));

            //swagger
            services.AddSwaggerDocument();

            //oAuth
            var key = Encoding.ASCII.GetBytes($"{Configuration["Auth:Secret"]}");
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options=> {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //DI services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserGameSolutionService, UserGameSolutionService>();
            services.AddScoped<IGameEnteringService, GameEnteringService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //cors
            app.UseCors("MyPolicy");

            app.UseRouting();

            //oAuth
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
