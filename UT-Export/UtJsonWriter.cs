using System.IO;
using Newtonsoft.Json;

namespace UTExport
{
    public class UtJsonWriter
    {
        public static void WriteToJson<T>(T @object, string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            using (var streamWriter = new StreamWriter(fileName))
            {
                string jsonString = JsonConvert.SerializeObject(@object, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                streamWriter.Write(jsonString);
                streamWriter.Flush();
            }
        }
    }
}