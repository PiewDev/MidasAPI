using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Midas.Net.Autofac;
using Midas.Net.Crud;
using Midas.Net.Database;
using Midas.Net.Mapper;
using Midas.Net.ResponseHandling;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        builder.RegisterModule(new AutofacModuleRegister());

        IMapper mapper = AutoMapperConfiguration.Configure();
        builder.RegisterInstance(mapper).As<IMapper>();

        builder.RegisterType<CommerceDbContext>()
            .WithParameter("connectionString", configuration["connectionStrings:Commerce"])
                .InstancePerLifetimeScope();
    });

XmlDocument log4netConfig = new XmlDocument();
log4netConfig.Load(File.OpenRead("log4net.config"));
XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), log4netConfig["log4net"]);


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));


}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = issuer,
    ValidAudience = audience,
    IssuerSigningKey = key
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddScoped<CrudSupportFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
