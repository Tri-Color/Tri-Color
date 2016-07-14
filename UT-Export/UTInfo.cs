using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace UTExport
{
    public class UTInfo
    {
        public UTInfo()
        {
            WhenList = new List<string>();
            ThenList = new List<string>();
            Children = new List<UTInfo>();
        }

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

        [JsonIgnore]
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

        public bool Contains(string searchKeyword)
        {
            string lowerSearchKeyword = searchKeyword.ToLower();
            return Description.ToLower().Contains(lowerSearchKeyword)
                   || WhenList.Any(when => when.ToLower().Contains(lowerSearchKeyword))
                   || ThenList.Any(then => then.ToLower().Contains(lowerSearchKeyword))
                   || Children.Any(child => child.Contains(lowerSearchKeyword));
        }
    }
}