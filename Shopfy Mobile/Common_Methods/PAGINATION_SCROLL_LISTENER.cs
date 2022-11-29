using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Common_Methods
{
    public abstract class PAGINATION_SCROLL_LISTENER : RecyclerView.OnScrollListener
    {
        LinearLayoutManager layoutManager;

        public PAGINATION_SCROLL_LISTENER(LinearLayoutManager _layoutManager)
        {
            layoutManager = _layoutManager;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            int visible_item_count = layoutManager.ChildCount;
            int total_item_count = layoutManager.ItemCount;
            int first_visible_item_position = layoutManager.FindFirstVisibleItemPosition();

            if(!isLoading() && !isLastPage())
            {
                if ((visible_item_count + first_visible_item_position) >=
                    total_item_count && first_visible_item_position >= 0)
                {
                    loadMoreItems();
                }
            }
        }

        protected abstract void loadMoreItems();
        public abstract int getTotalPageCount();
        public abstract bool isLastPage();
        public abstract bool isLoading();
    }
}