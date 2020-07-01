using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.API.Data;

namespace SmartSchool.API
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
            services.AddDbContext<DataContext>(
                context => context.UseSqlite( Configuration.GetConnectionString("Default"))
            );

            //services.AddSingleton<IRepository,Repository>();//CRIAR UMA UNICA INSTANCIA QUANDO PE SOLICITADO PELA PRIMEIRA VEZ E REUTILIZA QUANDO O SERVIÇO É NCESSÁRIO
            //services.AddTransient<IRepository,Repository>();//SEMPRE VAI CRIAR UMA NOVA INSTANCIA PARA CADE ITEM ENCONTRANDO QUE POSSUA A DEPENDENNCIA, SE TIVER 5 DEPENTENDE IRA GERAR 5 INSTANCIAS
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository,Repository>();//REAPROVEITA A DEPENDENCIA, A CADE REQUISIÇÃO DO CONTROLE CRIA UMA NOVA
            
            services.AddControllers()
                .AddNewtonsoftJson(
                    opt => opt.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddVersionedApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options => 
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
            var apiProviderDescription = services.BuildServiceProvider()
                                        .GetService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(options => {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "SmartSchool API",
                            Version = description.ApiVersion.ToString(),
                            TermsOfService = new Uri("http://SeusTermosDeUso.com"),
                            Description = "A descrição da WebAPI do SmartSchool",
                            License = new Microsoft.OpenApi.Models.OpenApiLicense
                            {
                                Name = "SmartSchool License",
                                Url = new Uri("http://mit.com")
                            },
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                Name = "João Claudino",
                                Email = "joao@claudinosoftware.com.br",
                                Url = new Uri("http://claudinosoftware.com.br")
                            }
                        }    
                    );
                }

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider apiProviderDescription)
        {
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "€";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                   {                       
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json", 
                            description.GroupName.ToUpperInvariant());
                   }
                   options.RoutePrefix = "";
               });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
