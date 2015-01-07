using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace Hooks.API
{
    public abstract class BaseAPI
    {
        protected HttpClient httpClient = new HttpClient();
        protected Uri baseUri = new Uri("http://apphooks.mauriciogior.com/", UriKind.Absolute);

        protected async Task<bool> HandleErrorAndExit(HttpResponseMessage responseMessage, string response)
        {
            Debug.WriteLine("Response: " + response);

            if (!responseMessage.IsSuccessStatusCode)
            {
                if (!String.IsNullOrEmpty(responseMessage.ReasonPhrase))
                    await new MessageDialog(responseMessage.ReasonPhrase, "Oops!").ShowAsync();

                return true;
            }

            return String.IsNullOrEmpty(response);
        }
    }

    [DataContract]
    class ErrorResponse
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
