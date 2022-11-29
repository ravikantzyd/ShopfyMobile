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

namespace Shopfy_Mobile.Models
{
    public class CreateBranchModel
    {
        public string BranchName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }

    public class UpdateBranchModel : CreateBranchModel
    {

    }

    public class ViewBranchByNameModel
    {
        public string BranchName { get; set; }
    }

    public class BranchModel : CreateBranchModel
    {
        public int Id { get; set; }

        public virtual IList<SalePriceModel> SalePrices { get; set; }
    }
}