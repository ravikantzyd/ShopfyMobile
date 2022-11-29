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
    public class BranchProcessor
    {
        public static async Task<List<BranchModel>> LoadBranches()
        {
            string url = "api/Branch";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<BranchModel> Branches = await response.Content.ReadAsAsync<List<BranchModel>>();

                    return Branches;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<BranchModel>> LoadBranchesByDecending()
        {
            string url = "api/Branch/BranchesByDecending";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<BranchModel> Branches = await response.Content.ReadAsAsync<List<BranchModel>>();

                    return Branches;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<BranchModel> LoadBranch(int Id)
        {
            string url = $"api/Branch/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    BranchModel Branches = await response.Content.ReadAsAsync<BranchModel>();

                    return Branches;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<BranchModel> LoadBranchByName(string customer_name)
        {
            ViewBranchByNameModel customer = new ViewBranchByNameModel
            {
                BranchName = customer_name
            };

            string url = $"api/Branch/BranchByName";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    BranchModel Branches = await response.Content.ReadAsAsync<BranchModel>();

                    return Branches;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<BranchModel> SetBranch(CreateBranchModel customer)
        {
            string url = "api/Branch";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    BranchModel Branches = await response.Content.ReadAsAsync<BranchModel>();

                    return Branches;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task ModifyBranch(int id, UpdateBranchModel customer)
        {
            string url = $"api/Branch/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, customer))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteBranch(int id)
        {
            string url = $"api/Branch/{id}";

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