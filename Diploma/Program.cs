using System.Text.Json.Serialization;
using Diploma.Auth;
using Diploma.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Configure(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

void RegisterServices(IServiceCollection services)
{
    var builder = new MySqlConnectionStringBuilder
    {
        Server = "cfif31.ru",
        Port = 3306,
        UserID = "ISPr24-39_BeluakovDS",
        Password = "ISPr24-39_BeluakovDS",
        Database = "ISPr24-39_BeluakovDS_Kwork_Diplom_1",
        CharacterSet = "utf8"
    };

    services.AddDbContext<EfModel>(o => o.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString)));

    services.AddControllers()
        .AddJsonOptions(
            opt => opt.JsonSerializerOptions
                .Converters.Add(new JsonStringEnumConverter()));
    
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = TokenBaseOptions.AUDIENCE,
                ValidateIssuer = true,
                ValidIssuer = TokenBaseOptions.ISSUER,
                ValidateLifetime = true,
                IssuerSigningKey = TokenBaseOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = false
            };
        });

    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });

        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        }, new string []{}
                    }
                });
    });
}

void Configure(WebApplication app)
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}