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
    public class SalePriceProcessor
    {
        public static async Task<SalePriceModel> LoadSalePrice(int sale_price_id)
        {
            string url = $"api/SalePrice/{sale_price_id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    SalePriceModel sale_price = await response.Content.ReadAsAsync<SalePriceModel>();

                    return sale_price;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadSalePriceByDate(DateTime fromDate, DateTime toDate)
        {
            string url = $"api/SalePrice/SalePriceByDate";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadLatestPrices(SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM input)
        {
            string url = $"api/SalePrice/LatestPrices";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadLatestPricesByPageNum(SHOPFY_SALE_PRICE_FILTER_INPUT_PARAM input)
        {
            string url = $"api/SalePrice/LatestPricesByPageNum";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadSalePriceByDateByDecending(DateTime fromDate, DateTime toDate)
        {
            string url = $"api/SalePrice/SalePriceByDateByDecending";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadSalePriceByDateAndSalePriceData(CreateSalePriceModel salePrice)
        {
            string url = $"api/SalePrice/SalePriceByDateAndSalePriceSpecificData";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, salePrice))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadSalePriceByDateAndItemId(DateTime fromDate, DateTime toDate, int itemId)
        {
            string url = $"api/SalePrice/SalePriceByDateAndItemId?itemId={itemId}";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<SalePriceModel>> LoadSalePriceByFilter(SHOPFY_SALE_PRICE_FILTER_INPUT_PARAM input)
        {
            string url = $"api/SalePrice/SalePriceByFilter";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<SalePriceModel> sale_prices = await response.Content.ReadAsAsync<List<SalePriceModel>>();

                    return sale_prices;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<SalePriceModel> SetSalePrice(CreateSalePriceModel sale_price)
        {
            string url = "api/SalePrice";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, sale_price))
            {
                if (response.IsSuccessStatusCode)
                {
                    SalePriceModel _sale_price = await response.Content.ReadAsAsync<SalePriceModel>();

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