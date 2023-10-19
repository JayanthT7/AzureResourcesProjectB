using KeyValutJayanthApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer());

//builder.Configuration.GetConnectionString("SQLConnection");

// Add services to the container.
//builder.Services.AddScoped<IProductRepository, ProductRepository>();


//Key valut part


builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var settings = config.Build();
    var keyvaultUrl = settings["KeyVaultConfiguration:KeyVaultUrl"];
    var clientId = settings["KeyVaultConfiguration:ClientId"];
    var clientSecret = settings["KeyVaultConfiguration:ClientSecret"];
    config.AddAzureKeyVault(keyvaultUrl, clientId, clientSecret, new DefaultKeyVaultSecretManager());

});
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration["DBconnstring"]));
//if (Environment.GetEnvironmentVariable("SQL_DB") != null)
//{
//    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_DB")));
//}
//else
//{
//    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection")));
//}

// Add services to the container.

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

app.MapControllers();

app.Run();
