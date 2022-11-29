using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.TextField;
using Shopfy_Mobile.API_Processor;
using Shopfy_Mobile.Common_Activity;
using Shopfy_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Customers
{
    [Activity(Label = "CUSTOMER_SEARCH_ACTIVITY", Theme = "@style/SearchTheme", MainLauncher = false, NoHistory = true)]
    public class CUSTOMER_SEARCH_ACTIVITY : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        RecyclerView recyclerView;
        TextInputEditText search_et;
        ProgressBar progress_bar;

        List<CustomerModel> listOfCustomer;
        int selected_position = 0;

        CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTER customer_search_recycler_adapter;        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.customer_search_layout);
            OverridePendingTransition(Resource.Animation.trans_top_in, Resource.Animation.trans_top_out);

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.customer_search_toolbar);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.customer_search_recyclerview);
            search_et = FindViewById<TextInputEditText>(Resource.Id.customer_search_search_bar_et);
            progress_bar = FindViewById<ProgressBar>(Resource.Id.customer_search_progressbar);

            listOfCustomer = new List<CustomerModel>();

            search_et.TextChanged += Search_et_TextChanged;

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Customers";

            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.arrow_back_24);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            SetupRecyclerView();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Intent intent = new Intent(this, typeof(CUSTOMER_REPORT_ACTIVITY));
                    StartActivity(intent);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private async void Search_et_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            progress_bar.Visibility = ViewStates.Visible;

            try
            {
                string customer_start_name = search_et.Text.Trim();

                if (customer_start_name.Equals(String.Empty))
                {
                    listOfCustomer.Clear();

                    customer_search_recycler_adapter.AddAll(listOfCustomer);
                }
                else
                {
                    listOfCustomer = await CustomerProcessor.LoadCustomerBySearch(customer_start_name);

                    customer_search_recycler_adapter.AddAll(listOfCustomer);
                }
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }

            progress_bar.Visibility = ViewStates.Gone;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            Intent intent = new Intent(this, typeof(CUSTOMER_REPORT_ACTIVITY));
            StartActivity(intent);

            OverridePendingTransition(Resource.Animation.trans_bottom_in, Resource.Animation.trans_bottom_out);
        }

        private void SetupRecyclerView()
        {
            LinearLayoutManager layoutManager = new LinearLayoutManager(recyclerView.Context);

            recyclerView.SetLayoutManager(layoutManager);
            customer_search_recycler_adapter = new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTER(listOfCustomer);            

            recyclerView.SetAdapter(customer_search_recycler_adapter);
        }                
    }
}