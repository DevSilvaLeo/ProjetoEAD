using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto.Data.Contracts;
using Projeto.Data.Repositories;

namespace Projeto.Servicos
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);

            var connectionString = Configuration.GetConnectionString("eadBD");

            services.AddTransient<IUsuarioRepository, UsuarioRepository>(map => new UsuarioRepository(connectionString));

            services.AddSwaggerGen(
                swagger =>
                {
                    
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "API de acesso a escola virtual",
                        Version = "v1"
                    });

                   // swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                }
            );

            services.AddCors();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(services =>
                {
                    services.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    services.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
                .AddJwtBearer(services =>
                    {
                        services.RequireHttpsMetadata = false;
                        services.SaveToken = true;
                        services.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                swagger =>
                {
                    swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto");
                }
            );

            app.UseCors(x => x
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc(); 


        }
    }
}
