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
using Shopfy_Mobile.User;
using Shopfy_Mobile.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopfy_Mobile.Product.SalePrice
{
    [Activity(Label = "PRODUCT_SALE_PRICE_SEARCH_ACTIVITY", MainLauncher = false, NoHistory = true)]
    public class PRODUCT_SALE_PRICE_SEARCH_ACTIVITY : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        RecyclerView recyclerView;
        TextInputEditText search_et;
        ProgressBar progress_bar;

        List<PRODUCT_PRICE_PROC_OUT> listOfProductPrice;
        int selected_position = 0;
        public static PreferenceManager preferenceManager;

        PRODUCT_SALE_PRICE_SEARCH_RECYCLER_VIEW_ADAPTER product_price_search_recycler_adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.product_sale_price_report_search_layout);
            OverridePendingTransition(Resource.Animation.trans_top_in, Resource.Animation.trans_top_out);

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.product_price_search_toolbar);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.product_price_search_recyclerview);
            search_et = FindViewById<TextInputEditText>(Resource.Id.product_price_search_search_bar_et);
            progress_bar = FindViewById<ProgressBar>(Resource.Id.product_price_search_progressbar);

            listOfProductPrice = new List<PRODUCT_PRICE_PROC_OUT>();

            preferenceManager = new PreferenceManager(this);

            search_et.TextChanged += Search_et_TextChanged;

            SetSupportActionBar(toolbar);
            //SupportActionBar.Title = "Search";

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
                    Intent intent = new Intent(this, typeof(PRODUCT_SALE_PRICE_REPORT_ACTIVITY));
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
                string product_price_start_name = search_et.Text.Trim();

                if (product_price_start_name.Equals(String.Empty))
                {
                    listOfProductPrice.Clear();

                    product_price_search_recycler_adapter.RefreshProductPrice(listOfProductPrice);
                }
                else
                {
                    string[] word_arr = product_price_start_name.Split(' ');

                    string search_word1 = "";
                    string search_word2 = "";
                    string search_word3 = "";
                    string search_word4 = "";

                    if (word_arr.Length == 1)
                    {
                        search_word1 = word_arr[0];
                    }

                    if (word_arr.Length == 2)
                    {
                        search_word1 = word_arr[0];
                        search_word2 = word_arr[1];
                    }

                    if (word_arr.Length == 3)
                    {
                        search_word1 = word_arr[0];
                        search_word2 = word_arr[1];
                        search_word3 = word_arr[2];
                    }

                    if (word_arr.Length == 4)
                    {
                        search_word1 = word_arr[0];
                        search_word2 = word_arr[1];
                        search_word3 = word_arr[2];
                        search_word4 = word_arr[3];
                    }

                    if (word_arr.Length > 4)
                    {
                        search_word1 = product_price_start_name;
                    }

                    int brand_id = BrandSelection.brand_id;
                    int branch_id = 0;
                    string branch_name = preferenceManager.GetString(ConstantValues.KEY_BRANCH_NAME);

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

                    PRODUCT_PRICE_SEARCH_PROC_IN search_input = new PRODUCT_PRICE_SEARCH_PROC_IN
                    {
                        StatusInfo = "Paging",
                        PageNo = 1,
                        SearchWord1 = search_word1,
                        SearchWord2 = search_word2,
                        SearchWord3 = search_word3,
                        SearchWord4 = search_word4,
                        BranchId = branch_id,
                        BrandId = brand_id,
                        SearchWordLength = word_arr.Length
                    };

                    listOfProductPrice.Clear();

                    listOfProductPrice = await ProcProductPriceProcessor.LoadLatestProductPriceListSearch(search_input);

                    product_price_search_recycler_adapter.RefreshProductPrice(listOfProductPrice);
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

            Intent intent = new Intent(this, typeof(PRODUCT_SALE_PRICE_REPORT_ACTIVITY));
            StartActivity(intent);

            OverridePendingTransition(Resource.Animation.trans_bottom_in, Resource.Animation.trans_bottom_out);
        }

        private void SetupRecyclerView()
        {
            LinearLayoutManager layoutManager = new LinearLayoutManager(recyclerView.Context);

            recyclerView.SetLayoutManager(layoutManager);
            product_price_search_recycler_adapter = new PRODUCT_SALE_PRICE_SEARCH_RECYCLER_VIEW_ADAPTER(listOfProductPrice);

            recyclerView.SetAdapter(product_price_search_recycler_adapter);
        }
    }
}