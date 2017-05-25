using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merger.Data_structures;
using System.Windows.Forms;

namespace Merger
{
    public class Helper
    {
        List<string> allowedExt = new List<string>();

        #region Helper Methods

        /// <summary>
        /// Default cnstructor
        /// </summary>
        public Helper()
        {
            allowedExt.Add(".xls");
            allowedExt.Add(".xlsx");
        }

        /// <summary>
        /// Checks if 2 string are same
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        public bool IsMatching(string firstString, string secondString)
        {
            string fileName1 = GetFileName(firstString);
            string fileName2 = GetFileName(secondString);
            return String.Equals(fileName1, fileName2);
        }

        /// <summary>
        /// Create an instance of ExcelObject from View 
        /// </summary>
        /// <param name="pathToFileWithData"></param>
        /// <param name="pathToFileWithStructure"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public ExcelObject CreateInstanceOfExcelObject(string pathToFileWithData,string pathToFileWithStructure, int mode = 0)
        {
            return new ExcelObject(pathToFileWithData,pathToFileWithStructure, mode);
        }

        /// <summary>
        /// Check if extensions are valid
        /// </summary>
        /// <param name="pathToFileWithData"></param>
        /// <param name="pathToFileWithStructure"></param>
        /// <returns></returns>
        public bool HaveGoodExt(string pathToFileWithData, string pathToFileWithStructure)
        {
            bool result = false;
            List<string> data = AddToListData(pathToFileWithData,pathToFileWithStructure);
            foreach (string d in data)
            {
                foreach (string ext in allowedExt)
                {
                    if (d.Contains(ext))
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Convert Array to list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<string> AddToListData(params string[] data)
        {
            List<string> newData = new List<string>();
            foreach (string d in data)
            {
                newData.Add(d);
            }
            return newData;
        }

        /// <summary>
        /// Return extension of a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        /// <summary>
        /// Clear all " "
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ClearString(string input)
        {
            return input.Replace(" ",string.Empty);
        }

        /// <summary>
        /// Return name from filePath
        /// </summary>
        /// <returns></returns>
        public string GetFileName( string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// Convert List of strings to one string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public string ListOfStringsToString(List<string> inputString)
        {
            string outputString = string.Empty;
            foreach (string s in inputString)
            {
                outputString += s;
            }
            return outputString;
        }

        #endregion
    }
}
