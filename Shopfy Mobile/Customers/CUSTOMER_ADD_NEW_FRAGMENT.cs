using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.TextField;
using Shopfy_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Customers
{
    public class CUSTOMER_ADD_NEW_FRAGMENT : AndroidX.AppCompat.App.AppCompatDialogFragment
    {
        TextInputEditText name_et;
        TextInputEditText phone_et;
        TextInputEditText address_et;

        AppCompatButton save_btn;
        AppCompatButton cancel_btn;

        public event EventHandler<CreateCustomerDetailsEventArgs> OnRegisteredCustomer;

        public class CreateCustomerDetailsEventArgs : EventArgs
        {
            public CreateCustomerModel _customer { get; set; }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.customer_add_new_fragment, container, false);

            name_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_add_new_name_et);
            phone_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_add_new_phone_et);
            address_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_add_new_address_et);

            save_btn = view.FindViewById<AppCompatButton>(Resource.Id.customer_add_new_save_btn);
            cancel_btn = view.FindViewById<AppCompatButton>(Resource.Id.customer_add_new_cancel_btn);

            save_btn.Click += Save_btn_Click;
            
            cancel_btn.Click += Cancel_btn_Click;

            return view;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            string customer_name = name_et.Text.Trim();
            string phone_no = phone_et.Text.Trim();
            string address = address_et.Text.Trim();

            if (customer_name.Equals(String.Empty))
            {
                Toast.MakeText(Activity, "Please enter Customer Name!",ToastLength.Short).Show();
            }
            else
            {
                CreateCustomerModel customer = new CreateCustomerModel
                {
                    CustomerName = customer_name,
                    Phone = phone_no,
                    Address = address
                };

                OnRegisteredCustomer?.Invoke(this, new CreateCustomerDetailsEventArgs { _customer = customer });
            }
        }
    }
}