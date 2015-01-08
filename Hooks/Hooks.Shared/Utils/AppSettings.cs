using Hooks.Common;

namespace Hooks.Utils
{
    public class AppSettings : AppSettingsBase
    {
        public static readonly AppSettings Instance = new AppSettings();

        private const string CHANNEL_KEY = "CHANNEL";
        private string CHANNEL_DEFAULT = null;

        private AppSettings() : base() { }

        public string ChannelUri
        {
            get { return GetValueOrDefault<string>(CHANNEL_KEY, CHANNEL_DEFAULT); }
            set { SetValue<string>(CHANNEL_KEY, value); }
        }
    }
}
