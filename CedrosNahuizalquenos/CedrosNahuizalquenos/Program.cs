using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Client.Pages;
using CedrosNahuizalquenos.Components;
using CedrosNahuizalquenos.Infrastructure.Data;
using CedrosNahuizalquenos.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add SQLConecction
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//Mapeando Clases
builder.Services.AddControllers();
builder.Services.AddScoped<IProductoRepository, ProductRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IReporteCliente, ReporteCliente>();
//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger(); // Esto habilita el middleware que sirve /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cedros API V1");
        // c.RoutePrefix = ""; // Opcional: para que swagger UI sea la raíz "/"
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
// Aquí agregamos el mapeo de los controllers para que respondan a las peticiones API
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CedrosNahuizalquenos.Client._Imports).Assembly);

app.Run();
