using Api.Infrastructure.Data;
using Api.Infrastructure.Data.Interceptors;
using Api.Services.Authentication;
using Api.Services.Auyhentication;
using Api.Services.Mail;
using Api.Services.User;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Api;

public static class DipendencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection Services, IConfiguration Configuration)
    {

        Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => Configuration.Bind(nameof(JwtBearerOptions), options));

        Services.AddHttpContextAccessor();
        Services.AddLogging();
        Services.AddAuthorization();

        Services.AddDbContext<ApplicationDbContext>(o =>
        o.UseSqlServer(Configuration.GetConnectionString("MyConnection")));

        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddCookie();

        //Services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        //{
        //    opt.UseInMemoryDatabase("type-tests");
        //    opt.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
        //});
        Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        Services.AddFastEndpoints().SwaggerDocument();


        Services.AddTransient<IUser, User>();
        Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        Services.AddScoped<IMailService, MailService>();
        Services.AddScoped<IAuthConfiguration, AuthConfiguration>();

        return Services;
    }
}
