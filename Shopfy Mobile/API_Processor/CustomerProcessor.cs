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
    public class CustomerProcessor
    {
        public static async Task<List<CustomerModel>> LoadCustomers()
        {
            string url = $"api/Customer";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CustomerModel> Customers = await response.Content.ReadAsAsync<List<CustomerModel>>();

                    return Customers;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<int> LoadCustomersCount()
        {
            string url = $"api/Customer/CustomersCount";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    int Customers = await response.Content.ReadAsAsync<int>();

                    return Customers;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CustomerModel>> LoadCustomerByPaging(int current_page)
        {
            ViewCustomerByIdModel customer = new ViewCustomerByIdModel
            {
                CurrentPage = current_page
            };

            string url = $"api/Customer/CustomersByPaging";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CustomerModel> Customers = await response.Content.ReadAsAsync<List<CustomerModel>>();

                    return Customers;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CustomerModel>> LoadCustomerBySearch(string start_name)
        {
            SearchCustomerByNameStartModel customer = new SearchCustomerByNameStartModel()
            {
                StartName = start_name
            };

            string url = $"api/Customer/CustomersBySearch";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<CustomerModel> Customers = await response.Content.ReadAsAsync<List<CustomerModel>>();

                    return Customers;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public static async Task<CustomerModel> SetCustomer(CreateCustomerModel customer)
        {
            string url = "api/Customer";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
            {
                if (response.IsSuccessStatusCode)
                {
                    CustomerModel Customers = await response.Content.ReadAsAsync<CustomerModel>();

                    return Customers;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task ModifyCustomer(int id, UpdateCustomerModel customer)
        {
            string url = $"api/Customer/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, customer))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteCustomer(int id)
        {
            string url = $"api/Customer/{id}";

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