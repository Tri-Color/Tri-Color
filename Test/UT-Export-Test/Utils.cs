using System.IO;
using System.Reflection;

namespace UTExport.Test
{
    public class Utils
    {
        public static string GetFixtureFileFullName(string relativeFixtureFileName)
        {
            var assemblyFileName = Assembly.GetExecutingAssembly().FullName;
            var assemblyfileInfo = new FileInfo(assemblyFileName);
            var testFileName = string.Format("{0}\\..\\..\\{1}", assemblyfileInfo.Directory, relativeFixtureFileName);
            return testFileName;
        }
    }
}