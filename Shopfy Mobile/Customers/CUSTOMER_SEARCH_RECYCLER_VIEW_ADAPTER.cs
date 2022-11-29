using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Shopfy_Mobile.Models;
using System;
using System.Collections.Generic;

namespace Shopfy_Mobile.Customers
{
    public class CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTER : RecyclerView.Adapter
    {
        public event EventHandler<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> ItemClick;
        public event EventHandler<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> ItemLongClick;
        public event EventHandler<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> CallLayoutClick;
        public event EventHandler<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> EditLayoutClick;
        public event EventHandler<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> RemoveLayoutClick;

        List<CustomerModel> customerList=new List<CustomerModel>();

        Random random = new Random();

        public CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTER(List<CustomerModel> data)
        {
            customerList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.customer_report_row_layout;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERViewHolder(itemView, OnClick, OnLongClick, OnCallLayoutClick,
                OnEditClick, OnRemoveClick);

            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = customerList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERViewHolder;

            if (holder != null)
            {
                if (item.CustomerName != null)
                {
                    holder.customer_report_row_name_img.Text = item.CustomerName.ToCharArray()[0].ToString();
                }

                if (GetCustomerNameIcon() != 0)
                {
                    holder.customer_report_row_name_img.SetBackgroundResource(GetCustomerNameIcon());
                }

                holder.customer_report_row_name_tv.Text = "Name :      " + item.CustomerName;
                holder.customer_report_row_phone_tv.Text = "Phone :     " + item.Phone;
                holder.customer_report_row_address_tv.Text = "Address :  " + item.Address;
            }
        }

        public void AddAll(List<CustomerModel> _customer_list)
        {
            customerList = _customer_list;
            NotifyDataSetChanged();
        }

        public void UpdateByFragment(CustomerModel customer, int position)
        {
            customerList.RemoveAt(position);
            NotifyItemRemoved(position);

            customerList.Insert(position, customer);
            NotifyItemInserted(position);
        }

        private int GetCustomerNameIcon()
        {
            switch (random.Next(6))
            {
                case 0:
                    return Resource.Drawable.customer_report_name_icon;

                case 1:
                    return Resource.Drawable.customer_report_name_icon_1;

                case 2:
                    return Resource.Drawable.customer_report_name_icon_2;

                case 3:
                    return Resource.Drawable.customer_report_name_icon_3;

                case 4:
                    return Resource.Drawable.customer_report_name_icon_4;

                case 5:
                    return Resource.Drawable.customer_report_name_icon_5;

                default: return 0;
            }
        }

        public override int ItemCount => customerList.Count;

        void OnClick(CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnCallLayoutClick(CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs args) => CallLayoutClick?.Invoke(this, args);
        void OnEditClick(CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs args) => EditLayoutClick?.Invoke(this, args);
        void OnRemoveClick(CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs args) => RemoveLayoutClick?.Invoke(this, args);

    }

    public class CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERViewHolder : RecyclerView.ViewHolder
    {
        public TextView customer_report_row_name_img { get; set; }
        public TextView customer_report_row_name_tv { get; set; }
        public TextView customer_report_row_phone_tv { get; set; }
        public TextView customer_report_row_address_tv { get; set; }

        public TextView call_tv { get; set; }
        public TextView edit_tv { get; set; }
        public TextView remove_tv { get; set; }

        public LinearLayout customer_report_row_call_layout { get; set; }
        public RelativeLayout customer_report_row_edit_layout { get; set; }
        public RelativeLayout customer_report_row_remove_layout { get; set; }

        public CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERViewHolder(View itemView, Action<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> clickListener,
                            Action<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> longClickListener,
                            Action<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> callLayoutListener,
                            Action<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> editLayoutListener,
                            Action<CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs> removeLayoutListener) : base(itemView)
        {

            customer_report_row_name_img = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_name_icon_tv);
            customer_report_row_name_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_name_tv);
            customer_report_row_phone_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_phone_tv);
            customer_report_row_address_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_address_tv); ;

            call_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_call_tv);
            edit_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_edit_tv);
            remove_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_remove_tv);

            customer_report_row_call_layout = itemView.FindViewById<LinearLayout>(Resource.Id.customer_report_row_call_layout);
            customer_report_row_edit_layout = itemView.FindViewById<RelativeLayout>(Resource.Id.customer_report_row_edit_layout);
            customer_report_row_remove_layout = itemView.FindViewById<RelativeLayout>(Resource.Id.customer_report_row_remove_layout);

            itemView.Click += (sender, e) => clickListener(new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            customer_report_row_call_layout.Click += (sender, e) => callLayoutListener(new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            customer_report_row_edit_layout.Click += (sender, e) => editLayoutListener(new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            customer_report_row_remove_layout.Click += (sender, e) => removeLayoutListener(new CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });

            customer_report_row_name_img.SetTextColor(Android.Graphics.Color.Maroon);
            customer_report_row_name_tv.SetTextColor(Android.Graphics.Color.Maroon);
            customer_report_row_phone_tv.SetTextColor(Android.Graphics.Color.Maroon);
            customer_report_row_address_tv.SetTextColor(Android.Graphics.Color.Maroon);

            call_tv.SetTextColor(Android.Graphics.Color.Maroon);
            edit_tv.SetTextColor(Android.Graphics.Color.Maroon);
            remove_tv.SetTextColor(Android.Graphics.Color.Maroon);
        }
    }

    public class CUSTOMER_SEARCH_RECYCLER_VIEW_ADAPTERClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}