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
    public class UnitProcessor
    {
        public static async Task<List<UnitModel>> LoadUnits()
        {
            string url = "api/Units";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<UnitModel> Units = await response.Content.ReadAsAsync<List<UnitModel>>();

                    return Units;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<UnitModel> LoadUnit(int Id)
        {
            string url = $"api/Units/UnitById/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    UnitModel Units = await response.Content.ReadAsAsync<UnitModel>();

                    return Units;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<UnitModel> LoadUnit(string name)
        {
            ViewByUnitNameModel unit_name = new ViewByUnitNameModel
            {
                UnitName = name
            };

            string url = $"api/Units/UnitByName";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, unit_name))
            {
                if (response.IsSuccessStatusCode)
                {
                    UnitModel Units = await response.Content.ReadAsAsync<UnitModel>();

                    return Units;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<UnitModel> SetUnit(CreateUnitModel unit)
        {
            string url = "api/Units";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, unit))
            {
                if (response.IsSuccessStatusCode)
                {
                    UnitModel Units = await response.Content.ReadAsAsync<UnitModel>();

                    return Units;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task ModifyUnit(int id, UpdateUnitModel unit)
        {
            string url = $"api/Units/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, unit))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteUnit(int id)
        {
            string url = $"api/Units/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}