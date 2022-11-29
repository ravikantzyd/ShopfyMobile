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
    public class CreateUserModel
    {       
        public string UserNameId { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public int BranchId { get; set; }
    }

    public class UpdateUserModel : CreateUserModel
    {

    }

    public class ViewUserByNameModel
    {        
        public string UserNameId { get; set; }
    }

    public class UserModel : CreateUserModel
    {
        public int Id { get; set; }

        public virtual RoleModel Roles { get; set; }

        public virtual BranchModel Branches { get; set; }
    }
}