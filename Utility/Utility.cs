using Dapper;
using Dapper.Contrib.Extensions;
using DeaconAdminHybrid.Components.Data;
using DeaconAdminHybrid.Components.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeaconAdminHybrid.Utility
{
    public static class Utility
    {
        public static List<StateModel> GetStateModel()
        {
            List<StateModel> ro = new List<StateModel>();
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"{name}.Resources.Json.stateList.json")!;
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                string json = streamReader.ReadToEnd();
                ro = JsonConvert.DeserializeObject<List<StateModel>>(json);

                return ro;
            }
        }

        public static async Task<List<DeaconTitleModel>> GetDeaconTitleModelsAsync()
        {
            List<DeaconTitleModel> returnModel = new List<DeaconTitleModel>();
            using (var connection = new SqlConnection("Data Source=67.211.213.157,1433;Database=Deacons;TrustServerCertificate=True;MultiSubnetFailover=False;User ID=sa;Password=Logvc123!"))
            {
                await connection.OpenAsync();
               var component = (List<DeaconTitleModel>)await connection.GetAllAsync<DeaconTitleModel>();
                List<DeaconTitleModel> model = new List<DeaconTitleModel>();

                foreach (var user in component)
                {
                    var deacon = new DeaconTitleModel();
                    deacon.TitleId = user.TitleId;
                    deacon.DeaconPosition = user.DeaconPosition == "" ? user.DeaconTitle : user.DeaconPosition;
                    model.Add(deacon);
                }
                returnModel = model.ToList();
            }
            return returnModel.OrderBy(key => key.DeaconPosition).ToList();
        }
    }
               
    
}
