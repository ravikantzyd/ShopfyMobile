using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Shopfy_Mobile.API_Processor;
using Shopfy_Mobile.Common_Activity;
using Shopfy_Mobile.Common_Methods;
using Shopfy_Mobile.Main_Nav;
using Shopfy_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shopfy_Mobile.Customers
{
    [Activity(Label = "CUSTOMER_REPORT_ACTIVITY", Theme = "@style/AppTheme", MainLauncher = false, NoHistory = true)]
    public class CUSTOMER_REPORT_ACTIVITY : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        RecyclerView recyclerView;
        SwipeRefreshLayout swipeRefreshLayout;
        ProgressBar progressBar;

        FloatingActionButton add_new_customer_btn;

        ImageView search_img;

        List<CustomerModel> listOfCustomer;

        List<CustomerModel> listOfAllCustomer;
        
        CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTER customer_report_adapter;

        CUSTOMER_ADD_NEW_FRAGMENT customer_add_new_fragment;
        CUSTOMER_EDIT_FRAGMENT customer_edit_fragment;

        private int selected_position = 0;

        public static int PAGE_START = 1;

        // Indicates if footer ProgressBar is shown (i.e. next page is loading)
        public static bool isLoading = false;

        // If current page is the last page (Pagination will stop after this page load)
        public static bool isLastPage = false;

        // total no. of pages to load. Initial load is page 0, after which 2 more pages will load.
        public static int TOTAL_PAGES = 0;

        // indicates the current page which Pagination is fetching.
        public static int currentPage = PAGE_START;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.customer_report_layout);
            
            listOfCustomer = new List<CustomerModel>();
            listOfAllCustomer = new List<CustomerModel>();

            isLoading = false;
            isLastPage = false;

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.customer_report_toolbar);
            recyclerView = FindViewById<AndroidX.RecyclerView.Widget.RecyclerView>(Resource.Id.customer_report_recyclerview);

            search_img = FindViewById<ImageView>(Resource.Id.customer_report_search_img);
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.customer_report_swipe_refresh_layout);
            progressBar = FindViewById<ProgressBar>(Resource.Id.customer_report_progressbar);
            add_new_customer_btn = FindViewById<FloatingActionButton>(Resource.Id.customer_add_new_btn);

            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
            search_img.Click += Search_img_Click;
            add_new_customer_btn.Click += Add_new_customer_btn_Click;

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Customers";

            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.menuaction);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            progressBar.Visibility= ViewStates.Visible;

            try
            {
                SetupRecyclerView();

                await loadFirstPage();
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }
            
            progressBar.Visibility = ViewStates.Gone;
        }

        private void Search_img_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CUSTOMER_SEARCH_ACTIVITY));
            StartActivity(intent);
        }

        private void Add_new_customer_btn_Click(object sender, EventArgs e)
        {
            customer_add_new_fragment = new CUSTOMER_ADD_NEW_FRAGMENT();

            var trans = SupportFragmentManager.BeginTransaction();
            customer_add_new_fragment.Cancelable = false;
            customer_add_new_fragment.OnRegisteredCustomer += Add_new_customer_fragment_OnRegisteredCustomer;
            customer_add_new_fragment.Show(trans, "New Customer");
        }

        private async void Add_new_customer_fragment_OnRegisteredCustomer(object sender, CUSTOMER_ADD_NEW_FRAGMENT.CreateCustomerDetailsEventArgs e)
        {
            try
            {
                CustomerModel customer_result = await CustomerProcessor.SetCustomer(e._customer);

                customer_report_adapter.AddByFragment(customer_result);

                listOfAllCustomer.Insert(1, customer_result);

                customer_add_new_fragment.Dismiss();
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            Intent intent = new Intent(this, typeof(MAIN_NAV_ACTIVITY));
            StartActivity(intent);
        }

        private async void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            try
            {
                customer_report_adapter.Clear();

                await RefreshCustomer();
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);

                swipeRefreshLayout.Refreshing = false;
            }            
        }

        private async Task RefreshCustomer()
        {
            await loadFirstPage();

            listOfCustomer.Add(new CustomerModel());
            
            isLastPage = false;

            customer_report_adapter.RefreshCustomer(listOfCustomer);

            swipeRefreshLayout.Refreshing = false;
        }

        private async Task loadFirstPage()
        {
            listOfCustomer = new List<CustomerModel>();
            listOfAllCustomer.Clear();

            currentPage = 1;

            listOfCustomer = await CustomerProcessor.LoadCustomerByPaging(currentPage);

            int customer_count = await CustomerProcessor.LoadCustomersCount();

            int leftValue = customer_count % 50;

            if (leftValue == 0)
            {
                TOTAL_PAGES = customer_count / 50;
            }
            else
            {
                TOTAL_PAGES = (customer_count / 50) + 1;
            }

            progressBar.Visibility = ViewStates.Gone;
            customer_report_adapter.AddAll(listOfCustomer);

            listOfAllCustomer.AddRange(listOfCustomer);

            if (currentPage <= TOTAL_PAGES) customer_report_adapter.AddLoadingFooter();

            else isLastPage = true;
        }

        public async void loadNextPage()
        {
            try
            {
                progressBar.Visibility = ViewStates.Visible;

                listOfCustomer = await CustomerProcessor.LoadCustomerByPaging(currentPage);

                await Task.Delay(50);

                progressBar.Visibility = ViewStates.Gone;

                customer_report_adapter.RemoveLoadingFooter();
                isLoading = false;
                customer_report_adapter.AddAll(listOfCustomer);
                listOfAllCustomer.AddRange(listOfCustomer);

                if (currentPage != TOTAL_PAGES) customer_report_adapter.AddLoadingFooter();
                else isLastPage = true;
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Intent intent = new Intent(this, typeof(MAIN_NAV_ACTIVITY));
                    StartActivity(intent);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetupRecyclerView()
        {
            LinearLayoutManager layoutManager = new LinearLayoutManager(recyclerView.Context);

            recyclerView.SetLayoutManager(layoutManager);
            customer_report_adapter = new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTER(listOfCustomer);
            customer_report_adapter.CallLayoutClick += Customer_report_adapter_CallLayoutClick;
            customer_report_adapter.EditLayoutClick += Customer_report_adapter_EditLayoutClick;
            customer_report_adapter.RemoveLayoutClick += Customer_report_adapter_RemoveLayoutClick;
            
            recyclerView.SetAdapter(customer_report_adapter);

            CUSTOMER_REPORT_PAGINATION_SCROLL_LISTENER scroll_listener = new CUSTOMER_REPORT_PAGINATION_SCROLL_LISTENER(layoutManager);
            scroll_listener.LoadNextPage += Scroll_listener_LoadNextPage;

            recyclerView.AddOnScrollListener(scroll_listener);
        }

        private void Scroll_listener_LoadNextPage(object sender, EventArgs e)
        {
            loadNextPage();
        }

        private void Customer_report_adapter_RemoveLayoutClick(object sender, CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs e)
        {
            Toast.MakeText(this, "Remove is Clicked", ToastLength.Short).Show();
        }

        private async void Customer_report_adapter_EditLayoutClick(object sender, CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs e)
        {
            customer_edit_fragment = new CUSTOMER_EDIT_FRAGMENT(listOfAllCustomer[e.Position]);

            selected_position = e.Position;
            var trans = SupportFragmentManager.BeginTransaction();
            customer_edit_fragment.Cancelable = false;
            customer_edit_fragment.OnUpdatedCustomer += Customer_edit_fragment_OnUpdatedCustomer;
            customer_edit_fragment.Show(trans, "New Customer");
        }

        private async void Customer_edit_fragment_OnUpdatedCustomer(object sender, CUSTOMER_EDIT_FRAGMENT.UpdateCustomerDetailsEventArgs e)
        {
            try
            {
                await CustomerProcessor.ModifyCustomer(e.customer.Id, new UpdateCustomerModel
                {
                    CustomerName = e.customer.CustomerName,
                    Phone = e.customer.Phone,
                    Address = e.customer.Address
                });

                customer_report_adapter.UpdateByFragment(e.customer, selected_position);

                listOfAllCustomer.RemoveAt(selected_position);
                listOfAllCustomer.Insert(selected_position, e.customer);

                customer_edit_fragment.Dismiss();
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }
        }

        private void Customer_report_adapter_CallLayoutClick(object sender, CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs e)
        {
            CustomerModel customer = listOfAllCustomer[e.Position];

            if (customer.Phone.Trim().Equals(String.Empty))
            {
                AndroidX.AppCompat.App.AlertDialog.Builder cancel_alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
                cancel_alert.SetMessage("This customer doesn't have phone number!");

                cancel_alert.SetNegativeButton("Cancel", (alert, args) =>
                {
                    cancel_alert.Dispose();
                });

                cancel_alert.Show();
            }
            else
            {
                AndroidX.AppCompat.App.AlertDialog.Builder call_alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
                call_alert.SetMessage("Call " + customer.CustomerName);

                call_alert.SetPositiveButton("Call", (alert, args) =>
                {
                    var uri = Android.Net.Uri.Parse("tel:" + customer.Phone);
                    var intent = new Intent(Intent.ActionDial, uri);
                    StartActivity(intent);
                });

                call_alert.SetNegativeButton("Cancel", (alert, args) =>
                {
                    call_alert.Dispose();
                });

                call_alert.Show();
            }
        }
    }
}