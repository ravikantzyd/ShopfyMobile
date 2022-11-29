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
    public class CreateItemImageModel
    {
        public int ItemId { get; set; }
        
        public byte[] Picture { get; set; }
    }

    public class UpdateItemImageModel : CreateItemImageModel
    {

    }

    public class ItemImageModel : CreateItemImageModel
    {
        public int Id { get; set; }
        public ItemModel Items { get; set; }
    }
}