using System;
using System.IO;

namespace Git_Analysis.Utils
{
    public class FileReader : IDisposable
    {
        string file_path;
        FileStream fileStream;
        StreamReader steamReader;
        public FileReader(string path)
        {
            this.file_path = path;

        }

        public void Open()
        {
            if (File.Exists(file_path))
            {
                fileStream = new FileStream(file_path, FileMode.Open);
                steamReader = new StreamReader(fileStream);
                return;
            }
            throw new FileNotFoundException();

        }

        public void Close()
        {
            if (steamReader != null)
            {
                steamReader.Close();
            }
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }

        public string ReadLine()
        {
            if (fileStream != null && steamReader != null)
            {
                return steamReader.ReadLine();
            }
            return null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}