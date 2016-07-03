using System.Collections.Generic;

namespace UTExport
{
    public class UTInfo
    {
        public UTInfo()
        {
            ThenList = new List<string>();
        }

        public string FileName { get; set; }
        public string Description { get; set; }
        public List<string> ThenList { get; set; }
    }
}