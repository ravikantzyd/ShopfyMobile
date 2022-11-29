using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Models
{
    public class CreateCustomerModel
    {
        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }

    public class UpdateCustomerModel : CreateCustomerModel
    {

    }

    public class ViewCustomerByNameModel
    {
        public string CustomerName { get; set; }
    }

    public class ViewCustomerByIdModel
    {
        public int CurrentPage { get; set; }
    }

    public class SearchCustomerByNameStartModel
    {
        public string StartName { get; set; }
    }

    public class CustomerModel : CreateCustomerModel
    {
        public int Id { get; set; }
    }
}