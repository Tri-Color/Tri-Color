using System;
using System.IO;
using System.Text;

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

        public string GetOneCommit()
        {
            string tmp = ReadLine();
            StringBuilder builder = new StringBuilder();
            builder.Append(tmp);
/*            if (tmp != null && IsCommitInfo(tmp))
                return */
            var line = ReadLine();
            while (!IsCommitInfo(line) )
            {
                builder.Append(line);
                line = ReadLine();
            }
            return builder.ToString();
        }

        static bool IsCommitInfo(string line)
        {
            return line.Contains("hash:") &&
                line.Contains("addTime:") &&
                line.Contains("commitTime:") &&
                line.Contains("comment:");
        }

        public long GetCommitCount()
        {
            var count = 0;
            var line = ReadLine();
            while (line != null)
            {
                if (IsCommitInfo(line))
                    count++;
                line = ReadLine();
            }
            return count;
        }

        public void Dispose()
        {
            Close();
        }
    }
}