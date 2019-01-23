using System;
using System.Collections.Generic;
using System.Net.Http;
using Easy.Common;

namespace BridgeWebsUpload
{
    public class Constants 
    {
        public const string BridgeWebUrl = "https://www.bridgewebs.com/cgi-bin/bwx/api.cgi?club=";
        public const string AceOfClubsClub = "aceofclubs";
        public const string AceOfClubsPassword = "*****";
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var content = new FormUrlEncodedContent(new Dictionary<string,string>{
                {"club", Constants.AceOfClubsClub},
                {"password", Constants.AceOfClubsPassword},
                {"type", "upload"},
                {"filename", "xxxxxxxx.csv"},
                {"data", "some,data\nmore,data"}
            });
            var client = new RestClient();
            var response = client.PostAsync(Constants.BridgeWebUrl + Constants.AceOfClubsClub, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.Read();
        }
    }
}
