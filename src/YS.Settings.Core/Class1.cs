using System;
using System.Threading.Tasks;
using YS.AppContext;
using YS.Knife.Data;

namespace YS.Settings
{
    public interface ISettingService
    {
        Task<SettingItem> GetSetting(string contextKey, string settingKey);

        Task SaveSetting(string contextKey, string settingKey, string settingValue);
    }

    public class SettingItem
    {
        public string ContentKey { get; set; }
        public string ContentType { get; set; } = "text";
        public string Context { get; set; }
    }

    public static class SettingServiceExtensions
    {
        private const string JSON_CONTENT_TYPE = "JSON";
        private const string TEXT_CONTENT_TYPE = "TEXT";
        
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

        public static async Task<T> GetSetting<T>(this ISettingService settingService, string contextKey, string settingKey)
        {
            var item = await settingService.GetSetting(contextKey, settingKey);
            
            return item==null?default:DeSerialize<T>(item.ContentType,item.Context);
        }

        public  static async Task SaveSetting<T>(this ISettingService settingService, string contextKey, string settingKey, T settingValue)
        {
            string val = settingValue.ToJsonString();
            await settingService.SaveSetting(contextKey, settingKey, val);
        }

        public static Task<T> GetUserSetting<T>(this ISettingService settingService, string settingKey) =>
            settingService.GetSetting<T>(AppContextKeys.UserId, settingKey);

        public static Task SaveUserSetting<T>(this ISettingService settingService, string settingKey, T settingValue) =>
            settingService.SaveSetting(AppContextKeys.UserId, settingKey, settingValue);

    }

}
