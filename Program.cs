using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Easy.Common;

namespace BridgeWebsUpload
{
    public class Constants 
    {
        public const string BridgeWebUrl = "https://www.bridgewebs.com/cgi-bin/bwx/api.cgi?club=";
        public const string AceOfClubsClub = "aceofclubs";
        public const string AceOfClubsPassword = "TheAce07!";
    }
    class Program
    {
        static void Main(string[] args)
        {
            var @event = "20191001_1";
            UploadData(@event, "deal", "190102M.pbn", @"d:\git\BridgeWebsUpload\190102M.pbn");           
            //UploadData(@event, "deal", "190102M.pbn", @"d:\git\BridgeWebsUpload\190102M.pbn");
            //UploadData(@event, "deal", "190102M.pbn", @"d:\git\BridgeWebsUpload\190102M.pbn");
        }

        private static void UploadData(string @event, string type, string filename, string dealdata)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string,string> {
                {"club", Constants.AceOfClubsClub},
                {"password", Constants.AceOfClubsPassword},
                {"type", "upload"},
                //{"transfer", "deal" },
                {"event_id", "20191001_1"},
                {"dealname", "190102M.pbn"},
                {"dealdata", GetFileData(@"d:\git\BridgeWebsUpload\190102M.pbn")},
                //{"bwsname", "190102M.BWS"},
                //{"bwsdata", GetFileData(@"d:\git\BridgeWebsUpload\190102M.BWS")},
                {"acblname", "190102.ACM"},
                {"acbldata", GetFileData(@"d:\git\BridgeWebsUpload\190102.ACM")},
            });
                
            var client = new RestClient();
            var response = client.PostAsync(Constants.BridgeWebUrl + Constants.AceOfClubsClub, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
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
    }
}
