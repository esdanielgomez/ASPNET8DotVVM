using Microsoft.Extensions.FileProviders;
using DotVVM.Framework.Hosting;
using App.BL.Services;
using MySql.EntityFrameworkCore.Extensions;
using App.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.PL;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddDataProtection();
builder.Services.AddAuthorization();
builder.Services.AddWebEncoders();
builder.Services.AddAuthentication();

builder.Services.AddTransient(typeof(StudentService));
builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<StudentDbContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);
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

// use DotVVM
app.UseDotVVM<DotvvmStartup>(app.Environment.ContentRootPath);

// use static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath)
});

app.MapDotvvmHotReload();

app.Run();