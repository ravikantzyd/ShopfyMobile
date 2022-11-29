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
    public class CreateUnitModel
    {
        public string UnitName { get; set; }
    }

    public class UpdateUnitModel : CreateUnitModel
    {
        public virtual List<ItemUnitModel> ItemUnits { get; set; }
    }

    public class ViewByUnitNameModel : CreateUnitModel
    {

    }

    public class UnitModel : CreateUnitModel
    {
        public int Id { get; set; }

        public virtual List<ItemUnitModel> ItemUnits { get; set; }

        public virtual IList<SalePriceModel> SalePrices { get; set; }
    }
}