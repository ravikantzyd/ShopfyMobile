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
    public class ItemProcessor
    {
        public static async Task<List<ItemModel>> LoadItems()
        {
            string url = "api/Items";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemModel> Items = await response.Content.ReadAsAsync<List<ItemModel>>();

                    return Items;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemModel>> LoadOnlyItemsOfSalePrices()
        {
            string url = "api/Items/OnlyItemsOfSalePrices";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemModel> Items = await response.Content.ReadAsAsync<List<ItemModel>>();

                    return Items;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemModel>> LoadItemsDecending()
        {
            string url = "api/Items/ItemsByDecending";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemModel> Items = await response.Content.ReadAsAsync<List<ItemModel>>();

                    return Items;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ItemModel> LoadItem(int Id)
        {
            string url = $"api/Items/ItemById/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemModel Items = await response.Content.ReadAsAsync<ItemModel>();

                    return Items;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ItemModel> LoadItem(string itemName)
        {
            ViewByItemNameDTO item_name = new ViewByItemNameDTO
            {
                ItemName = itemName
            };

            string url = $"api/Items/ItemByName";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, item_name))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemModel Items = await response.Content.ReadAsAsync<ItemModel>();

                    return Items;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ItemModel> LoadItemWithUnitById(int Id)
        {
            string url = $"api/Items/ItemWithUnitById/{Id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemModel Items = await response.Content.ReadAsAsync<ItemModel>();

                    return Items;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ItemModel> SetItem(CreateItemModel unit)
        {
            string url = "api/Items";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, unit))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemModel Items = await response.Content.ReadAsAsync<ItemModel>();

                    return Items;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task ModifyItem(int id, UpdateItemModel item)
        {
            string url = $"api/Items/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, item))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteItem(int id)
        {
            string url = $"api/Items/{id}";

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