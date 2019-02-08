using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Easy.Common;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace BridgeWebsUpload
{
    public class Constants 
    {
        public const string BridgeWebUrl = "https://www.bridgewebs.com/cgi-bin/bwom/bw.cgi?club=aceofclubs&pid=upload_results";
        public const string AceOfClubsClub = "aceofclubs";
        public const string AceOfClubsPassword = "TheAce07!";
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cookieVal = Login();
            UploadData(cookieVal);
        }

        private static void UploadData(string cookieVal)
        {
            const string url = "https://www.bridgewebs.com/cgi-bin/bwom/bw.cgi?club=aceofclubs&pid=upload_results";

            
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(GetFileStream(@"d:\git\BridgeWebsUpload\190102.ACM")), "\"bridge_results\"", "190102.ACM");
            content.Add(new StreamContent(GetFileStream(@"d:\git\BridgeWebsUpload\190102M.BWS")), "\"bridge_bws\"", "190102M.BWS");
            content.Add(new StreamContent(GetFileStream(@"d:\git\BridgeWebsUpload\190102M.pbn")), "\"bridge_hands\"", "190102M.pbn");
            content.Add(new StringContent("upload"), "\"bridge_button\"");
            content.Add(new StringContent(Constants.AceOfClubsClub), "\"hidden_club\"");
            content.Add(new StringContent("upload_results"), "\"hidden_pid\"");
            content.Add(new StringContent("1"), "\"hidden_wd\"");
            content.Add(new StringContent("1"), "\"menu_present\"");
            content.Add(new StringContent("upload"), "\"option_last\"");
            content.Add(new StringContent("upload"), "\"page_last\"");
            content.Add(new StringContent("upload"), "\"popt\"");
            content.Add(new StringContent(cookieVal.Split('&')[1]), "\"sessid\"");
            content.Headers.Add("Cookie", cookieVal);
            
            var client = new RestClient();
            
            var response = client.PostAsync(url, content).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }

        private static string Login()
        {
            const string url = "https://www.bridgewebs.com/cgi-bin/bwom/bw.cgi?club=aceofclubs&pid=upload_results";
            
            using (var client = new RestClient())
            {
                var loginPageResponse = client.GetAsync(url).Result;
                var cookieVal = string.Join("", loginPageResponse.Headers.GetValues("Set-Cookie").First().Split(" ")[0].TrimEnd(';', ' '));
                var loginPayload = new FormUrlEncodedContent(new Dictionary<string,string> {
                    {"bridge_password", Constants.AceOfClubsPassword},
                    {"popt", "login"},
                    {"hidden_club", Constants.AceOfClubsClub},
                    {"sessid",  cookieVal.Split('&')[1]},
                    {"hidden_wd", "1"},
                    {"hidden_pid", "upload_results"},
                    {"menu_present", "1"}
                });
                
                try
                {
                    loginPayload.Headers.Add("Cookie", "cbwsec=" + cookieVal);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                var result = client.PostAsync(url, loginPayload).Result;
                cookieVal = string.Join("", result.Headers.GetValues("Set-Cookie").First().Split(" ")[0].TrimEnd(';', ' '));
                return cookieVal;
            }
        }

        private static string GetFileData(string path) 
        {
            using (var fs = File.Open(path, FileMode.Open))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                var urlString = HttpUtility.UrlEncode(ms.ToArray());
                return urlString;
            }
        }

        private static FileStream GetFileStream(string path)
        {
            return File.Open(path, FileMode.Open);
        }
    }
}
