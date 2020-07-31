using System.Text;
using JWTAuthorizationASPNETCoreDemo.Models;
using JWTAuthorizationASPNETCoreDemo.Services;
using JWTAuthorizationASPNETCoreDemo.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthorizationASPNETCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            InitializeDb();
            services.AddDbContext<AppUserDbContext>(options =>
            options.UseMySQL("server=localhost;port=3306;user=root;password=Mysqlparola123;database=AppUserDb"));
            services.AddTransient<IAppUserRepo, AppUserRepo>();

            services.AddCors();

            var appSettingsSection = Configuration;
            services.Configure<AppSettings>(Configuration);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(Configuration["SecretKey"]);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IAccountService, AccountService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void InitializeDb()
        {
            using (var context = new AppUserDbContext())
            {
                if (context.Database.EnsureCreated())
                {
                    string salt = Utilities.CreateSalt();

                    var admin = new AppUser
                    {
                        Username = "onurkayabasi",
                        HashedPassword = Utilities.CreateHash("testparola123", salt)
                    };

                    context.AppUsers.Add(admin);

                    context.SaveChanges();
                }
            }
        }
    }
}
