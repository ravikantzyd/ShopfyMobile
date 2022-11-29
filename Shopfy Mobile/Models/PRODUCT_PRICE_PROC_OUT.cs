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

namespace Shopfy_Mobile.Models
{
    public class PRODUCT_PRICE_PROC_OUT
    {
        public DateTime? Date { get; set; }

        public int? ItemId { get; set; } = 0;

        public string ItemName { get; set; } = "";

        public string UnitName { get; set; } = "";

        public string BranchName { get; set; } = "";

        public string BrandName { get; set; } = "";

        public string CityName { get; set; } = "";

        public string LevelName { get; set; } = "";

        public float? SalePriceAmount { get; set; } = 0;

        public float? ItemDisAmount { get; set; } = 0;

        public float? CashDisAmount { get; set; } = 0;
    }
}