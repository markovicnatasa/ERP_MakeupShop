using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MakeupShopContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IBrendRepository, BrendRepository>();
builder.Services.AddScoped<IListaZeljaRepository,ListaZeljaRepository>();
builder.Services.AddScoped<IListaZeljaProizvodRepository, ListaZeljaProizvodRepository>();
builder.Services.AddScoped<IPlacanjeRepository, PlacanjeRepository>();
builder.Services.AddScoped<IPorudzbinaRepository,PorudzbinaRepository>();
builder.Services.AddScoped<IProizvodRepository, ProizvodRepository>();
builder.Services.AddScoped<IRecenzijaRepository, RecenzijaRepository>();
builder.Services.AddScoped<IStavkaPorudzbineRepository, StavkaPorudzbineRepository>();
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Zaposleni", policy => policy.RequireRole("Zaposleni"));
    options.AddPolicy("Kupac", policy => policy.RequireRole("Kupac"));
    options.AddPolicy("Access", policy => policy.RequireAuthenticatedUser().RequireClaim("korisnikID"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
