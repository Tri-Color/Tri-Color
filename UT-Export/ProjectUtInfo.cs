using System.Collections.Generic;

namespace UTExport
{
    public class ProjectUtInfo
    {
        public string ProjectName { get; set; }
        public List<UTInfo> ApiTests { get; set; }
        public List<UTInfo> UnitTests { get; set; }
        public List<UTInfo> JavaScriptTests { get; set; }
    }
}