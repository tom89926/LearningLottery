using Microsoft.Extensions.Configuration;

namespace LearningLottery.Core.Services.Models.MetaData; 

public static class AppSettingConfigurationManager {
    public static IConfiguration AppSetting { get; }
    
    static AppSettingConfigurationManager(){
        var appSettings = "appsettings.json";
        AppSetting = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(appSettings)
            .Build();
    }
}