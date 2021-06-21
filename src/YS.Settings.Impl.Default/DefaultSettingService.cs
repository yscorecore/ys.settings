using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using YS.AppContext;
using YS.Knife;
using YS.Knife.Data;

namespace YS.Settings.Impl.Default
{
    [Service(typeof(IAppSettingService))]
    [Service(typeof(IUserSettingService))]
    [Service(typeof(ISettingService))]
    public class DefaultSettingService: ISettingService
    {
        private readonly IAppContext _appContext;

        public DefaultSettingService(IAppContext appContext)
        {
            this._appContext = appContext;
        }

        public Task<SettingItem> GetUserSetting(string settingKey)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserSetting(SettingItem settingItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserSetting(string settingKey)
        {
            throw new NotImplementedException();
        }

        public Task<LimitData<SettingItem>> ListUserSettings(LimitInfo limitInfo)
        {
            throw new NotImplementedException();
        }

        public Task<SettingItem> GetAppSetting(string settingKey)
        {
            throw new NotImplementedException();
        }

        public Task SaveAppSetting(SettingItem settingItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAppSetting(string settingKey)
        {
            throw new NotImplementedException();
        }

        public Task<LimitData<SettingItem>> ListAppSettings(LimitInfo limitInfo)
        {
            throw new NotImplementedException();
        }
    }
}
