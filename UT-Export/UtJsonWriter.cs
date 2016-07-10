using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace UTExport
{
    public class UtJsonWriter
    {
        public static void WriteToJson(List<UTInfo> utInfos, string fileName)
        {
            using (var streamWriter = new StreamWriter(fileName))
            {
                string jsonString = JsonConvert.SerializeObject(utInfos, Formatting.Indented, new JsonSerializerSettings
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