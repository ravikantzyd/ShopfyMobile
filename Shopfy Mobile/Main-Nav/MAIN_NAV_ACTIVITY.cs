using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Shopfy_Mobile.Customers;
using Shopfy_Mobile.Product.SalePrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Main_Nav
{
    [Activity(Label = "MAIN_NAV_ACTIVITY", Theme = "@style/AppTheme", MainLauncher = false, NoHistory = true)]
    public class MAIN_NAV_ACTIVITY : AppCompatActivity
    {
        LinearLayout sale_menu_layout;
        LinearLayout customer_menu_layout;
        LinearLayout stock_menu_layout;
        LinearLayout product_menu_layout;
        LinearLayout cashier_menu_layout;

        LinearLayout sale_sub_menu_layout;
        LinearLayout customer_sub_menu_layout;
        LinearLayout stock_sub_menu_layout;
        LinearLayout product_sub_menu_layout;
        LinearLayout cashier_sub_menu_layout;

        LinearLayout customer_report_sub_menu_item_layout;
        LinearLayout product_sale_price_sub_menu_item_layout;

        ImageView sale_expend_img;
        ImageView customer_expend_img;
        ImageView stock_expend_img;
        ImageView product_expend_img;
        ImageView cashier_expend_img;

        List<LinearLayout> sub_menu_layout_list;
        List<ImageView> menu_expend_img_list;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main_navigation_drawer_layout);
            OverridePendingTransition(Resource.Animation.trans_right_in, Resource.Animation.trans_right_out);

            sale_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_sale_menu_layout);
            customer_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_customer_menu_layout);
            stock_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_stock_menu_layout);
            product_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_product_menu_layout);
            cashier_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_cashier_menu_layout);

            sale_sub_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_sale_sub_menu_layout);
            customer_sub_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_customer_sub_menu_layout);
            stock_sub_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_stock_sub_menu_layout);
            product_sub_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_product_sub_menu_layout);
            cashier_sub_menu_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_cashier_sub_menu_layout);

            sale_expend_img = FindViewById<ImageView>(Resource.Id.main_nav_sale_menu_expend_img);
            customer_expend_img = FindViewById<ImageView>(Resource.Id.main_nav_customer_menu_expend_img);
            stock_expend_img = FindViewById<ImageView>(Resource.Id.main_nav_stock_menu_expend_img);
            product_expend_img = FindViewById<ImageView>(Resource.Id.main_nav_product_menu_expend_img);
            cashier_expend_img = FindViewById<ImageView>(Resource.Id.main_nav_cashier_menu_expend_img);

            customer_report_sub_menu_item_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_customer_report_sub_menu_item_layout);
            product_sale_price_sub_menu_item_layout = FindViewById<LinearLayout>(Resource.Id.main_nav_sale_price_sub_menu_item_layout);

            sub_menu_layout_list = new List<LinearLayout>();

            sub_menu_layout_list.Add(sale_sub_menu_layout);
            sub_menu_layout_list.Add(customer_sub_menu_layout);
            sub_menu_layout_list.Add(stock_sub_menu_layout);
            sub_menu_layout_list.Add(product_sub_menu_layout);
            sub_menu_layout_list.Add(cashier_sub_menu_layout);

            menu_expend_img_list = new List<ImageView>();

            menu_expend_img_list.Add(sale_expend_img);
            menu_expend_img_list.Add(customer_expend_img);
            menu_expend_img_list.Add(stock_expend_img);
            menu_expend_img_list.Add(product_expend_img);
            menu_expend_img_list.Add(cashier_expend_img);

            sale_menu_layout.Click += Sale_menu_layout_Click;
            customer_menu_layout.Click += Customer_menu_layout_Click;
            stock_menu_layout.Click += Stock_menu_layout_Click;
            product_menu_layout.Click += Product_menu_layout_Click;
            cashier_menu_layout.Click += Cashier_menu_layout_Click;

            customer_report_sub_menu_item_layout.Click += Customer_report_sub_menu_item_layout_Click;
            product_sale_price_sub_menu_item_layout.Click += Product_sale_price_sub_menu_item_layout_Click;
        }

        private void Product_sale_price_sub_menu_item_layout_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PRODUCT_SALE_PRICE_REPORT_ACTIVITY));
            StartActivity(intent);
        }

        private void Customer_report_sub_menu_item_layout_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CUSTOMER_REPORT_ACTIVITY));
            StartActivity(intent);
        }

        private void Cashier_menu_layout_Click(object sender, EventArgs e)
        {
            MAIN_NAV_MENU_SELECTION_PARAM param = new MAIN_NAV_MENU_SELECTION_PARAM
            {
                selected_sub_menu_layout = cashier_sub_menu_layout,
                sub_menu_layout_list = sub_menu_layout_list,
                selected_menu_img = cashier_expend_img,
                selected_menu_img_list = menu_expend_img_list
            };

            MAIN_NAV_MENU_SELECTION menu_selection = new MAIN_NAV_MENU_SELECTION();
            menu_selection.Menu_Selection(param);
        }

        private void Product_menu_layout_Click(object sender, EventArgs e)
        {
            MAIN_NAV_MENU_SELECTION_PARAM param = new MAIN_NAV_MENU_SELECTION_PARAM
            {
                selected_sub_menu_layout = product_sub_menu_layout,
                sub_menu_layout_list = sub_menu_layout_list,
                selected_menu_img = product_expend_img,
                selected_menu_img_list = menu_expend_img_list
            };

            MAIN_NAV_MENU_SELECTION menu_selection = new MAIN_NAV_MENU_SELECTION();
            menu_selection.Menu_Selection(param);
        }

        private void Stock_menu_layout_Click(object sender, EventArgs e)
        {
            MAIN_NAV_MENU_SELECTION_PARAM param = new MAIN_NAV_MENU_SELECTION_PARAM
            {
                selected_sub_menu_layout = stock_sub_menu_layout,
                sub_menu_layout_list = sub_menu_layout_list,
                selected_menu_img = stock_expend_img,
                selected_menu_img_list = menu_expend_img_list
            };

            MAIN_NAV_MENU_SELECTION menu_selection = new MAIN_NAV_MENU_SELECTION();
            menu_selection.Menu_Selection(param);
        }

        private void Customer_menu_layout_Click(object sender, EventArgs e)
        {
            MAIN_NAV_MENU_SELECTION_PARAM param = new MAIN_NAV_MENU_SELECTION_PARAM
            {
                selected_sub_menu_layout = customer_sub_menu_layout,
                sub_menu_layout_list = sub_menu_layout_list,
                selected_menu_img = customer_expend_img,
                selected_menu_img_list = menu_expend_img_list
            };

            MAIN_NAV_MENU_SELECTION menu_selection = new MAIN_NAV_MENU_SELECTION();
            menu_selection.Menu_Selection(param);
        }

        private void Sale_menu_layout_Click(object sender, EventArgs e)
        {
            MAIN_NAV_MENU_SELECTION_PARAM param = new MAIN_NAV_MENU_SELECTION_PARAM
            {
                selected_sub_menu_layout = sale_sub_menu_layout,
                sub_menu_layout_list = sub_menu_layout_list,
                selected_menu_img = sale_expend_img,
                selected_menu_img_list = menu_expend_img_list
            };

            MAIN_NAV_MENU_SELECTION menu_selection = new MAIN_NAV_MENU_SELECTION();
            menu_selection.Menu_Selection(param);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            OverridePendingTransition(Resource.Animation.trans_left_in, Resource.Animation.trans_left_out);
        }
    }
}