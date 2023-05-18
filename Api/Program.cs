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

var authenticationOptions = new KeycloakAuthenticationOptions
{
    AuthServerUrl = "http://localhost:8088/",
    Realm = "Test",
    Resource = "test-client",
};

builder.Services.AddKeycloakAuthentication(authenticationOptions);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("users", policy =>
               policy.RequireAssertion(context =>
               context.User.HasClaim(c =>
                       (c.Value == "user") || (c.Value == "admin"))));
    //Create policy with only one claim
    options.AddPolicy("admins", policy =>
        policy.RequireClaim(ClaimTypes.Role, "admin"));
    //Create a policy with a claim that doesn't exist or you are unauthorized to
    options.AddPolicy("noaccess", policy =>
        policy.RequireClaim(ClaimTypes.Role, "noaccess"));

});

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
