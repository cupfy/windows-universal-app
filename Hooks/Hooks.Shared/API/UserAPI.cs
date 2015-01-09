using Hooks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Hooks.API
{
    public class UserAPI : BaseAPI
    {
        const string USER_LOGIN = "/user/login";

        public static UserAPI Instance { get { return instance; } }
        private static UserAPI instance = new UserAPI();

        private UserAPI() { }

        public async Task<User> Login(string email, string password)
        {
            var content = new Dictionary<string, string>();
            content.Add("email", email);
            content.Add("password", password);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(new Uri(API_BASE_URI, USER_LOGIN), new HttpFormUrlEncodedContent(content));
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (await HandleErrorAndExit(responseMessage, response)) return null;

            return JsonConvert.DeserializeObject<User>(response);
        }
    }
}
