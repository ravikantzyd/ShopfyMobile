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
    public class CityProcessor
    {
        public static async Task<List<CityModel>> LoadCities()
        {
            string url = "api/City";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CityModel> City = await response.Content.ReadAsAsync<List<CityModel>>();

                    return City;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CityModel>> LoadCitiesByDecending()
        {
            string url = "api/City/CitiesByDecending";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CityModel> City = await response.Content.ReadAsAsync<List<CityModel>>();

                    return City;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<CityModel> LoadCity(int Id)
        {
            string url = $"api/City/CityById/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    CityModel City = await response.Content.ReadAsAsync<CityModel>();

                    return City;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static async Task<CityModel> LoadCityByName(string customer_name)
        {
            ViewByCityNameModel customer = new ViewByCityNameModel
            {
                CityName = customer_name
            };

            string url = $"api/City/CityByName";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    CityModel City = await response.Content.ReadAsAsync<CityModel>();

                    return City;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<CityModel> SetCity(CreateCityModel city)
        {
            string url = "api/City";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, city))
            {
                if (response.IsSuccessStatusCode)
                {
                    CityModel City = await response.Content.ReadAsAsync<CityModel>();

                    return City;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task ModifyCity(int id, UpdateCityModel city)
        {
            string url = $"api/City/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, city))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteCity(int id)
        {
            string url = $"api/City/{id}";

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