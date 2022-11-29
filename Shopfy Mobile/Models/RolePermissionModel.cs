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
    public class CreateRolePermissionModel
    {
        public int PermissionId { get; set; }

        public int RoleId { get; set; }
    }

    public class UpdateRolePermissionModel : CreateRolePermissionModel
    {

    }

    public class RolePermissionModel : CreateRolePermissionModel
    {
        public virtual PermissionModel Permissions { get; set; }

        public virtual RoleModel Roles { get; set; }
    }
}