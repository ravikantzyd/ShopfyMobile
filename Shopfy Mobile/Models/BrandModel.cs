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
    public class CreateBrandModel
    {
        public string BrandName { get; set; }
    }

    public class UpdateBrandModel : CreateBrandModel
    {

    }

    public class ViewByBrandNameModel : CreateBrandModel
    {

    }

    public class BrandModel : CreateBrandModel
    {
        public int Id { get; set; }

        public virtual IList<SalePriceModel> SalePrices { get; set; }
    }
}