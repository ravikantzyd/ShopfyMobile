using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Shopfy_Mobile.API_Common_Methods;
using Shopfy_Mobile.API_Processor;
using Shopfy_Mobile.Common_Activity;
using Shopfy_Mobile.Main_Nav;
using Shopfy_Mobile.Models;
using Shopfy_Mobile.Product.SalePrice.BL_Methods.BL_Method_Params;
using Shopfy_Mobile.User;
using Shopfy_Mobile.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopfy_Mobile.Product.SalePrice
{
    [Activity(Label = "PRODUCT_SALE_PRICE_REPORT_ACTIVITY")]
    public class PRODUCT_SALE_PRICE_REPORT_ACTIVITY : AppCompatActivity
    {
        #region"Declaration"

        AndroidX.AppCompat.Widget.Toolbar toolbar;
        RecyclerView recyclerView;
        SwipeRefreshLayout swipeRefreshLayout;
        ProgressBar progressBar;

        ImageView search_img;

        List<PRODUCT_PRICE_PROC_OUT> listOfProductPrice;

        List<PRODUCT_PRICE_PROC_OUT> listOfAllProductPrice;

        PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTER product_price_report_adapter;

        private int selected_position = 0;

        public static PreferenceManager preferenceManager;

        public static int PAGE_START = 1;

        // Indicates if footer ProgressBar is shown (i.e. next page is loading)
        public static bool isLoading = false;

        // If current page is the last page (Pagination will stop after this page load)
        public static bool isLastPage = false;

        // total no. of pages to load. Initial load is page 0, after which 2 more pages will load.
        public static int TOTAL_PAGES = 0;

        // indicates the current page which Pagination is fetching.
        public static int currentPage = PAGE_START;

        #endregion

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.product_sale_price_report_layout);

            listOfProductPrice = new List<PRODUCT_PRICE_PROC_OUT>();
            listOfAllProductPrice = new List<PRODUCT_PRICE_PROC_OUT>();

            preferenceManager = new PreferenceManager(this);

            isLoading = false;
            isLastPage = false;

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.product_sale_price_report_toolbar);
            recyclerView = FindViewById<AndroidX.RecyclerView.Widget.RecyclerView>(Resource.Id.product_sale_price_report_recyclerview);

            search_img = FindViewById<ImageView>(Resource.Id.product_sale_price_report_search_img);
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.product_sale_price_report_swipe_refresh_layout);
            progressBar = FindViewById<ProgressBar>(Resource.Id.product_sale_price_report_progressbar);

            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
            search_img.Click += Search_img_Click;

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Product Prices";

            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.menuaction);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            progressBar.Visibility = ViewStates.Visible;

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
            Intent intent = new Intent(this, typeof(PRODUCT_SALE_PRICE_SEARCH_ACTIVITY));
            StartActivity(intent);
        }

        private async void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            try
            {
                product_price_report_adapter.Clear();

                await RefreshCustomer();
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);

                swipeRefreshLayout.Refreshing = false;
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private async Task RefreshCustomer()
        {
            await loadFirstPage();

            listOfProductPrice.Add(new PRODUCT_PRICE_PROC_OUT());

            isLastPage = false;

            product_price_report_adapter.RefreshProductPrice(listOfProductPrice);

            swipeRefreshLayout.Refreshing = false;
        }

        private async Task loadFirstPage()
        {
            listOfProductPrice = new List<PRODUCT_PRICE_PROC_OUT>();
            listOfAllProductPrice.Clear();

            currentPage = 1;

            int brand_id = BrandSelection.brand_id;
            int branch_id = 0;
            string branch_name = new PreferenceManager(this).GetString(ConstantValues.KEY_BRANCH_NAME);

            if ((branch_name != null) && (branch_name != String.Empty))
            {
                if(preferenceManager.GetString(ConstantValues.KEY_ROLE_NAME) == "Admin")
                {
                    branch_id = 0;
                }
                else
                {
                    BranchModel branch = await BranchProcessor.LoadBranchByName(branch_name);

                    if (branch != null)
                    {
                        branch_id = branch.Id;
                    }
                }                
            }

            PRODUCT_PRICE_PROC_IN input = new PRODUCT_PRICE_PROC_IN
            {
                ItemId = 0,
                UnitId = 0,
                BranchId = branch_id,
                BrandId = brand_id,
                CityId = 0,
                StatusInfo = "Paging",
                PageNo = currentPage
            };

            listOfProductPrice = await GetProductPriceList(input);

            List<COUNT_PROC_OUT> _customer_count = await ProcProductPriceProcessor.LoadLatestProductPriceListCount(input);

            int customer_count = _customer_count[0].Count;

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
            product_price_report_adapter.AddAll(listOfProductPrice);

            listOfAllProductPrice.AddRange(listOfProductPrice);

            if (currentPage <= TOTAL_PAGES) product_price_report_adapter.AddLoadingFooter();

            else isLastPage = true;
        }

        public async void loadNextPage()
        {
            try
            {
                progressBar.Visibility = ViewStates.Visible;

                int brand_id = BrandSelection.brand_id;
                int branch_id = 0;
                string branch_name = new PreferenceManager(this).GetString(ConstantValues.KEY_BRANCH_NAME);

                if ((branch_name != null) && (branch_name != String.Empty))
                {
                    if (preferenceManager.GetString(ConstantValues.KEY_ROLE_NAME) == "Admin")
                    {
                        branch_id = 0;
                    }
                    else
                    {
                        BranchModel branch = await BranchProcessor.LoadBranchByName(branch_name);

                        if (branch != null)
                        {
                            branch_id = branch.Id;
                        }
                    }
                }

                PRODUCT_PRICE_PROC_IN input = new PRODUCT_PRICE_PROC_IN
                {
                    ItemId = 0,
                    UnitId = 0,
                    BranchId = branch_id,
                    BrandId = brand_id,
                    CityId = 0,
                    StatusInfo = "Paging",
                    PageNo = currentPage
                };

                listOfProductPrice = await GetProductPriceList(input);

                await Task.Delay(50);

                progressBar.Visibility = ViewStates.Gone;

                product_price_report_adapter.RemoveLoadingFooter();
                isLoading = false;
                product_price_report_adapter.AddAll(listOfProductPrice);
                listOfAllProductPrice.AddRange(listOfProductPrice);

                if (currentPage != TOTAL_PAGES) product_price_report_adapter.AddLoadingFooter();
                else isLastPage = true;
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }
        }

        private async Task<List<PRODUCT_PRICE_PROC_OUT>> GetProductPriceList(PRODUCT_PRICE_PROC_IN input)
        {            
            List<PRODUCT_PRICE_PROC_OUT> product_prices = await ProcProductPriceProcessor.LoadLatestProductPriceList(input);

            return product_prices;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //Intent intent = new Intent(this, typeof(MAIN_NAV_ACTIVITY));
                    //StartActivity(intent);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetupRecyclerView()
        {
            LinearLayoutManager layoutManager = new LinearLayoutManager(recyclerView.Context);

            recyclerView.SetLayoutManager(layoutManager);
            product_price_report_adapter = new PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTER(listOfProductPrice);
            
            recyclerView.SetAdapter(product_price_report_adapter);

            PRODUCT_SALE_PRICE_REPORT_PAGINATION_SCROLL_LISTENER scroll_listener = new PRODUCT_SALE_PRICE_REPORT_PAGINATION_SCROLL_LISTENER(layoutManager);
            scroll_listener.LoadNextPage += Scroll_listener_LoadNextPage;

            recyclerView.AddOnScrollListener(scroll_listener);
        }

        private void Scroll_listener_LoadNextPage(object sender, EventArgs e)
        {
            loadNextPage();
        }

    }
}