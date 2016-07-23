using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Git_Analysis.Domain;

namespace Git_Analysis.Utils
{
    public class FileReader : IDisposable
    {
        readonly string file_path;
        FileStream fileStream;
        StreamReader steamReader;
        StringBuilder strBuffer;
        public FileReader(string path)
        {
            file_path = path;
            strBuffer = new StringBuilder();
        }

        public void Open()
        {
            if (!File.Exists(file_path)) throw new FileNotFoundException();
            fileStream = new FileStream(file_path, FileMode.Open);
            steamReader = new StreamReader(fileStream);
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

        public bool HasMoreCommitInfo()
        {
            return strBuffer.ToString().Length > 0 && IsCommitInfo(strBuffer.ToString());
        }

        public List<CommitBlockInfo> GetAllCommits()
        {
            List<CommitBlockInfo> commits = new List<CommitBlockInfo>();
            do
            {
                commits.Add(GetOneCommit());
            } while (HasMoreCommitInfo());
            return commits;
        }

        public CommitBlockInfo GetOneCommit()
        {
            string commitInfo;
            if (strBuffer.Length > 0)
            {
                commitInfo = strBuffer.ToString();
                strBuffer.Clear();
            }
            else
            {
                commitInfo = ReadLine();
            }
            StringBuilder builder = new StringBuilder();
            strBuffer.Append(ReadLine());
            while (!IsCommitInfo(strBuffer.ToString()) && !steamReader.EndOfStream)
            {
                builder.Append(strBuffer+"\n");
                strBuffer.Clear();
                strBuffer.Append(ReadLine());
            }
           return  new CommitBlockInfo
            {
                CommitInfo = commitInfo,
                ParseInfo = builder.ToString()
            };
        }

        static bool IsCommitInfo(string line)
        {
            return line.Length > 0&&
                line.Contains("hash:") &&
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