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

namespace Shopfy_Mobile.Utilities
{
    public class PreferenceManager
    {
        private ISharedPreferences _sharedPreferences;

        public PreferenceManager(Context context)
        {
            _sharedPreferences = context.GetSharedPreferences(ConstantValues.KEY_PREFERENCE_NAME, FileCreationMode.Private);
        }

        public void PutBoolean(string Key, bool value)
        {
            ISharedPreferencesEditor editor = _sharedPreferences.Edit();
            editor.PutBoolean(Key, value);
            editor.Apply();
        }

        public bool GetBoolean(string key)
        {
            return _sharedPreferences.GetBoolean(key, false);
        }

        public void PutString(string key, string value)
        {
            ISharedPreferencesEditor editor = _sharedPreferences.Edit();
            editor.PutString(key, value);
            editor.Apply();
        }

        public string GetString(string key)
        {
            return _sharedPreferences.GetString(key, null);
        }

        public void Clear()
        {
            ISharedPreferencesEditor editor = _sharedPreferences.Edit();
            editor.Clear();
            editor.Apply();
        }
    }
}