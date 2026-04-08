using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using App.BL.Services;
using MySql.EntityFrameworkCore.Extensions;
using App.DAL;
using Microsoft.EntityFrameworkCore;

namespace App.PL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();

        builder.Services.AddDataProtection();
        builder.Services.AddAuthorization();
        builder.Services.AddWebEncoders();
        builder.Services.AddAuthentication();

        builder.Services.AddTransient<StudentService>();
        builder.Services.AddEntityFrameworkMySQL()
            .AddDbContext<StudentDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");
                options.UseMySQL(connectionString);
            });

        builder.Services.AddDotVVM<DotvvmStartup>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
            app.UseHsts();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDotVVM<DotvvmStartup>(app.Environment.ContentRootPath);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath)
        });

        app.MapDotvvmHotReload();

        app.Run();
    }
}
