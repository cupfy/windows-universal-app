using Hooks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace Hooks.API
{
    public class DeviceAPI : BaseAPI
    {
        public static string DEVICE_ID;
        public static string DEVICE_MODEL = (new EasClientDeviceInformation()).FriendlyName;
        const string DEVICE_INTERNAL_TYPE_CODE = "2"; //Windows Phone

        const string DEVICE_REGISTER    = "/device/";
        const string DEVICE_PUSH        = "/device/push";

        public static DeviceAPI Instance { get { return instance; } }
        private static DeviceAPI instance = new DeviceAPI();

        public static bool? IsRegistered { get; internal set; }

        private DeviceAPI() {
            var deviceID = HardwareIdentification.GetPackageSpecificToken(null).Id;
            var dataReader = DataReader.FromBuffer(deviceID);

            byte[] bytes = new byte[deviceID.Length];
            dataReader.ReadBytes(bytes);

            DEVICE_ID = BitConverter.ToString(bytes);
            Debug.WriteLine("Device ID: " + DEVICE_ID);
        }

        public async Task<bool> Register(string name)
        {
            var content = new Dictionary<string, string>();
            content.Add("name", name);
            content.Add("model", DEVICE_MODEL);
            content.Add("type", DEVICE_INTERNAL_TYPE_CODE);
            content.Add("pushId", DEVICE_ID);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(new Uri(baseUri, DEVICE_REGISTER), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            return !(await HandleErrorAndExit(responseMessage, response));
        }
    }
}
