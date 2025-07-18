using Application;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System.Net.Http.Headers;
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

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddHttpClient<IWorkshopService, WorkshopService>(client =>
            {
                client.BaseAddress = new Uri("https://dev.tecnomcrm.com/api/v1/");
                // Si quieres, puedes agregar encabezados por defecto:
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp")
                );
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", credentials);
            });

            services.AddControllers();

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
