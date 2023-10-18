using System;
using System.Text;
using Contracts;
using Entities;
using Entities.DTO.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase("InMemory"));


builder.Services.AddAuthentication(check =>
{
    check.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    check.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    check.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(check =>
{
    check.SaveToken = true;
    check.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:secretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddControllers();
builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
builder.Services.AddAutoMapper(typeof(Program)); 

builder.Services.Configure<AppOption>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddCors(option =>
{
    option.AddPolicy("Policy", build =>
        build.AllowAnyOrigin().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT_WebAPI", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter this like => Bearer jfbjfjfbhkosdfhgbojnfdfbfhffdajh478",
        Name = "Authorzation",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                },
                Array.Empty<string>()
            },
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
