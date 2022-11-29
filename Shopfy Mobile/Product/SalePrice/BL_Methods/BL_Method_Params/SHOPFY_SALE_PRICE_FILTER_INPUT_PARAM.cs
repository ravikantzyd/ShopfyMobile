using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Product.SalePrice.BL_Methods.BL_Method_Params
{
    public class SHOPFY_SALE_PRICE_FILTER_INPUT_PARAM : SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM
    {
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
    }
}