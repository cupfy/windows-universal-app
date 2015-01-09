using Hooks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.Web.Http;

namespace Hooks.API
{
    public class DeviceAPI : BaseAPI
    {
        public static string DEVICE_ID;
        public static string DEVICE_ID_HASH;
        public static string DEVICE_MODEL = (new EasClientDeviceInformation()).FriendlyName;
        const int DEVICE_INTERNAL_TYPE_CODE = (int)Device.DeviceOSType.WindowsPhone;

        const string DEVICE_REGISTER = "/device";
        const string DEVICE_UPDATE_FORMAT = "/device/{0}";

        const string DEVICE_INFO_AND_HOOKS_FORMAT = "/device/{0}?nocache={1}";
        const string DEVICE_HOOK_FORMAT = "/device/{0}/hook?nocache={1}";

        public static DeviceAPI Instance { get { return instance; } }
        private static DeviceAPI instance = new DeviceAPI();

        private DeviceAPI() {
            var deviceID = HardwareIdentification.GetPackageSpecificToken(null).Id;
            var dataReader = DataReader.FromBuffer(deviceID);

            byte[] bytes = new byte[deviceID.Length];
            dataReader.ReadBytes(bytes);

            DEVICE_ID = BitConverter.ToString(bytes);
            DEVICE_ID_HASH = CryptographicBuffer.EncodeToHexString(HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256).HashData(CryptographicBuffer.ConvertStringToBinary(DEVICE_ID, BinaryStringEncoding.Utf8)));

            Debug.WriteLine("Device ID: " + DEVICE_ID);
            Debug.WriteLine("Device ID Hash: " + DEVICE_ID_HASH);
        }


        public async Task<DeviceInfoAndHooksResponse> GetDeviceInfoAndHooks(bool ignoreCache = false)
        {
            string url = String.Format(DEVICE_INFO_AND_HOOKS_FORMAT, DeviceAPI.DEVICE_ID_HASH, ignoreCache ? Guid.NewGuid().ToString() : "");

            HttpResponseMessage responseMessage = await httpClient.GetAsync(new Uri(API_BASE_URI, url));
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (await HandleErrorAndExit(responseMessage, response)) return null;

            return JsonConvert.DeserializeObject<DeviceInfoAndHooksResponse>(response);
        }

        public async Task<Device> Register(string name, string channelUri = null)
        {
            var content = new Dictionary<string, string>();
            content.Add("name", name);
            content.Add("model", DEVICE_MODEL);
            content.Add("type", DEVICE_INTERNAL_TYPE_CODE.ToString());
            content.Add("deviceId", DEVICE_ID_HASH);
            content.Add("pushId", channelUri);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(new Uri(API_BASE_URI, DEVICE_REGISTER), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (await HandleErrorAndExit(responseMessage, response)) return null;
            return JsonConvert.DeserializeObject<Device>(response);
        }

        public async Task<Device> UpdateInfo(string name = null)
        {
            string url = String.Format(DEVICE_UPDATE_FORMAT, DeviceAPI.DEVICE_ID_HASH);

            var content = new Dictionary<string, string>();
            content.Add("name", name);

            HttpResponseMessage responseMessage = await httpClient.PutAsync(new Uri(API_BASE_URI, url), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (await HandleErrorAndExit(responseMessage, response)) return null;
            return JsonConvert.DeserializeObject<Device>(response);
        }

        public async Task<string> UpdateChannel(string oldChannelUri = null, bool force = false)
        {
            string url = String.Format(DEVICE_UPDATE_FORMAT, DeviceAPI.DEVICE_ID_HASH);
            string newChannelUri = oldChannelUri;

            try
            {
                newChannelUri = (await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync()).Uri;
                Debug.WriteLine("Channel: {0}", newChannelUri);
            }
            catch (Exception) { }

            //nothing changed -- dont need to send to server (unless its forced)
            if (newChannelUri == oldChannelUri && !force) return oldChannelUri;

            var content = new Dictionary<string, string>();
            content.Add("pushId", newChannelUri);

            HttpResponseMessage responseMessage = await httpClient.PutAsync(new Uri(API_BASE_URI, url), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            await HandleErrorAndExit(responseMessage, response);
            return newChannelUri;
        }

        public async Task<DeviceInfoAndHooksResponse> Hook(string _namespace)
        {
            string url = String.Format(DEVICE_HOOK_FORMAT, DeviceAPI.DEVICE_ID_HASH, Guid.NewGuid().ToString());

            var content = new Dictionary<string, string>();
            content.Add("namespace", _namespace);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(new Uri(API_BASE_URI, url), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (await HandleErrorAndExit(responseMessage, response)) return null;

            return JsonConvert.DeserializeObject<DeviceInfoAndHooksResponse>(response);
        }
    }

    [DataContract]
    public class DeviceInfoAndHooksResponse
    {
        [DataMember(Name = "device")]
        public Device Device { get; set; }

        [DataMember(Name = "hooks")]
        public HookList Hooks { get; set; }
    }
}
