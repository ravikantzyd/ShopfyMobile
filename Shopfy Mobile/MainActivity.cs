using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Firebase.Messaging;
using Google.Android.Material.Button;
using Shopfy_Mobile.API_Common_Methods;
using Shopfy_Mobile.Common_Activity;
using Shopfy_Mobile.FirebaseDB;
using Shopfy_Mobile.Main_Nav;
using Shopfy_Mobile.Product.SalePrice;
using Shopfy_Mobile.User;
using Shopfy_Mobile.Utilities;
using System;
using System.Threading;

namespace Shopfy_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false,NoHistory =true)]
    public class MainActivity : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        MaterialButton Eng_5Star_Go_btn;
        MaterialButton Mm_5Star_Go_btn;
        ProgressBar Eng_5Star_Go_progress;
        ProgressBar Mm_5Star_Go_progress;
        TextView Sign_Out_tv;

        PreferenceManager preferenceManager;

        internal static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            try
            {
                var observable = FirebaseDatabaseHelper.DatabaseClient
                  .Child("Address")
                  .AsObservable<string>()
                  .Subscribe(d => InitializeConnection(d.Object));
            }
            catch (Exception)
            {
                Intent intent = new Intent(this, typeof(NO_CONNECTION_ACTIVITY));
                StartActivity(intent);
            }
            
            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            Eng_5Star_Go_btn = FindViewById<MaterialButton>(Resource.Id.Eng_5Star_Go_btn);
            Eng_5Star_Go_progress = FindViewById<ProgressBar>(Resource.Id.Eng_5Star_Go_progress);
            Mm_5Star_Go_btn = FindViewById<MaterialButton>(Resource.Id.MM_5Star_Go_btn);
            Mm_5Star_Go_progress = FindViewById<ProgressBar>(Resource.Id.MM_5Star_Go_progress);
            Sign_Out_tv = FindViewById<TextView>(Resource.Id.sign_out_tv);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Shopfy";

            Eng_5Star_Go_btn.Click += Eng_5Star_Go_btn_Click;
            Mm_5Star_Go_btn.Click += Mm_5Star_Go_btn_Click;
            Sign_Out_tv.Click += Sign_Out_tv_Click;

            preferenceManager = new PreferenceManager(this);

            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.menuaction);
            actionBar.SetDisplayHomeAsUpEnabled(true);
        }

        private void InitializeConnection(string address)
        {
            WebApiAddress.Address = address;
            ApiHelper.InitializeClient();

            if (IsPlayServicesAvailable())
            {
                CreateNotificationChannel();
                
                FirebaseMessaging.Instance.SubscribeToTopic(preferenceManager.GetString(ConstantValues.KEY_BRANCH_NAME).Split(" ")[0]);
                ApiHelper.InitializeFirebaseClient();                
            }
            else
            {
                ShowToastMessage("Google Play Services is available.");
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    ShowToastMessage(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    ShowToastMessage("This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {                
                return true;
            }
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private void Sign_Out_tv_Click(object sender, System.EventArgs e)
        {
            preferenceManager.Clear();

            Intent intent = new Intent(this, typeof(SIGN_IN_ACTIVITY));
            StartActivity(intent);
            Finish();

            ShowToastMessage("Logout");
        }

        private void Mm_5Star_Go_btn_Click(object sender, System.EventArgs e)
        {
            BrandSelection.brand_id = 2;

            Intent intent = new Intent(this, typeof(PRODUCT_SALE_PRICE_REPORT_ACTIVITY));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(intent);
        }

        private void Eng_5Star_Go_btn_Click(object sender, System.EventArgs e)
        {
            BrandSelection.brand_id = 1;

            Intent intent = new Intent(this, typeof(PRODUCT_SALE_PRICE_REPORT_ACTIVITY));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(intent);
        }

        private void ShowToastMessage(string message)
        {
            new System.Threading.Thread(new ThreadStart(() =>
            {
                RunOnUiThread(() => { 
                    Toast.MakeText(this, message, ToastLength.Long).Show(); 
                });
            })).Start();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //Intent intent = new Intent(this, typeof(MAIN_NAV_ACTIVITY));
                    //StartActivity(intent);
                    //Finish();

                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
