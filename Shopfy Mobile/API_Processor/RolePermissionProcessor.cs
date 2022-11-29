using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shopfy_Mobile.API_Common_Methods;
using Shopfy_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shopfy_Mobile.API_Processor
{
    public class RolePermissionProcessor
    {
        public static async Task<List<RolePermissionModel>> LoadRolePermissionsByRoleId(int role_id)
        {
            string url = $"api/RolePermission/RolePermissionSpecificByRoleId?roleId={role_id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<RolePermissionModel> RolePermissions = await response.Content.ReadAsAsync<List<RolePermissionModel>>();

                    return RolePermissions;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}