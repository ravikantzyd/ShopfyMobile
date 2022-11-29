using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using ImageViews.Rounded;
using Shopfy_Mobile.API_Processor;
using Shopfy_Mobile.Models;
using Shopfy_Mobile.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopfy_Mobile.Product.SalePrice
{
    internal class PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTER : RecyclerView.Adapter
    {
        List<PRODUCT_PRICE_PROC_OUT> productPriceList;

        Random random = new Random();

        private bool isLoadingAdded = false;

        private List<ItemImageModel> loadedItemImages = new List<ItemImageModel>();

        public PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTER(List<PRODUCT_PRICE_PROC_OUT> data)
        {
            productPriceList = data;
        }

        public void RefreshProductPrice(List<PRODUCT_PRICE_PROC_OUT> _productPriceList)
        {
            productPriceList = _productPriceList;
            this.NotifyDataSetChanged();
        }

        public void Add(PRODUCT_PRICE_PROC_OUT productPrice)
        {
            productPriceList.Add(productPrice);
            NotifyItemInserted(productPriceList.Count - 1);
        }

        public void AddByFragment(PRODUCT_PRICE_PROC_OUT productPrice)
        {
            productPriceList.Insert(1, productPrice);
            NotifyItemInserted(1);
        }

        public void UpdateByFragment(PRODUCT_PRICE_PROC_OUT productPrice, int position)
        {
            productPriceList.RemoveAt(position);
            NotifyItemRemoved(position);

            productPriceList.Insert(position, productPrice);
            NotifyItemInserted(position);
        }

        public void AddAll(List<PRODUCT_PRICE_PROC_OUT> _productPriceList)
        {
            foreach (PRODUCT_PRICE_PROC_OUT productPrice in _productPriceList)
            {
                Add(productPrice);
            }
        }

        public void Remove(PRODUCT_PRICE_PROC_OUT productPrice)
        {
            int position = productPriceList.IndexOf(productPrice);

            if (position > -1)
            {
                productPriceList.RemoveAt(position);
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
            Add(new PRODUCT_PRICE_PROC_OUT());
        }

        public void RemoveLoadingFooter()
        {
            isLoadingAdded = false;

            int position = productPriceList.Count - 1;
            PRODUCT_PRICE_PROC_OUT item = GetItem(position);
            if (item != null)
            {
                productPriceList.RemoveAt(position);
                NotifyItemRemoved(position);
            }
        }

        public PRODUCT_PRICE_PROC_OUT GetItem(int position)
        {
            return productPriceList[position];
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.product_sale_price_report_row_layout;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTERViewHolder(itemView);

            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override async void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var sale_price_data = productPriceList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTERViewHolder;

            if (holder != null)
            {
                //if (sale_price_data.BrandName != null)
                //{
                //    holder.product_sale_price_report_row_brand_name_tv.Text = "Brand Name       : " + sale_price_data.BrandName.ToString();
                //}
                //else
                //{
                //    holder.product_sale_price_report_row_brand_name_tv.Text = "Brand Name       : Unknown";
                //}                

                if (sale_price_data.ItemName != null)
                {
                    holder.product_sale_price_report_row_item_name_tv.Text = "Product Name   : " + sale_price_data.ItemName;

                    int item_id = (int)sale_price_data.ItemId;

                    if (loadedItemImages.Where(img => img.ItemId == item_id).ToList().Count == 0)
                    {
                        ItemImageModel item_image = await ItemImageProcessor.LoadItemImageByItemId(item_id);

                        if (item_image != null)
                        {
                            holder.product_sale_price_item_img.SetImageBitmap(GetBitmapFromEncodedString(item_image.Picture));
                            loadedItemImages.Add(item_image);
                        }
                        else
                        {
                            holder.product_sale_price_item_img.SetImageBitmap(null);
                        }
                    }
                    else
                    {
                        byte[] picture = loadedItemImages.Where(item => item.ItemId == item_id)
                                .FirstOrDefault().Picture;
                        
                        if (picture != null)
                        {
                            holder.product_sale_price_item_img.SetImageBitmap(
                            GetBitmapFromEncodedString(picture));
                        }                        
                    }
                    
                }
                else
                {
                    holder.product_sale_price_report_row_item_name_tv.Text = "Product Name   : Unknown";
                }

                if (sale_price_data.UnitName != null)
                {
                    holder.product_sale_price_report_row_unit_tv.Text = "Unit                      : " + sale_price_data.UnitName;
                }
                else
                {
                    holder.product_sale_price_report_row_unit_tv.Text = "Unit                      : Unknown";
                }

                if (sale_price_data.SalePriceAmount != null)
                {
                    holder.product_sale_price_report_row_price_tv.Text = "Price                    : " + sale_price_data.SalePriceAmount.ToString();
                }
                else
                {
                    holder.product_sale_price_report_row_price_tv.Text = "Price                    : 0";
                }

                if (sale_price_data.ItemDisAmount != null)
                {
                    holder.product_sale_price_report_row_item_dis_tv.Text = "Item Discount    : " + sale_price_data.ItemDisAmount.ToString()+" %";
                }
                else
                {
                    holder.product_sale_price_report_row_item_dis_tv.Text = "Item Discount    : 0 %";
                }

                if (sale_price_data.CashDisAmount != null)
                {
                    holder.product_sale_price_report_row_cash_dis_tv.Text = "Cash Discount   : " + sale_price_data.CashDisAmount.ToString()+" %";
                }
                else
                {
                    holder.product_sale_price_report_row_cash_dis_tv.Text = "Cash Discount   : 0 %";
                }

                if (PRODUCT_SALE_PRICE_REPORT_ACTIVITY.preferenceManager.GetString(ConstantValues.KEY_ROLE_NAME) == "Admin")
                {
                    holder.product_sale_price_report_row_branch_tv.Visibility = ViewStates.Visible;

                    if (sale_price_data.BranchName != null)
                    {
                        holder.product_sale_price_report_row_branch_tv.Text = "Branch                 : " + sale_price_data.BranchName;
                    }
                    else
                    {
                        holder.product_sale_price_report_row_branch_tv.Text = "Branch                 : Unknown";
                    }
                }
                
                if (sale_price_data.LevelName != null)
                {
                    holder.product_sale_price_report_row_level_tv.Text = "Level                  : " + sale_price_data.LevelName;
                }
                else
                {
                    holder.product_sale_price_report_row_level_tv.Text = "Level                  : Unknown";
                }

                if (sale_price_data.CityName != null)
                {
                    holder.product_sale_price_report_row_city_tv.Text = "City                       : " + sale_price_data.CityName;
                }
                else
                {
                    holder.product_sale_price_report_row_city_tv.Text = "City                       : Unknown";
                }
            }
        }

        private Bitmap GetBitmapFromEncodedString(byte[] encoded_img)
        {            
            return BitmapFactory.DecodeByteArray(encoded_img, 0, encoded_img.Length);
        }

        public override int ItemCount => productPriceList.Count;
    }

    public class PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTERViewHolder : RecyclerView.ViewHolder
    {
        public TextView product_sale_price_report_row_brand_name_tv { get; set; }
        public TextView product_sale_price_report_row_item_name_tv { get; set; }
        public TextView product_sale_price_report_row_unit_tv { get; set; }
        public TextView product_sale_price_report_row_price_tv { get; set; }
        public TextView product_sale_price_report_row_item_dis_tv { get; set; }
        public TextView product_sale_price_report_row_cash_dis_tv { get; set; }
        public TextView product_sale_price_report_row_branch_tv { get; set; }
        public TextView product_sale_price_report_row_city_tv { get; set; }
        public TextView product_sale_price_report_row_level_tv { get; set; }

        public RoundedImageView product_sale_price_item_img { get; set; }

        public PRODUCT_SALE_PRICE_REPORT_RECYCLER_VIEW_ADAPTERViewHolder(View itemView) : base(itemView)
        {
            product_sale_price_report_row_brand_name_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_brand_name_tv);
            product_sale_price_report_row_item_name_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_product_name_tv);
            product_sale_price_report_row_unit_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_unit_tv);
            product_sale_price_report_row_price_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_price_tv);
            product_sale_price_report_row_item_dis_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_item_dis_tv);
            product_sale_price_report_row_cash_dis_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_cash_dis_tv);
            product_sale_price_report_row_branch_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_branch_tv);
            product_sale_price_report_row_city_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_city_tv);
            product_sale_price_report_row_level_tv = itemView.FindViewById<TextView>(Resource.Id.product_sale_price_report_row_level_tv);
            product_sale_price_item_img = itemView.FindViewById<RoundedImageView>(Resource.Id.product_sale_price_img);
        }
    }
}