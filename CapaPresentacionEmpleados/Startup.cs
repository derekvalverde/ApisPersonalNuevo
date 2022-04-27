using CapaDatos.Data;
using CapaNegocio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CapaPresentacionEmpleados
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
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AplicacionDbContext1>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection1")));


            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //JWT Authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Key);

            services.AddAuthentication(au => {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {

                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

                   
            

            //Actualizacion de Datos
            services.AddScoped<IApiAgregarEstudioUsuarioService, ApiAgregarEstudioUsuarioService>();
            services.AddScoped<IApiAgregarFamiliarUsuarioService, ApiAgregarFamiliarUsuarioService>();
            services.AddScoped<IApiAgregarUbicacionUsuarioService, ApiAgregarUbicacionUsuarioService>();
            services.AddScoped<IApiCambioRegistroService, ApiCambioRegistroService>();
            services.AddScoped<IApiConocimientoListarService, ApiConocimientoListarService>();
            services.AddScoped<IApiCursoListarService, ApiCursoListarService>();
            services.AddScoped<IApiFamiliarListarService, ApiFamiliarListarService>();
            services.AddScoped<IApiEmpleadoDatosListarService, ApiEmpleadoDatosListarService>();
            services.AddScoped<IApiEmpleadoUbicacionListarService, ApiEmpleadoUbicacionListarService>();
            services.AddScoped<IApiEmpleadoListarService, ApiEmpleadoListarService>();
            services.AddScoped<IApiEstudioListarService, ApiEstudioListarService>();
            services.AddScoped<IApiExperienciaListarService, ApiExperienciaListarService>();
            services.AddScoped<IApiReferenciaListarService, ApiReferenciaListarService>();
            services.AddScoped<IApiUtilitarioListarEmpleadoService, ApiUtilitarioListarEmpleadoService>();
            services.AddScoped<IApiParienteIntiListarService, ApiParienteIntiListarService>();
            services.AddScoped<IApiConocimientoOtroListarService, ApiConocimientoOtroListarService>();
            /************para la obtimizacion de las APIS************/
            services.Configure<GzipCompressionProviderOptions>(options =>
            options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            /********fin obtimizacion de las APIS***********/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseResponseCompression();
            /*************/

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
