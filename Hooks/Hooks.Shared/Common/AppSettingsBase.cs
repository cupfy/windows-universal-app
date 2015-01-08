using Windows.Storage;
using Windows.ApplicationModel;
using System;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hooks.Common
{
    public abstract class AppSettingsBase
    {
        protected ApplicationDataContainer localSettings;
        //protected ApplicationDataContainer roamingSettings;

        //protected StorageFolder localFolder;
        //protected StorageFolder roamingFolder;

        protected AppSettingsBase()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                localSettings = ApplicationData.Current.LocalSettings;
                //roamingSettings = ApplicationData.Current.RoamingSettings;

                //localFolder = ApplicationData.Current.LocalFolder;
                //roamingFolder = ApplicationData.Current.RoamingFolder;
            }
        }

        protected bool SetValue<T>(string key, T value)
        {
            try
            {
                string json = JsonConvert.SerializeObject(value);
                localSettings.Values[key] = json;
                //Debug.WriteLine("SetValue of {0} to {1}", key, json);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        protected T GetValueOrDefault<T>(string key, T defaultValue) {
            try
            {
                var value = localSettings.Values[key];
                if (value == null || String.IsNullOrEmpty(value.ToString())) value = defaultValue;
                //Debug.WriteLine("Value of {0} is {1}", key, value.ToString());

                if (typeof(T) != typeof(string)) return JsonConvert.DeserializeObject<T>(value.ToString());
                return (T)value;
            }
            catch (System.Exception)
            {
                return defaultValue;
            }
        }
    }
}