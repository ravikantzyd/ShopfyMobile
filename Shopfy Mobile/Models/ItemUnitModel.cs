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
    public class CreateItemUnitModel
    {
        public int ItemId { get; set; }

        public int UnitId { get; set; }
    }

    public class ItemUnitModel : CreateItemUnitModel
    {
        public virtual ItemModel Items { get; set; }
        public virtual UnitModel Units { get; set; }
    }

    public class UpdateItemUnitModel : ItemUnitModel
    {

    }
}