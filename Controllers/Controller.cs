using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recongnize_Text_Console.Controllers
{
    class Controller
    {
        #region C'tor

        /// <summary>
        /// C'tor
        /// </summary>
        public Controller()
        {
        }

        #endregion

        #region Constants

        /// <summary>
        /// REFERENCEFILE
        /// </summary>
        private string REFERENCEFILE = @"C:\Workspac\Recongnize_Text_Console\_Daten\NumberParserExtended.txt";
        private string LOGS = @"C:\temp\Logs.txt";

        #endregion

        #region Field

        /// <summary>
        /// NumberExpandDictionary
        /// </summary>
        public Dictionary<string, char[][]> NumberExpandDictionary = new Dictionary<string, char[][]>()
        {
            { "NumberOne", new char[4][] { new char[] { '\0', '|' }, new char[] { '\0', '|' }, new char[] { '\0', '|' }, new char[] { '\0', '|' } } },
            { "NumberTwo", new char[4][] { new char[] { '-', '-', '-' }, new char[] { '\0', '_', '|' }, new char[] { '|', '\0', '\0' }, new char[] { '-', '-', '-' } } },
            { "NumberThree", new char[4][] { new char[] { '-', '-', '-' }, new char[] { '\0', '/', '\0' }, new char[] { '\0', '\\', '\0' }, new char[] { '-', '-', '\0' } }  },
            { "NumberFour", new char[4][] { new char[] { '|', '\0', '\0', '\0', '|' }, new char[] { '|', '_', '_', '_', '|' }, new char[] { '\0', '\0', '\0', '\0', '|' }, new char[] { '\0', '\0', '\0', '\0', '|' } }  },
            { "NumberFive", new char[4][] { new char[] { '-', '-', '-', '-', '-' }, new char[] { '|', '_', '_', '_', '\0' }, new char[] { '\0', '\0', '\0', '\0', '|' }, new char[] { '_', '_', '_', '_', '|' } }  }
        };

        #endregion

        #region Properties

        /// <summary>
        /// OriginalText
        /// </summary>
        public static char[][] OriginalText { get; set; }
        public static char[][] TempTabPosition { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public int LastLength { get; set; }
        private string Logs { get; set; }
        private static int CountLogNumber { get; set; }
        private static int ValidNumber { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// CreateDeploymentDir
        /// </summary>
        public void CreateDeploymentDir()
        {
            if (Directory.Exists(@"C:\temp\referenz"))
                Directory.Delete(@"C:\temp\", true);
            Directory.CreateDirectory(@"C:\temp\Validate");
            Directory.CreateDirectory(@"C:\temp\Extract");
            Directory.CreateDirectory(@"C:\temp\Referenz");
        }

        /// <summary>
        /// CreateRefernzDir
        /// </summary>
        public void CreateRefernzDir()
        {
            ReportLogsInfo("CreateRefernzDir()");
            foreach (var element in NumberExpandDictionary)
                ReportLogsInfo(element.Key, element.Value, @"C:\temp\Referenz\" + element.Key + ".txt");
        }

        /// <summary>
        /// ReadTXTFile
        /// </summary>
        /// <returns>true if Read otherwise false</returns>
        public bool ReadTXTFile()
        {
            ReportLogsInfo("ReadTXTFile()");
            if (File.Exists(REFERENCEFILE))
            {
                Initializer();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Initializer
        /// </summary>
        public void Initializer()
        {
            var lines = File.ReadAllLines(REFERENCEFILE);
            char[][] line = new char[lines.Length][];
            int max = 0;
            for (int counter = 0; counter < lines.Length; counter++)
            {
                line[counter] = lines[counter].ToCharArray();
                if (line[counter].Length > max)
                    max = line[counter].Length;
                ReportLogsInfo("Linia " + counter + " has length: " + line[counter].Length +"\n", null, @"C:\temp\referenz\Line " + counter +  " .txt");
            }
            OriginalText = TransformText(line, max);
            ColumnIndex = 0;
            RowIndex = 0;
            LastLength = 0;
            CountLogNumber = 0;
            ValidNumber = 1;
            ReportLogsInfo("MyEncoding \n", OriginalText, @"C:\temp\validate\MyEncoding.txt");
        }

        /// <summary>
        /// TransformText
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="max">max</param>
        /// <returns>results</returns>
        public char[][] TransformText(char[][] line, int max)
        {
            char[][] results = new char[line.Length][];
            for(int countLine = 0; countLine < line.Length; countLine++)
            {

                results[countLine] = CharList(line, max, countLine);
            }
            return results;
        }

        /// <summary>
        /// CharList
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="max">max</param>
        /// <param name="countLine">countLine</param>
        /// <returns></returns>
        public char[] CharList(char[][] line, int max, int countLine)
        {
            char[] tmp = new char[max];
            int counterTab = 0;
            for (int count = 0; count < max && counterTab < max; count++)
            {
                if (count < line[countLine].Length)
                    switch (line[countLine][count])
                    {
                        case ' ':
                            tmp[counterTab] = '\0';
                            break;
                        case '\n':
                            tmp[counterTab] = '\0';
                            break;
                        case '\t':
                            {
                                int times = SearchNextChar(line, countLine, count) - SearchLastChar(line, countLine, count) - 1;
                                for (int i = times; i > 0; i--)
                                {
                                    tmp[counterTab] = '\0';
                                    counterTab++;
                                    if (i != 1) count++;
                                }
                                if (countLine == 2) counterTab--;
                                break;
                            }
                        default:
                            if (countLine == 2 && count == 28) tmp[counterTab - 1] = line[countLine][count]; 
                            else tmp[counterTab] = line[countLine][count];
                            break;
                    }
                else
                    tmp[counterTab] = '\0';
                counterTab++;
            }
            return tmp;
        }

        /// <summary>
        /// SearchLastChar
        /// </summary>
        /// <param name="collection">collection</param>
        /// <param name="line">line</param>
        /// <param name="index">index</param>
        /// <returns>count</returns>
        public int SearchLastChar(char[][] collection, int line, int index)
        {
            int count = index;
            while (count > 0)
            {
                if (collection[line][count] == '\t' || collection[line][count] == '\n' || collection[line][count] == '\0' || collection[line][count] == ' ')
                     --count;
                else
                    return count;
            }
            return -1;
        }

        /// <summary>
        /// SearchNextChar
        /// </summary>
        /// <param name="collection">collection</param>
        /// <param name="line">line</param>
        /// <param name="index">index</param>
        /// <returns>count</returns>
        public int SearchNextChar(char[][] collection, int line, int index)
        {
            int count = index;
            while (count < collection[line].Length)
            {
                if (collection[line][count] == '\t' || collection[line][count] == '\n' || collection[line][count] == '\0' || collection[line][count] == ' ')
                    ++count;
                else
                    return count;
            }
            return count;
        }

        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            CreateDeploymentDir();
            if (File.Exists(LOGS))
                Logs = CreateUniquePath(LOGS);
            else
                Logs = LOGS;
            ReportLogsInfo("Start()");
            if (ReadTXTFile())
            {
                CreateRefernzDir();
                for (int col = 0; col < OriginalText[1].Length;)
                {
                    ReportLogsInfo("LOOP IN Start\t  Column:  " + col);
                    ColumnIndex = col;
                    if (SearchInDictionary(col))
                    {
                        col += LastLength;
                        LastLength = 0;
                    }
                    else
                        col++;
                }
            }
        }

        /// <summary>
        /// SearchInDictionary
        /// </summary>
        /// <param name="column">column</param>
        /// <returns>found</returns>
        public bool SearchInDictionary(int column)
        {
            bool found = false;
            foreach (var element in NumberExpandDictionary)
            {
                ReportLogsInfo("SearchInNumberExpand for  " + element.Key +
                    "\n ExtractText(element.Value[0].Length " + element.Value[0].Length);
                var expect = ExtractText(element.Value[0].Length, column, 0, element.Key);
                bool flag = ValidateNumber(expect, element.Key);
                if (flag)
                {
                    string validFile = @"C:\temp\validate\" + element.Key + ".txt";
                    ReportLogsInfo("VALIDATE VALIDATE VALIDATE VALIDATE VALIDATE VALIDATE \t" + ValidNumber + element.Key);
                    ReportLogsInfo("VALIDATE VALIDATE VALIDATE VALIDATE VALIDATE VALIDATE \t" + ValidNumber + element.Key, expect, validFile);
                    LastLength = element.Value.Length - 1;
                    ValidNumber++;
                    found = true;
                    var proc = OpenFiles(validFile);
                    CloseFiles(proc);
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// ExtractText
        /// </summary>
        /// <param name="length">length</param>
        /// <param name="column">column</param>
        /// <param name="row">row</param>
        /// <param name="searchNumber">searchNumber</param>
        /// <returns>results[][]</returns>
        public char[][] ExtractText(int length, int column = 0, int row = 0, string searchNumber = "")
        {
            char[][] results = new char[4][];
            TempTabPosition = new char[4][];
            for (int lines = row; lines < 4 && lines < OriginalText.Length; lines++)
            {
                char[] temp = new char[length];
                int count = 0;
                for (int counter = column; count < temp.Length && counter < OriginalText[lines].Length; counter++)
                { 
                    temp[count] = OriginalText[lines][counter];
                    count++;
                }
                results[lines] = temp;
            }
            ReportLogsInfo("Extract     Length = " + length + " column = " + column + " \n SearchNumber: " + 
                searchNumber + "\n", results, @"C:\temp\extract\" + CountLogNumber + "column" + column + " Length " + length + " " + searchNumber + " " + ".txt");
            CountLogNumber++;
            return results;
        }

        /// <summary>
        /// ValidateNumber
        /// </summary>
        /// <param name="expected">char[][] expected</param>
        /// /// <param name="index">index</param>
        /// <returns>true if number found otherwise false</returns>
        public bool ValidateNumber(char[][] expected, string index)
        {
            var found = false;
            var referenz = NumberExpandDictionary[index];
            if (referenz[0].Length == expected[0].Length)
            {
                for (int lines = 0; lines < expected.Length; lines++)
                {
                    for (int col = 0; col < expected[lines].Length; col++)
                    {
                        if (referenz[lines][col] == expected[lines][col])
                            found = true;
                        else
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found == false)
                        break;
                }
            }
            return found;
        }

        #endregion

        #region OpenFiles

        /// <summary>
        /// OpenFiles
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <returns>fileName</returns>
        public Process OpenFiles(string fileName)
        {
            var process = Process.Start(fileName, "notepad.exe");
            Thread.Sleep(7000);
            return process;
        }

        /// <summary>
        /// CloseFiles
        /// </summary>
        /// <param name="process">process</param>
        public void CloseFiles(Process process)
        {
            process.Kill();
        }

        #endregion

        #region Logging

        /// <summary>
        /// ReportLogsInfo
        /// </summary>
        /// <param name="strMessage">strMessage</param>
        /// <param name="collection">collection</param>
        /// <param name="log">log</param>
        public void ReportLogsInfo(string strMessage, char[][] collection = null, string log = "")
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
        public void WriteToLogs(string strMessage, TextWriter w)
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
        public string CreateUniquePath(string file)
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
