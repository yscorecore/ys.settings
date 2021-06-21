using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using YS.AppContext;
using YS.Knife.Data;

namespace YS.Settings
{
    public interface ISettingService:IUserSettingService,IAppSettingService
    {

        Task<UserOrAppSetting> GetUserOrAppSetting(string settingKey);

    }

    public interface IUserSettingService
    {
        Task<SettingItem> GetUserSetting(string settingKey);

        Task SaveUserSetting(SettingItem settingItem);

        Task<bool> DeleteUserSetting(string settingKey);

        Task<LimitData<SettingItem>> ListUserSettings(LimitInfo limitInfo);
    }

    public interface IAppSettingService
    {
        Task<SettingItem> GetAppSetting(string settingKey);

        Task SaveAppSetting(SettingItem settingItem);
        
        Task<bool> DeleteAppSetting(string settingKey);

        Task<LimitData<SettingItem>> ListAppSettings(LimitInfo limitInfo);
    }

    public class SettingItem
    {
        public string ContentKey { get; set; }
        public string ContentType { get; set; } = "text";
        public string Context { get; set; }
    }

    public class UserOrAppSetting
    {
        public SettingItem UserSetting { get; set; }
        public SettingItem AppSetting { get; set; }
    }

    public static class SettingServiceExtensions
    {
        private const string JSON_CONTENT_TYPE = "JSON";
        private const string TEXT_CONTENT_TYPE = "TEXT";

        private static T DeSerialize<T>(SettingItem settingItem)
        {
            return default;
        }
        

        private static T DeSerialize<T>(string contentType,string content)
        {
            switch (contentType.ToUpper())
            {
                case TEXT_CONTENT_TYPE:
                    return default;
                case JSON_CONTENT_TYPE:
                    return Json.DeSerialize<T>(content);
                default:
                    throw new NotSupportedException($"content type '{contentType}' not supported.");
            }
            
        }
        private static string Serialize<T>(T content)
        {
            return null;

        }

        private static T Merge<T>(T source, T target)
        {
            return default;
        }

        public static async Task<T> GetUserSetting<T>(this IUserSettingService userSettingService, string settingKey)
        {
            var item = await userSettingService.GetUserSetting(settingKey);
            
            return item==null?default:DeSerialize<T>(item.ContentType,item.Context);
        }
        
        public  static async Task SaveUserSetting<T>(this IUserSettingService userSettingService, string settingKey, T settingValue)
        {
            if (settingValue is string str)
            {
                await userSettingService.SaveUserSetting(new SettingItem{ ContentKey = settingKey,ContentType = TEXT_CONTENT_TYPE,  Context = str } );
            }
            else
            {
                await userSettingService.SaveUserSetting(new SettingItem{ ContentKey = settingKey,ContentType = JSON_CONTENT_TYPE,  Context = Serialize(settingValue) } );
            }
        }
        public static async Task<T> GetAppSetting<T>(this IAppSettingService appSettingService, string settingKey)
        {
            var item = await appSettingService.GetAppSetting(settingKey);
            
            return item==null?default:DeSerialize<T>(item.ContentType,item.Context);
        }
        
        public  static async Task SaveAppSetting<T>(this IAppSettingService appSettingService, string settingKey, T settingValue)
        {
            if (settingValue is string str)
            {
                await appSettingService.SaveAppSetting(new SettingItem{ ContentKey = settingKey,ContentType = TEXT_CONTENT_TYPE,  Context = str } );
            }
            else
            {
                await appSettingService.SaveAppSetting(new SettingItem{ ContentKey = settingKey,ContentType = JSON_CONTENT_TYPE,  Context = Serialize(settingValue) } );
            }

        }

        public static async Task<T> GetUserOrAppSetting<T>(this ISettingService settingService, string settingKey)
        {
            var userOrAppSetting = await settingService.GetUserOrAppSetting(settingKey);
            var appValue = DeSerialize<T>(userOrAppSetting.AppSetting);
            var userValue = DeSerialize<T>(userOrAppSetting.UserSetting);
            return Merge(appValue, userValue);
        }

    }

}
