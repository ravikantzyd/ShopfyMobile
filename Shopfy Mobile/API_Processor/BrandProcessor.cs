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
    public class BrandProcessor
    {
        public static async Task<List<BrandModel>> LoadBrands()
        {
            string url = "api/Brand";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<BrandModel> Brand = await response.Content.ReadAsAsync<List<BrandModel>>();

                    return Brand;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<BrandModel>> LoadBrandsByDecending()
        {
            string url = "api/Brand/BrandsByDecending";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<BrandModel> Brand = await response.Content.ReadAsAsync<List<BrandModel>>();

                    return Brand;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<BrandModel> LoadBrand(int Id)
        {
            string url = $"api/Brand/BrandById/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    BrandModel Brand = await response.Content.ReadAsAsync<BrandModel>();

                    return Brand;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static async Task<BrandModel> LoadBrandByName(string customer_name)
        {
            ViewByBrandNameModel customer = new ViewByBrandNameModel
            {
                BrandName = customer_name
            };

            string url = $"api/Brand/BrandByName";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    BrandModel Brand = await response.Content.ReadAsAsync<BrandModel>();

                    return Brand;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<BrandModel> SetBrand(CreateBrandModel city)
        {
            string url = "api/Brand";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, city))
            {
                if (response.IsSuccessStatusCode)
                {
                    BrandModel Brand = await response.Content.ReadAsAsync<BrandModel>();

                    return Brand;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task ModifyBrand(int id, UpdateBrandModel city)
        {
            string url = $"api/Brand/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, city))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteBrand(int id)
        {
            string url = $"api/Brand/{id}";

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