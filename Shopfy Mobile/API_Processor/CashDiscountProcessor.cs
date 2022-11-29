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
    public class CashDiscountProcessor
    {
        public static async Task<CashDiscountModel> LoadCashDiscount(int cash_dis_id)
        {
            string url = $"api/CashDiscount/{cash_dis_id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    CashDiscountModel cash_dis = await response.Content.ReadAsAsync<CashDiscountModel>();

                    return cash_dis;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadLatestCashDis(SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM input)
        {
            string url = $"api/CashDiscount/LatestCashDis";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_dis = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_dis;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadLatestCashDisByPageNum(SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM input)
        {
            string url = $"api/CashDiscount/LatestCashDisByPageNum";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_dis = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_dis;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadCashDiscountByDate(DateTime fromDate, DateTime toDate)
        {
            string url = $"api/CashDiscount/CashDiscountByDate";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_diss = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_diss;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadCashDiscountByDateByDecending(DateTime fromDate, DateTime toDate)
        {
            string url = $"api/CashDiscount/CashDiscountByDateByDecending";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_diss = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_diss;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadCashDiscountByDateAndCashDiscountData(CreateCashDiscountModel salePrice)
        {
            string url = $"api/CashDiscount/CashDiscountByDateAndCashDiscountSpecificData";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, salePrice))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_diss = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_diss;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadCashDiscountByDateAndItemId(DateTime fromDate, DateTime toDate, int itemId)
        {
            string url = $"api/CashDiscount/CashDiscountByDateAndItemId?itemId={itemId}";

            DateRangeInput input = new DateRangeInput
            {
                from_date = fromDate,
                to_date = toDate
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_diss = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_diss;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CashDiscountModel>> LoadCashDiscountByFilter(SHOPFY_CASH_DIS_FILTER_INPUT_PARAM input)
        {
            string url = $"api/CashDiscount/CashDiscountByFilter";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, input))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CashDiscountModel> cash_diss = await response.Content.ReadAsAsync<List<CashDiscountModel>>();

                    return cash_diss;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<CashDiscountModel> SetCashDiscount(CreateCashDiscountModel cash_dis)
        {
            string url = "api/CashDiscount";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, cash_dis))
            {
                if (response.IsSuccessStatusCode)
                {
                    CashDiscountModel _cash_dis = await response.Content.ReadAsAsync<CashDiscountModel>();

                    return _cash_dis;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}