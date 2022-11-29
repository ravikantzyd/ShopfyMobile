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
    public class PRODUCT_PRICE_PROC_IN
    {
        public int ItemId { get; set; } = 0;
        public int UnitId { get; set; } = 0;
        public int BranchId { get; set; } = 0;
        public int BrandId { get; set; } = 0;
        public int CityId { get; set; } = 0;
        public string StatusInfo { get; set; } = "";
        public int PageNo { get; set; } = 0;
    }
}