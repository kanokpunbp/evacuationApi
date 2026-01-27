using evacuation.Application;
using evacuation.Application.Interfaces;
using evacuation.Infrastructure;
using evacuation.Infrastructure.Redis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddSingleton<IConnectionMultiplexer>(
//    ConnectionMultiplexer.Connect(
//        builder.Configuration.GetConnectionString("Redis")));

//builder.Services.AddScoped<IEvacuationStatusCache,
//    EvacuationStatusRedisCache>();

//DI Service
builder.Services.AddApplication();

//DI Repos
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
