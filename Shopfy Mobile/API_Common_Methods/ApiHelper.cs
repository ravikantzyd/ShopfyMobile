using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Shopfy_Mobile.API_Common_Methods
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static HttpClient FirebaseClientApi { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri($"http://{WebApiAddress.Address}/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void InitializeFirebaseClient()
        {
            FirebaseClientApi = new HttpClient
            {
                BaseAddress = new Uri("https://fcm.googleapis.com/fcm/")
            };

            FirebaseClientApi.DefaultRequestHeaders.Accept.Clear();
            FirebaseClientApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            FirebaseClientApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                "AAAADda-tQM:APA91bE4QCXvrViVlvnmM56cr_C8OlYJLGSfMXE2nM2J6TcnhztgdgVRMoxHPNv7uHe-jj-pMPmqe9lA0p9quX0wm6vSxWKf0lXZTLlEj7S3QJJObYDm4ZOv87wa6oiC0KEaRTeR6srm");
        }
    }
}