using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using DeaconAdminHybrid.Components.Models;

namespace DeaconAdminHybrid.Components.Data
{
    public class UserDataAdaptor : DataAdaptor
    {
        private UserDataAccessLayer _dataLayer;
        public UserDataAdaptor(UserDataAccessLayer userDataAccessLayer)
        {
            _dataLayer = userDataAccessLayer;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            List<UserModel> users = await _dataLayer.GetUsersAsync();
            int count = await _dataLayer.GetUserCountAsync();
            return dataManagerRequest.RequiresCounts ? new DataResult() { Result = users, Count = count } : count;
        }

        public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
        {
            await _dataLayer.AddUserAsync(data as UserModel);
            return data;
        }

        public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
        {
            await _dataLayer.UpdateUserAsync(data as UserModel);
            return data;
        }

        public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
        {
            await _dataLayer.RemoveUserAsync(Guid.Parse((string)primaryKeyValue));
            return primaryKeyValue;
        }

        public async Task<object?> GetDeaconTitlesAsync()
        {
            var data = await _dataLayer.GetDeaconTitlesAsync();
            return data;
        }
    }
}
