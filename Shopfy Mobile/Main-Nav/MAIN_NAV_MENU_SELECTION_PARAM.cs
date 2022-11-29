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
    public class MAIN_NAV_MENU_SELECTION_PARAM
    {
        public List<LinearLayout> sub_menu_layout_list;
        public LinearLayout selected_sub_menu_layout;
        public ImageView selected_menu_img;
        public List<ImageView> selected_menu_img_list;
    }
}