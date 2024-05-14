using APOYOS_SOCIALES.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using APOYOS_SOCIALES.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace APOYOS_SOCIALES
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //// Conexion de la base de datos en el flujo principal
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));


            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<Services.IAuthorizationService, AuthorizationService>();

            var key = Encoding.UTF8.GetBytes(Configuration["JwtSettings:key"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddTransient<IAlmacenadorImagenes, AlmacenadorImagenesLocal>();
            services.AddHttpContextAccessor();



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();

            //using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            //    dbContext.Database.Migrate();
            //}
        }
    }
}