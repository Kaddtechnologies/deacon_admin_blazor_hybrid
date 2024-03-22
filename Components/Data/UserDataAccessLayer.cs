using Dapper;
using DeaconAdminHybrid.Components.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace DeaconAdminHybrid.Components.Data
{
    public class UserDataAccessLayer
    {

        public IConfiguration _configuration;
        private string _connectionString { get; set; }
        private const string SELECT_All_DEACONS = "select * from Users";
        private const string SELECT_All_DEACON_TITLES = "select * from DeaconTitle";

        public UserDataAccessLayer(IConfiguration configuration)
        {
            //Inject configuration to access Connection string from appsettings.json.
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == EnvironmentName.Development)
            {
                _connectionString = "Data Source=67.211.213.157,1433;Database=Deacons;TrustServerCertificate=True;MultiSubnetFailover=False;User ID=sa;Password=Logvc123!";//configuration.GetConnectionString("DevStaffConnString");
            }
            else
            {
                _connectionString = "Data Source=67.211.213.157,1433;Database=Deacons;TrustServerCertificate=True;MultiSubnetFailover=False;User ID=sa;Password=Logvc123!";//configuration.GetConnectionString("StaffConnString");
            }
        }

         public async Task<List<UserModel>> GetUsersAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                IEnumerable<UserModel> result = await connection.QueryAsync<UserModel>(SELECT_All_DEACONS);
                return result.ToList();
            }
        }

        public async Task<int> GetUserCountAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                int result = await db.ExecuteScalarAsync<int>("Select count(*) from Users");
                return result;
            }
        }

    public async Task AddUserAsync(UserModel user)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            db.Open();
            await db.InsertAsync<UserModel>(user);
        }

    }

        public async Task UpdateUserAsync(UserModel user)
        {
            bool component = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                component = await connection.UpdateAsync<UserModel>(user);
            }
            Console.WriteLine(component);
            //using (var db = new SqlConnection(_connectionString))
            //{
            //    db.Open();
            //    await db.ExecuteAsync("Update Users set Summary=@Summary, UserPriority=@UserPriority, Assignee=@Assignee, UserStatus=@UserStatus where id=@Id", user);
            //}
        }

        public async Task RemoveUserAsync(Guid userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                await db.ExecuteAsync("Delete from Users Where UserId=@UserId", new { UserId = userId });
            }
        }

        public async Task<List<DeaconTitleModel>> GetDeaconTitlesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                IEnumerable<DeaconTitleModel> result = await connection.QueryAsync<DeaconTitleModel>(SELECT_All_DEACON_TITLES);
                List<DeaconTitleModel> model = new List<DeaconTitleModel>();

                foreach (var user in result)
                {
                    var deacon = new DeaconTitleModel();
                    deacon.TitleId = user.TitleId;
                    deacon.DeaconPosition = user.DeaconPosition == "" ? user.DeaconTitle : user.DeaconPosition;
                    model.Add(deacon);
                }                
                return (List<DeaconTitleModel>)model.OrderBy(x => x.DeaconPosition);
            }
        }

    }
}
