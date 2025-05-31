using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Components;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.Infrastructure.Data;
using CedrosNahuizalquenos.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

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
builder.Services.AddScoped<IReporteProducto, ReporteProducto>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();


//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger(); // Esto habilita el middleware que sirve /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cedros API V1");
        // c.RoutePrefix = ""; // Opcional: para que swagger UI sea la ra�z "/"
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles("/client");  // <-- necesario
app.UseStaticFiles();           // <-- necesario
app.UseAntiforgery();

app.MapStaticAssets();
// Aqu� agregamos el mapeo de los controllers para que respondan a las peticiones API
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CedrosNahuizalquenos.Client._Imports).Assembly);

// <-- necesario para soportar F5 y navegaci�n directa
app.MapFallbackToFile("/client/{*path:nonfile}", "client/index.html");
app.MapGet("/", context =>
{
    context.Response.Redirect("/client/login");
    return Task.CompletedTask;
});

app.Run();
