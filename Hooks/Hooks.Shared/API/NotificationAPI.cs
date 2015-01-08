using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Hooks.Utils;

namespace Hooks.API
{
    public class NotificationAPI : BaseAPI
    {
        const string SET_CHANNEL = "/channel";

        public static NotificationAPI Instance { get { return instance; } }
        private static NotificationAPI instance = new NotificationAPI();

        public static string ChannelUri = AppSettings.Instance.ChannelUri;

        private NotificationAPI() { }

        public async Task<string> UpdateChannel(bool force = false)
        {
            string newChannelUri = ChannelUri;

            try
            {
                newChannelUri = (await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync()).Uri;
                Debug.WriteLine("Channel: {0}", newChannelUri);
            }
            catch (Exception) { }


            //nothing changed -- dont need to send to server (unless its forced)
            if (ChannelUri == newChannelUri && !force) return ChannelUri;

            //var content = new Dictionary<string, string>();
            //content.Add("channel", Channel.Uri);
            //content.Add("pushId", DeviceAPI.DEVICE_ID);

            //HttpResponseMessage responseMessage = await httpClient.PostAsync(new Uri(API_BASE_URI, SET_CHANNEL), new HttpFormUrlEncodedContent(content));
            //string response = await responseMessage.Content.ReadAsStringAsync();

            //if (await HandleErrorAndExit(responseMessage, response)) return null;

            AppSettings.Instance.ChannelUri = newChannelUri;
            return newChannelUri;
        }
    }
}
