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
    public class CUSTOMER_EDIT_FRAGMENT : AndroidX.AppCompat.App.AppCompatDialogFragment
    {
        TextInputEditText id_et;
        TextInputEditText name_et;
        TextInputEditText phone_et;
        TextInputEditText address_et;

        AppCompatButton save_btn;
        AppCompatButton cancel_btn;

        public event EventHandler<UpdateCustomerDetailsEventArgs> OnUpdatedCustomer;

        CustomerModel customer_data = new CustomerModel();

        public CUSTOMER_EDIT_FRAGMENT(CustomerModel _customer)
        {
            customer_data = _customer;
        }

        public class UpdateCustomerDetailsEventArgs : EventArgs
        {
            public CustomerModel customer { get; set; }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.customer_edit_customer_fragment, container, false);

            id_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_edit_new_id_et);
            name_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_edit_new_name_et);
            phone_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_edit_new_phone_et);
            address_et = view.FindViewById<TextInputEditText>(Resource.Id.customer_edit_new_address_et);

            save_btn = view.FindViewById<AppCompatButton>(Resource.Id.customer_edit_save_btn);
            cancel_btn = view.FindViewById<AppCompatButton>(Resource.Id.customer_edit_cancel_btn);

            save_btn.Click += Save_btn_Click;
            cancel_btn.Click += Cancel_btn_Click;

            id_et.Text = customer_data.Id.ToString();
            name_et.Text = customer_data.CustomerName;
            phone_et.Text = customer_data.Phone;
            address_et.Text = customer_data.Address;

            return view;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(id_et.Text.Trim());
            string name = name_et.Text.Trim();
            string phone = phone_et.Text.Trim();
            string address = address_et.Text.Trim();

            if (name.Equals(String.Empty))
            {
                Toast.MakeText(Activity, "Please enter Customer Name!", ToastLength.Short).Show();
            }
            else
            {
                CustomerModel _customer = new CustomerModel
                {
                    Id= id,
                    CustomerName = name,
                    Phone = phone,
                    Address = address
                };

                OnUpdatedCustomer?.Invoke(this, new UpdateCustomerDetailsEventArgs { customer = _customer });
            }
        }
    }
}