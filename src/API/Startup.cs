using Application;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Polly.Extensions.Http;
using Polly;
using System.Text;

namespace API
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
            // Add services to the container.
            services.AddApplicationServices(Configuration);

            // Registrar EF Core InMemory para leads
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("LeadsDatabase"));

            // Registrar el repositorios
            services.AddScoped<ILeadRepository, LeadRepository>();

            // Registrar el servicios
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<IWorkshopService, WorkshopService>();

            services.AddMemoryCache();

            services.AddHttpClient<IHttpClientService, HttpClientService>((serviceProvider, client) =>
            {
                string url = Configuration["WorkshopApi:Url"];
                string username = Configuration["WorkshopApi:Username"];
                string password = Configuration["WorkshopApi:Password"];

                client.BaseAddress = new Uri(url);
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{username}:{password}")
                );
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            })
            // 1) Timeout: falla si tarda >10s
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
            // 2) Circuit‑Breaker: tras 2 fallos seguidos (5xx, 4xx o excepción), rompe 30s
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                )
            );

            services.AddControllers();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<LeadRequestValidator>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
