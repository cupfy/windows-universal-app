using Hooks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.Web.Http;

namespace Hooks.API
{
    public class HookAPI : BaseAPI
    {
        const string HOOK_CREATE = "/device/hook";
        const string HOOK_LIST_FORMAT = "/device/?pushId={0}";

        public static HookAPI Instance { get { return instance; } }
        private static HookAPI instance = new HookAPI();

        private HookAPI() {}

        public async Task<HookList> GetHooks()
        {
            string url = String.Format(HOOK_LIST_FORMAT, DeviceAPI.DEVICE_ID);

            HttpResponseMessage responseMessage = await httpClient.GetAsync(new Uri(API_BASE_URI, url));
            string response = await responseMessage.Content.ReadAsStringAsync();

            DeviceAPI.IsRegistered = false;
            if (await HandleErrorAndExit(responseMessage, response)) return null;

            DeviceAPI.IsRegistered = true;
            return JsonConvert.DeserializeObject<HookList>(response);
        }

        public async Task<Hook> Subscribe(string _namespace)
        {
            var content = new Dictionary<string, string>();
            content.Add("namespace", _namespace);
            content.Add("pushId", DeviceAPI.DEVICE_ID);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(new Uri(API_BASE_URI, HOOK_CREATE), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (await HandleErrorAndExit(responseMessage, response)) return null;

            return JsonConvert.DeserializeObject<Hook>(response);
        }
    }
}
