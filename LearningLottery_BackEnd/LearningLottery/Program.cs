using AutoMapper;
using LearningLottery.Core.InterfaceControl;
using LearningLottery.Core.Repositories.Models;
using LearningLottery.Core.Services;
using LearningLottery.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var appSettings = "appsettings.json";
//import json => in service.cs can di injection IConfiguration interface to get json data
builder.Configuration.AddJsonFile(appSettings, optional: true, reloadOnChange: true);

// // 設定跨域
// builder.Services.AddCors(option => option.AddPolicy("Policy",builder =>{
//     builder.AllowAnyOrigin()
//         .AllowAnyHeader()
//         .AllowAnyMethod();
// }));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<SqlDbContext>(options => {
       options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection"));
   });

builder.Services.AddScoped<IModelTransformerService, ModelTransformerService>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
builder.Services.AddSingleton<IMapper>(x => {
    var config = new MapperConfiguration(cfg => {
        cfg.CreateMap<LearningLottery.Core.Services.Models.UserAccess, LearningLottery.Models.UserAccess>();
    });
    var mapper = config.CreateMapper();
    return mapper;
});

var register = new Register();
register.DI((interfaceType, imType, lifeType) =>
{
    switch (lifeType)
    {
        case LifeType.Scoped:
            builder.Services.AddScoped(interfaceType, imType);
            break;
        case LifeType.Transient:
            builder.Services.AddTransient(interfaceType, imType);
            break;
        case LifeType.Singleton:
            builder.Services.AddSingleton(interfaceType, imType);
            break;
        default:
            builder.Services.AddScoped(interfaceType, imType);
            break;
    }
}, "LearningLottery.Core");


Serilog.Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Filter.ByExcluding(logEvent =>
        logEvent.Properties.ContainsKey("SourceContext")
        &&
        logEvent.Properties["SourceContext"].ToString().StartsWith("Executed DbCommand") ||  // logEvent prevent sql in logs
        logEvent.MessageTemplate.Text.Contains("Executed DbCommand"))
    .WriteTo.Console()
    .WriteTo.File("./Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// If needed, Clear default providers
builder.Logging.ClearProviders();
builder.Host.UseSerilog();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
// 使用跨域設定名稱
// app.UseCors("Policy");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();