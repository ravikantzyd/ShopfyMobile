using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Button;
using Shopfy_Mobile.API_Common_Methods;
using Shopfy_Mobile.API_Processor;
using Shopfy_Mobile.FirebaseDB;
using Shopfy_Mobile.Models;
using Shopfy_Mobile.Product.SalePrice;
using Shopfy_Mobile.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.User
{
    [Activity(Label = "5 Star", MainLauncher = true)]
    public class SIGN_IN_ACTIVITY : Activity
    {
        private EditText inputUserId;
        private EditText inputPassword;
        private MaterialButton signInBtn;        
        private ProgressBar progressbar;
        
        PreferenceManager preferenceManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            preferenceManager = new PreferenceManager(this);

            try
            {
                var observable = FirebaseDatabaseHelper.DatabaseClient
                  .Child("Address")
                  .AsObservable<string>()
                  .Subscribe(d => InitializeConnection(d.Object));
            }
            catch (Exception)
            {
                ShowToastMessage("No Internet Access!");
                this.Finish();
            }
            
            if (preferenceManager.GetBoolean(ConstantValues.KEY_IS_SIGNED_IN))
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            }

            SetContentView(Resource.Layout.sign_in_activity);            

            inputUserId = FindViewById<EditText>(Resource.Id.sign_in_input_user_id);
            inputPassword = FindViewById<EditText>(Resource.Id.sign_in_input_password);
            signInBtn = FindViewById<MaterialButton>(Resource.Id.sign_in_signin_btn);
            progressbar = FindViewById<ProgressBar>(Resource.Id.sign_in_progress_bar);

            signInBtn.Click += SignInBtn_Click;
        }

        private void InitializeConnection(string address)
        {
            WebApiAddress.Address = address;
            ApiHelper.InitializeClient();
        }

        private void SignInBtn_Click(object sender, EventArgs e)
        {
            SignIn();
        }        

        private async void SignIn()
        {
            if (IsValidSignInDetails())
            {
                IsLoading(true);

                string user_name_id = inputUserId.Text.Trim();
                string password = inputPassword.Text.Trim();

                UserModel user = new UserModel();

                user = await UserProcessor.LoadUserByName(user_name_id);

                if (user == null)
                {
                    ShowToastMessage("Please enter correct User Id!");
                    IsLoading(false);
                }
                else
                {
                    if (user.Password != password)
                    {
                        ShowToastMessage("Please enter correct Password!");

                        IsLoading(false);
                        return;
                    }
                    else
                    {
                        RoleModel role = await RoleProcessor.LoadRole(user.RoleId);
                        BranchModel branch = await BranchProcessor.LoadBranch(user.BranchId);                       
                        
                        preferenceManager.PutBoolean(ConstantValues.KEY_IS_SIGNED_IN, true);
                        preferenceManager.PutString(ConstantValues.KEY_USER_ID, user.Id.ToString());
                        preferenceManager.PutString(ConstantValues.KEY_USER_NAME_ID, user.UserNameId);
                        preferenceManager.PutString(ConstantValues.KEY_ROLE_NAME, role.RoleName);
                        preferenceManager.PutString(ConstantValues.KEY_BRANCH_NAME, branch.BranchName);

                        Intent intent = new Intent(this, typeof(MainActivity));
                        intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                        StartActivity(intent);

                        IsLoading(false);
                        ShowToastMessage("Login successful");
                    }
                }                
            }
        }

        private void ShowToastMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        private bool IsValidSignInDetails()
        {
            if (string.IsNullOrEmpty(inputUserId.Text.Trim()))
            {
                ShowToastMessage("Enter email");
                return false;
            }            
            else if (string.IsNullOrEmpty(inputPassword.Text.Trim()))
            {
                ShowToastMessage("Enter password");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void IsLoading(Boolean loading)
        {
            if (loading)
            {
                signInBtn.Visibility = ViewStates.Invisible;
                progressbar.Visibility = ViewStates.Visible;
            }
            else
            {
                signInBtn.Visibility = ViewStates.Visible;
                progressbar.Visibility = ViewStates.Invisible;
            }
        }
    }
}