using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Main_Nav
{
    public class MAIN_NAV_MENU_SELECTION
    {
        public void Menu_Selection(MAIN_NAV_MENU_SELECTION_PARAM mp)
        {
            if (mp.selected_sub_menu_layout.Visibility == ViewStates.Visible)
            {
                mp.selected_sub_menu_layout.Visibility = ViewStates.Gone;
                mp.selected_menu_img.SetImageResource(Resource.Drawable.baseline_expand_more_24);
            }
            else
            {
                mp.selected_sub_menu_layout.Visibility = ViewStates.Visible;
                mp.selected_menu_img.SetImageResource(Resource.Drawable.baseline_expand_less_24);
            }

            foreach (LinearLayout sub_menu_layout in mp.sub_menu_layout_list)
            {
                if (!mp.selected_sub_menu_layout.Id.Equals(sub_menu_layout.Id))
                {
                    if (sub_menu_layout.Visibility == ViewStates.Visible)
                    {
                        sub_menu_layout.Visibility = ViewStates.Gone;                        
                    }
                }
            }

            foreach (ImageView menu_img in mp.selected_menu_img_list)
            {
                if (!mp.selected_menu_img.Id.Equals(menu_img.Id))
                {
                    menu_img.SetImageResource(Resource.Drawable.baseline_expand_more_24);
                }
            }
        }
    }
}