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
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region "Inyección de Dependencia"
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
builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(cookie =>
{

    cookie.Cookie.Name = "keycloak.cookie";
    cookie.Cookie.MaxAge = TimeSpan.FromMinutes(60);
    cookie.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    cookie.SlidingExpiration = true;
})
.AddOpenIdConnect(options =>
{

    //Use default signin scheme
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //Keycloak server
    options.Authority = builder.Configuration.GetSection("Keycloak")["ServerRealm"];
    //Keycloak client ID
    options.ClientId = builder.Configuration.GetSection("Keycloak")["ClientId"];
    //Keycloak client secret
    options.ClientSecret = builder.Configuration.GetSection("Keycloak")["ClientSecret"];
    //Keycloak .wellknown config origin to fetch config
    options.MetadataAddress = builder.Configuration.GetSection("Keycloak")["Metadata"];
    //Require keycloak to use SSL
    options.RequireHttpsMetadata = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    //Save the token
    options.SaveTokens = true;
    //Token response type, will sometimes need to be changed to IdToken, depending on config.
    options.ResponseType = OpenIdConnectResponseType.Code;
    //SameSite is needed for Chrome/Firefox, as they will give http error 500 back, if not set to unspecified.
    options.NonceCookie.SameSite = SameSiteMode.Unspecified;
    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        RoleClaimType = ClaimTypes.Role,
        ValidateIssuer = true
    };

});
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
