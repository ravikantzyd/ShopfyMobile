using Android.Views;
using Android.Widget;
using System;
using AndroidX.RecyclerView.Widget;
using Shopfy_Mobile.Models;
using System.Collections.Generic;

namespace Shopfy_Mobile.Customers
{
    public class CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTER : RecyclerView.Adapter
    {
        public event EventHandler<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> ItemClick;
        public event EventHandler<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> ItemLongClick;
        public event EventHandler<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> CallLayoutClick;
        public event EventHandler<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> EditLayoutClick;
        public event EventHandler<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> RemoveLayoutClick;

        List<CustomerModel> customerList;

        Random random = new Random();

        private bool isLoadingAdded = false;

        public CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTER(List<CustomerModel> data)
        {
            customerList = data;
        }

        public void RefreshCustomer(List<CustomerModel> _customerList)
        {
            customerList = _customerList;
            this.NotifyDataSetChanged();
        }

        public void Add(CustomerModel customer)
        {
            customerList.Add(customer);
            NotifyItemInserted(customerList.Count - 1);
        }

        public void AddByFragment(CustomerModel customer)
        {
            customerList.Insert(1, customer);
            NotifyItemInserted(1);
        }

        public void UpdateByFragment(CustomerModel customer,int position)
        {
            customerList.RemoveAt(position);
            NotifyItemRemoved(position);

            customerList.Insert(position, customer);
            NotifyItemInserted(position);
        }

        public void AddAll(List<CustomerModel> _customerList)
        {
            foreach (CustomerModel customer in _customerList)
            {
                Add(customer);
            }
        }

        public void Remove(CustomerModel customer)
        {
            int position = customerList.IndexOf(customer);

            if (position > -1)
            {
                customerList.RemoveAt(position);
                NotifyItemRemoved(position);
            }
        }

        public void Clear()
        {
            isLoadingAdded = false;

            while (ItemCount > 0)
            {
                Remove(GetItem(0));
            }
        }

        public bool IsEmpty()
        {
            return ItemCount == 0;
        }

        public void AddLoadingFooter()
        {
            isLoadingAdded = true;
            Add(new CustomerModel());
        }

        public void RemoveLoadingFooter()
        {
            isLoadingAdded = false;

            int position = customerList.Count - 1;
            CustomerModel item = GetItem(position);
            if (item != null)
            {
                customerList.RemoveAt(position);
                NotifyItemRemoved(position);
            }
        }

        public CustomerModel GetItem(int position)
        {
            return customerList[position];
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.customer_report_row_layout;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERViewHolder(itemView, OnClick, OnLongClick, OnCallLayoutClick,
                OnEditClick, OnRemoveClick);
            
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = customerList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERViewHolder;

            if(holder != null)
            {
                if (item.CustomerName != null)
                {
                    holder.customer_report_row_name_img.Text = item.CustomerName.ToCharArray()[0].ToString();
                }

                if(GetCustomerNameIcon() != 0)
                {
                    holder.customer_report_row_name_img.SetBackgroundResource(GetCustomerNameIcon());
                }
                
                holder.customer_report_row_name_tv.Text = "Name :      " + item.CustomerName;
                holder.customer_report_row_phone_tv.Text = "Phone :     " + item.Phone;
                holder.customer_report_row_address_tv.Text = "Address :  " + item.Address;
            }            
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

                default : return 0;
            }
        }

        public override int ItemCount => customerList.Count;

        void OnClick(CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnCallLayoutClick(CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs args) => CallLayoutClick?.Invoke(this, args);
        void OnEditClick(CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs args) => EditLayoutClick?.Invoke(this, args);
        void OnRemoveClick(CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs args) => RemoveLayoutClick?.Invoke(this, args);

    }

    public class CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERViewHolder : RecyclerView.ViewHolder
    {
        public TextView customer_report_row_name_img { get; set; }
        public TextView customer_report_row_name_tv { get; set; }
        public TextView customer_report_row_phone_tv { get; set; }
        public TextView customer_report_row_address_tv { get; set; }

        public LinearLayout customer_report_row_call_layout { get; set; }
        public RelativeLayout customer_report_row_edit_layout { get; set; }
        public RelativeLayout customer_report_row_remove_layout { get; set; }

        public CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERViewHolder(View itemView, Action<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> clickListener,
                            Action<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> longClickListener,
                            Action<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> callLayoutListener,
                            Action<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> editLayoutListener,
                            Action<CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs> removeLayoutListener) : base(itemView)
        {

            customer_report_row_name_img = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_name_icon_tv);
            customer_report_row_name_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_name_tv);
            customer_report_row_phone_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_phone_tv);
            customer_report_row_address_tv = itemView.FindViewById<TextView>(Resource.Id.customer_report_row_address_tv); ;

            customer_report_row_call_layout = itemView.FindViewById<LinearLayout>(Resource.Id.customer_report_row_call_layout);
            customer_report_row_edit_layout = itemView.FindViewById<RelativeLayout>(Resource.Id.customer_report_row_edit_layout);
            customer_report_row_remove_layout = itemView.FindViewById<RelativeLayout>(Resource.Id.customer_report_row_remove_layout);

            itemView.Click += (sender, e) => clickListener(new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            customer_report_row_call_layout.Click += (sender, e) => callLayoutListener(new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            customer_report_row_edit_layout.Click += (sender, e) => editLayoutListener(new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });
            customer_report_row_remove_layout.Click += (sender, e) => removeLayoutListener(new CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs { View = itemView, Position = AdapterPosition });

        }
    }

    public class CUSTOMER_REPORT_RECYCLER_VIEW_ADAPTERClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}