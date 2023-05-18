using Datos.Contexto;
using Datos.Repositorio;
using Dominio.Interface.Repositorio;
using Dominio.Modelo;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Microsoft.Extensions.Configuration;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region "Inyecci�n de Dependencia"
var connectionString = builder.Configuration.GetConnectionString("SqlConnections");
builder.Services.AddDbContext<ApplicationsContext>(x =>
{
    x.UseSqlServer(connectionString);
    x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddTransient<ApplicationsContext>();

//inyeccion para los repositorios
builder.Services.AddTransient<IRepositorioBase<ClsEmpleadoDom, int>,ClsEmpleadoRepo>();

//Inyeccion de Servicios
builder.Services.AddTransient<IServicioBase<ClsEmpleadoDom, int>, ClsEmpleadoServ>();
#endregion


#region "Authorizacion"

#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
