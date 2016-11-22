using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Connectivity
{
    public static class Connection
    {
        public static async Task<bool> IsOnline() => await IsValidUrl(@"http://google.com");

        public static async Task<bool> IsValidUrl(string providedUrl)
        {
            HttpWebRequest httpRequest = null;
            HttpWebResponse resp = null;
            Uri url = null;

            try
            {
                url = new Uri(providedUrl);
                httpRequest = WebRequest.Create(url) as HttpWebRequest;

                resp = await httpRequest.GetResponseAsync() as HttpWebResponse;
                httpRequest.Abort();
                httpRequest = null;

                return true;
            }
            catch
            {
                if (httpRequest != null)
                {
                    httpRequest.Abort();
                    httpRequest = null;
                }
            }
            finally
            {
                if (resp != null)
                {
                }
                url = null;
                resp = null;
            }

            return false;
        }

        public static bool IsValidEmailAddress(string email)
        {
            const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (email != null)
            {
                return Regex.IsMatch(email, MatchEmailPattern);
            }
            else
            {
                return false;
            }
        }
    }
}