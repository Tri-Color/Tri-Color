using System.Collections.Generic;

namespace UTExport
{
    public interface IUTManager
    {
        List<UTInfo> Export(string fileFullName);
    }
}