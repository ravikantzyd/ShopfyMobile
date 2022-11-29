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
    public class ProductPriceModel
    {
        public int ItemId { get; set; }

        public int UnitId { get; set; }

        public int BrandId { get; set; }

        public int BranchId { get; set; }

        public int CityId { get; set; }

        public int SalePriceId { get; set; }

        public int ItemDisId { get; set; }

        public int CashDisId { get; set; }
    }
}