using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopfy_Mobile.FirebaseDB
{
    public class FirebaseDatabaseHelper
    {
        private static string firebase_client = "https://star-571c6-default-rtdb.asia-southeast1.firebasedatabase.app/";
        private static string firebase_secret = "Mn1zvvOHxQQyono3O5XC2w5krw6R8w8ZaGLePixA";

        public static FirebaseClient DatabaseClient = new FirebaseClient(firebase_client,
                                   new Firebase.Database.FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(firebase_secret) });

    }
}