using BusineesLayer.Interface;
using BusineesLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using StackExchange.Redis;
using System.Web;



namespace FundooNotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        const string policyName = "AlloweOrigin";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<IUserRL, UserRL>();
            services.AddScoped<INotesBL, NotesBL>();
            services.AddScoped<INotesRL, NotesRL>();
            services.AddScoped<ILableBL, LableBL>();
            services.AddScoped<ILableRL, LableRL>();           

            services.AddScoped<INoteLabelBL, NoteLabelBL>();
            services.AddScoped<INoteLabelRL, NoteLabelRL>();
            //access data from environmentvariavble
            // services.AddDbContext<FundooNotesContext>(option => option.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStringDB")));
            //access data from appsetting
            services.AddDbContext<FundooNotesContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:UserDB"]));


            //Addjwt authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,       
                    ValidIssuer = Environment.GetEnvironmentVariable("jwtValidIssuer"),
                    ValidAudience = Environment.GetEnvironmentVariable("jwtValidAudience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwtSecretKey")))

                };
            });

           


            //addSwagger
            services.AddSwaggerGen(options=>
            {
                options.AddSecurityDefinition(
                    name: JwtBearerDefaults.AuthenticationScheme,
                    securityScheme:new OpenApiSecurityScheme{
                        Name="Authorization",
                        Description="Enter the Bearer Authoriztion : `Bearer Generated-Token`",
                        In= ParameterLocation.Header,
                        Type=SecuritySchemeType.ApiKey,
                        Scheme="Bearer"
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        }
                    },new string[] {}
                    }
                });
                
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                       Version = "v1",
                       Title ="Implement Swagger",
                       Description = $"Fundoo Notes APIs",
                          
                    });
            });

            //session management 
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);// Session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            //adding cors

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

            });

            //redis configuratuion
            services.AddStackExchangeRedisCache(options =>
            { 
                options.Configuration = "localhost:6379";
                
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //use Core
            
           
            //use Session
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            
            //SwaggerAdded
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });
        }
    }
}
