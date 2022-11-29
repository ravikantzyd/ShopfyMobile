using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shopfy_Mobile.API_Common_Methods;
using Shopfy_Mobile.Models;
using Shopfy_Mobile.Product.SalePrice.BL_Methods.BL_Method_Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shopfy_Mobile.API_Processor
{
    public class ItemDiscountProcessor
    {
        public static async Task<ItemDiscountModel> LoadItemDiscount(int sale_price_id)
        {
            string url = $"api/ItemDiscount/{sale_price_id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemDiscountModel sale_price = await response.Content.ReadAsAsync<ItemDiscountModel>();

                    return sale_price;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadLatestItemDis(SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM input)
        {
            string url = $"api/ItemDiscount/LatestItemDis";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> item_dis = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return item_dis;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadLatestItemDisByPageNum(SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM input)
        {
            string url = $"api/ItemDiscount/LatestItemDisByPageNum";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> item_dis = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return item_dis;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadItemDiscountByDate(DateTime fromDate, DateTime toDate)
        {
            string url = $"api/ItemDiscount/ItemDiscountByDate";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> sale_prices = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadItemDiscountByDateByDecending(DateTime fromDate, DateTime toDate)
        {
            string url = $"api/ItemDiscount/ItemDiscountByDateByDecending";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> sale_prices = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadItemDiscountByDateAndItemDiscountData(CreateItemDiscountModel salePrice)
        {
            string url = $"api/ItemDiscount/ItemDiscountByDateAndItemDiscountSpecificData";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, salePrice))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> sale_prices = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadItemDiscountByDateAndItemId(DateTime fromDate, DateTime toDate, int itemId)
        {
            string url = $"api/ItemDiscount/ItemDiscountByDateAndItemId?itemId={itemId}";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> sale_prices = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<ItemDiscountModel>> LoadItemDiscountByFilter(SHOPFY_ITEM_DIS_FILTER_INPUT_PARAM input)
        {
            string url = $"api/ItemDiscount/ItemDiscountByFilter";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ItemDiscountModel> sale_prices = await response.Content.ReadAsAsync<List<ItemDiscountModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ItemDiscountModel> SetItemDiscount(CreateItemDiscountModel sale_price)
        {
            string url = "api/ItemDiscount";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, sale_price))
            {
                if (response.IsSuccessStatusCode)
                {
                    ItemDiscountModel _sale_price = await response.Content.ReadAsAsync<ItemDiscountModel>();

                    return _sale_price;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}