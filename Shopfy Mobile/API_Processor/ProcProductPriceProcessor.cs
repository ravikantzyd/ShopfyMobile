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
    public class ProcProductPriceProcessor
    {
        public static async Task<List<PRODUCT_PRICE_PROC_OUT>> LoadLatestProductPriceList(PRODUCT_PRICE_PROC_IN product_price)
        {            
            string url = $"api/ProcProductPrice";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, product_price))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<PRODUCT_PRICE_PROC_OUT> product_prices = await response.Content.ReadAsAsync<List<PRODUCT_PRICE_PROC_OUT>>();

                    if (AppType.type == "User")
                    {
                        return product_prices.Where(level => level.LevelName != "Default").ToList();
                    }
                    else
                    {
                        return product_prices;
                    }                    
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<COUNT_PROC_OUT>> LoadLatestProductPriceListCount(PRODUCT_PRICE_PROC_IN product_price)
        {
            string url = $"api/ProcProductPrice/LatestProductPriceListCount";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, product_price))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<COUNT_PROC_OUT> product_prices = await response.Content.ReadAsAsync<List<COUNT_PROC_OUT>>();

                    return product_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<PRODUCT_PRICE_PROC_OUT>> LoadLatestProductPriceListSearch(PRODUCT_PRICE_SEARCH_PROC_IN product_price)
        {
            string url = $"api/ProcProductPrice/LatestProductPriceListSearchAll";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, product_price))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<PRODUCT_PRICE_PROC_OUT> product_prices = await response.Content.ReadAsAsync<List<PRODUCT_PRICE_PROC_OUT>>();

                    if (AppType.type == "User")
                    {
                        return product_prices.Where(level => level.LevelName != "Default").ToList();
                    }
                    else
                    {
                        return product_prices;
                    }
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<COUNT_PROC_OUT>> LoadLatestProductPriceListSearchCount(PRODUCT_PRICE_SEARCH_PROC_IN product_price)
        {
            string url = $"api/ProcProductPrice/LatestProductPriceListSearchCount";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, product_price))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<COUNT_PROC_OUT> product_prices = await response.Content.ReadAsAsync<List<COUNT_PROC_OUT>>();

                    return product_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}