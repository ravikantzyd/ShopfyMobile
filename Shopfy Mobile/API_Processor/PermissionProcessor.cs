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
    public class PermissionProcessor
    {
        public static async Task<PermissionModel> LoadPermission(int Id)
        {
            string url = $"api/Permission/PermissionById/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PermissionModel Permission = await response.Content.ReadAsAsync<PermissionModel>();

                    return Permission;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}