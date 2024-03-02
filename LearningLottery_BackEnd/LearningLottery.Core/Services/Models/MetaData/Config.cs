using System.Net;

namespace LearningLottery.Core.Services.Models.MetaData;

//
public static class Config {
    public static readonly string SqlDbConnection = AppSettingConfigurationManager.AppSetting["ConnectionStrings:SqlDbConnection"]!;
}