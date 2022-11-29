using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Shopfy_Mobile.Common_Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Customers
{
    public class CUSTOMER_REPORT_PAGINATION_SCROLL_LISTENER : PAGINATION_SCROLL_LISTENER
    {
        public event EventHandler LoadNextPage;
        public CUSTOMER_REPORT_PAGINATION_SCROLL_LISTENER(LinearLayoutManager _layoutManager) : base(_layoutManager)
        {
            
        }

        public override int getTotalPageCount()
        {
            return CUSTOMER_REPORT_ACTIVITY.TOTAL_PAGES;
        }

        public override bool isLastPage()
        {
            return CUSTOMER_REPORT_ACTIVITY.isLastPage;
        }

        public override bool isLoading()
        {
            return CUSTOMER_REPORT_ACTIVITY.isLoading;
        }

        protected override void loadMoreItems()
        {
            CUSTOMER_REPORT_ACTIVITY.isLoading = true;
            //Increment page index to load the next one
            CUSTOMER_REPORT_ACTIVITY.currentPage += 1;
            LoadNextPage?.Invoke(this, EventArgs.Empty);
        }
    }
}