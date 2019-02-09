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
        public const string AceOfClubsClub = "aceofclubs";
        public const string AceOfClubsPassword = "redacted";
    }
    class Program
    {
        static void Main(string[] args)
        {            
            UploadDataApi("20190215_1", @"c:\dir\acblFile", "acblFile", @"c:\dir\BwsFile", "BwsFile", @"c:\dir\PbnFile", "pbnFile");
        }

        private static void UploadDataApi(string eventId, string pathToAcbl, string acblName, string pathToBws, string bwsName, string pathToPbn, string pbnName)
        {
            const string url = "https://www.bridgewebs.com/cgi-bin/bwx/api.cgi?club=aceofclubs";

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(acblName), "\"acblname\"");
            content.Add(new StreamContent(GetFileStream(pathToAcbl)), "\"acbldata\"");
            content.Add(new StringContent(bwsName), "\"bwsname\"");
            content.Add(new StreamContent(GetFileStream(pathToBws)), "\"bwsdata\"");
            content.Add(new StringContent(pbnName), "\"dealname\"");
            content.Add(new StreamContent(GetFileStream(pathToPbn)), "\"dealdata\"");
            content.Add(new StringContent(Constants.AceOfClubsClub), "\"club\"");
            content.Add(new StringContent(Constants.AceOfClubsPassword), "\"password\"");
            content.Add(new StringContent(eventId), "\"20190208_1\"");
            content.Add(new StringContent("upload"), "\"type\"");

            var client = new RestClient();
            
            var response = client.PostAsync(url, content).Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }

        private static FileStream GetFileStream(string path)
        {
            return File.Open(path, FileMode.Open);
        }
    }
}
