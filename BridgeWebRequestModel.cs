using System.Collections.Generic;

namespace BridgeWebsUpload {
    public class BridgeWebRequestModel 
    {
        public string Club;
        public string Password;
        public string Type;
        public string Filename;
        public string EventId;
        public string Data;
        public string DealName;
        public string DealDataFile;
        public IList<string> DealNames;
        public IList<string> DealDatas;
        public string InfoName;
        public string InfoData;
        public string ACBLName;
        public string ACBLData;
        public string BWSName;
        public string BWSData;
    }
}