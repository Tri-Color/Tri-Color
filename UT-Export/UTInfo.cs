using System.Collections.Generic;
using System.Linq;

namespace UTExport
{
    public class UTInfo
    {
        public UTInfo(string fileFullName)
        {
            FileName = Utils.GetFileName(fileFullName);

            WhenList = new List<string>();
            ThenList = new List<string>();
            Children = new List<UTInfo>();
        }

        public string FileName { get; set; }
        public string Description { get; set; }
        public List<string> WhenList { get; set; }
        public List<string> ThenList { get; set; }

        public UTInfo Parent { get; set; }
        public List<UTInfo> Children { get; set; }

        public bool IsEmpty()
        {
            return !WhenList.Any() && !ThenList.Any() && Children.All(c => c.IsEmpty());
        }

        public void ClearEmptyChildren()
        {
            Children.Where(c => c.IsEmpty())
                .ToList()
                .ForEach(i => Children.Remove(i));
        }
    }
}