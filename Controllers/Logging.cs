using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recongnize_Text_Console.Controllers
{
    class Logging 
    {
        #region Properties

        public static string Logs { get; set; }

        #endregion

        #region Logging

        /// <summary>
        /// ReportLogsInfo
        /// </summary>
        /// <param name="strMessage">strMessage</param>
        /// <param name="collection">collection</param>
        /// <param name="log">log</param>
        public static void ReportLogsInfo(string strMessage, char[][] collection = null, string log = "")
        {
            if (log == string.Empty)
                log = Logs;
            if (collection != null)
            {
                string result = "\n";
                foreach (var element in collection)
                {
                    result += "\n";
                    foreach (var item in element)
                        result += item;
                }
                strMessage += result;
            }
            using (StreamWriter w = File.AppendText(log))
            {
                WriteToLogs(strMessage, w);
            }
        }

        /// <summary>
        /// WriteToLogs
        /// </summary>
        /// <param name="strMessage">strMessage</param>
        /// <param name="w">TextWriter</param>
        public static void WriteToLogs(string strMessage, TextWriter w)
        {
            w.Write("\r\nLog Info : ");
            w.WriteLine($"  :{strMessage}");
            w.WriteLine("-------------------------------");
        }

        /// <summary>
        /// CreateUniquePath
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>newFullPath</returns>
        public static string CreateUniquePath(string file)
        {
            int count = 1;
            string fileNameOnly = Path.GetFileNameWithoutExtension(file);
            string extension = Path.GetExtension(file);
            string path = Path.GetDirectoryName(file);
            string newFullPath = file;
            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return newFullPath;
        }

        #endregion
    }
}
