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
    public class CreateItemDiscountModel
    {
        public DateTime Date { get; set; }

        public int ItemId { get; set; }

        public int UnitId { get; set; }

        public int BrandId { get; set; }

        public int BranchId { get; set; }

        public int CityId { get; set; }

        public float ItemDis { get; set; }
    }

    public class ItemDiscountModel : CreateItemDiscountModel
    {
        public int Id { get; set; }

        public virtual ItemModel Items { get; set; }
        public virtual UnitModel Units { get; set; }
        public virtual BrandModel Brands { get; set; }
        public virtual BranchModel Branches { get; set; }
        public virtual CityModel Cities { get; set; }
    }
}