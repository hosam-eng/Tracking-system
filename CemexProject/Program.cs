using CemexProject.Hubs;
using CemexProject.MiddlewareExtentions;
using CemexProject.SqlTableDependancy;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddSingleton<TruckHub>();

builder.Services.AddSingleton<TruckHub>();
builder.Services.AddSingleton<SqlTruckDependancy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

var webRootProvider = new PhysicalFileProvider(builder.Environment.WebRootPath);
var newPathProvider = new PhysicalFileProvider(
  Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles"));

var compositeProvider = new CompositeFileProvider(webRootProvider,
                                                  newPathProvider);

// Update the default provider.
app.Environment.WebRootFileProvider = compositeProvider;


app.UseStaticFiles(); // Serve files from wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles"))
});


app.UseRouting();

app.UseAuthorization();

app.MapHub<TruckHub>("/truckHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Truck}/{action=Index}/{id?}");


app.UseSqlTableDependency();

app.Run();
