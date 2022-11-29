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
    public class UserProcessor
    {
        public static async Task<UserModel> LoadUserByName(string user_name)
        {
            ViewUserByNameModel user = new ViewUserByNameModel
            {
                UserNameId = user_name
            };

            string url = $"api/User/UserByName";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, user))
            {
                if (response.IsSuccessStatusCode)
                {
                    UserModel Users = await response.Content.ReadAsAsync<UserModel>();

                    return Users;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}