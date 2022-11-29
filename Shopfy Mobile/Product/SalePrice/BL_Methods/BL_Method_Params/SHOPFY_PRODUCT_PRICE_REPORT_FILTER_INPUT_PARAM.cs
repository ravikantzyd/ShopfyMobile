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
    public class SHOPFY_PRODUCT_PRICE_REPORT_FILTER_INPUT_PARAM
    {
        public int item_id { get; set; } = 0;
        public int unit_id { get; set; } = 0;
        public int branch_id { get; set; } = 0;
        public int brand_id { get; set; } = 0;
        public int city_id { get; set; } = 0;
        public int current_page_no { get; set; } = 0;
    }
}