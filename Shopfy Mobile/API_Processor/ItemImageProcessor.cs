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
    public class ItemImageProcessor
    {
        public static async Task<ItemImageModel> LoadItemImageByItemId(int item_id)
        {
            string url = $"api/ItemImage/ItemImageByItemId/{item_id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemImageModel ItemImage = await response.Content.ReadAsAsync<ItemImageModel>();

                    return ItemImage;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

    }
}