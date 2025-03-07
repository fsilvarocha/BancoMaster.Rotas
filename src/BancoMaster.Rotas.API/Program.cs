using BancoMaster.Rotas.Application.Rotas.AdcionarRotas;
using BancoMaster.Rotas.Application.Rotas.AdcionarRotas.Interface;
using BancoMaster.Rotas.Application.Rotas.CalcularRotas;
using BancoMaster.Rotas.Application.Rotas.CalcularRotas.Interface;
using BancoMaster.Rotas.Application.Rotas.EditarRotas;
using BancoMaster.Rotas.Application.Rotas.EditarRotas.Interface;
using BancoMaster.Rotas.Application.Rotas.ExcluirRota;
using BancoMaster.Rotas.Application.Rotas.ExcluirRota.Interface;
using BancoMaster.Rotas.Application.Rotas.ListarRotas;
using BancoMaster.Rotas.Application.Rotas.ListarRotas.Interface;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;
using BancoMaster.Rotas.Infra.Data;
using BancoMaster.Rotas.Infra.Repository.Rotas;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<BancoMasterContext>(options => options.UseInMemoryDatabase("RotaDb"));
builder.Services.AddScoped<IRotaRepository, RotaRepository>();

builder.Services.AddScoped<IAdicionarRotaServico, AdicionarRotaServico>();
builder.Services.AddScoped<ICalcularMelhorRotaServico, CalcularMelhorRotaServico>();
builder.Services.AddScoped<IListarRotaServico, ListarRotaServico>();
builder.Services.AddScoped<IExcluirRotaServico, ExcluirRotaServico>();
builder.Services.AddScoped<IEditarRotaServico, EditarRotaServico>();

var app = builder.Build();

app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BancoMasterContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCondo v1");
        c.DocExpansion(DocExpansion.None);
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
