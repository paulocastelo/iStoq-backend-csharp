using Microsoft.OpenApi.Models;
using iStoq.Application.Interfaces;
using iStoq.Infrastructure.Data;
using iStoq.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using iStoq.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// JWT Config
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
    };
});

builder.Services.AddAuthorization();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "iStoq.API", Version = "v1" });

    // Configuração de segurança para JWT
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Insira seu token JWT no campo abaixo. Exemplo: Bearer {seu token}",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddAuthorization();

// Serviços da aplicação
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();

// Serviço de autenticação
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserService, UserService>();
// Banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Pipeline HTTP
app.UseHttpsRedirection();

// Middleware global de tratamento de exceções
//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Ativa autenticação/autorizações
app.UseAuthentication();
app.UseAuthorization();

// Swagger liberado em qualquer ambiente
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "iStoq API V1");
    c.RoutePrefix = "docs";
});

// Protege acesso ao /docs (Swagger)
app.UseWhen(context => context.Request.Path.StartsWithSegments("/docs"), branch =>
{
    branch.Use(async (ctx, next) =>
    {
        if (!ctx.User.Identity?.IsAuthenticated ?? false)
        {
            ctx.Response.StatusCode = 401;
            await ctx.Response.WriteAsync("Acesso não autorizado ao Swagger.");
            return;
        }

        await next();
    });
});

app.MapControllers();

// Inicializa o banco com dados de seed
DbInitializer.Initialize(app.Services, app.Environment);

app.Run();