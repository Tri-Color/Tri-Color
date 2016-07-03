using System.Collections.Generic;

namespace UTExport
{
    public class UTInfo
    {
        public UTInfo()
        {
            ThenList = new List<string>();
            Children = new List<UTInfo>();
        }

        public string FileName { get; set; }
        public string Description { get; set; }
        public List<string> ThenList { get; set; }
        public List<UTInfo> Children { get; set; }
        public UTInfo Parent { get; set; }
    }
}