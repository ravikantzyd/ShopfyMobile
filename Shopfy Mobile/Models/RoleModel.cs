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
    public class CreateRoleModel
    {
        public string RoleName { get; set; }
    }

    public class UpdateRoleModel : CreateRoleModel
    {

    }

    public class ViewByRoleNameModel
    {
        public string RoleName { get; set; }
    }

    public class RoleModel : CreateRoleModel
    {
        public int Id { get; set; }

        public virtual IList<RolePermissionModel> RolePermissions { get; set; }
    }
}