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
    public class CreatePermissionModel
    {
        public string PermissionName { get; set; }
    }

    public class ViewByPermissionNameModel
    {
        public string PermissionName { get; set; }
    }

    public class PermissionModel : CreatePermissionModel
    {
        public int Id { get; set; }

        public virtual IList<RolePermissionModel> RolePermissions { get; set; }
    }
}