using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shopfy_Mobile.Main_Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Common_Activity
{
    [Activity(Label = "NO_INTERNET_ACTIVITY", Theme = "@style/AppTheme", MainLauncher = false, NoHistory = true)]
    public class NO_CONNECTION_ACTIVITY : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.no_connection);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            this.Finish();
        }
    }
}