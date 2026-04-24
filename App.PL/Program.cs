using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DotVVM.Framework.Routing;
using App.BL.Services;
using App.DAL;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

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
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        builder.Services.AddEntityFrameworkMySQL()
            .AddDbContext<StudentDbContext>(options =>
            {
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

        var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(app.Environment.ContentRootPath);
        dotvvmConfiguration.AssertConfigurationIsValid();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath)
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDotvvmHotReload();
        });

        app.Run();
    }
}