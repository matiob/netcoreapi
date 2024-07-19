using FluentValidation.AspNetCore;
using Library.Configs;
using Library.Context;
using Library.Repositories;
using Library.Repositories.Impl;
using Library.Services;
using Library.Services.Auth;
using Library.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen((options) => // JWT para swagger
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddHttpContextAccessor(); // Httpaccesor (para api client)

builder.Services.AddTransient<ILibroRepository, LibroRepositoryImpl>(); // Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryImpl>();
builder.Services.AddScoped<IPaisRepository, PaisRepositoryImpl>();
builder.Services.AddScoped<IAutorRepository, AutorRepositoryImpl>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepositoryImpl>();

builder.Services.AddScoped<ILibroService, LibroServiceImpl>(); // Service
builder.Services.AddScoped<IUsuarioService, UsuarioServiceImpl>();
builder.Services.AddScoped<IPaisService,  PaisServiceImpl>();
builder.Services.AddScoped<IAutorService, AutorServiceImpl>();
builder.Services.AddScoped<IGeneroService, GeneroServiceImpl>();

builder.Services.AddScoped<JwtHelper>(); // JWT helper service (no le hice interfaz)
builder.Services.AddScoped<ILoginService, LoginServiceImpl>(); // Login Service

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Mapper
// builder.Services.AddAutoMapper(typeof(MappingConfigurations)); // Mapper (otra forma)

builder.Services.AddHttpClient("CountryApi", options => // API cliente
{
    options.BaseAddress = new Uri("https://restcountries.com/v3.1/");
});

builder.Services.AddDbContext<LibraryContext>((context) => // DB
    {
        context.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDB"));
    });

builder.Services.AddFluentValidation((options) => // Validations
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
);
// builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // Validations => no funciona
//builder.Services.Configure<ApiBehaviorOptions>(options => // Quitar validacion automatica
//{
//    options.SuppressModelStateInvalidFilter = true;
//}); // permite ejecutar los validadores en los services de manera manual

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => // JWT
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});

// CORS (otra forma)
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//               builder =>
//               {
//                   builder.WithOrigins("http://localhost:3000")
//                       .AllowAnyHeader()
//                       .AllowAnyOrigin()
//                       .AllowAnyMethod();

//               });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication(); // en ese orden
app.UseAuthorization();

app.MapControllers();

app.Run();
