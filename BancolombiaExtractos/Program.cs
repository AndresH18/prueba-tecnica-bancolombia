using BancolombiaExtractos.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PruebaBancolombiaContext>();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Extractos API",
        Description = "Prueba técnica Bancolombia",
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => { o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

// redirect HTTP requests to HTTPS
app.UseHttpsRedirection();
// add static files like js, css
app.UseStaticFiles();
// enable routing
app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();