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
    public class DateRangeInput
    {
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
    }
}