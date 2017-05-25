using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merger.Data_structures;

namespace Merger
{
    public class Validator
    {
        /// <summary>
        /// Get pounds and count of pounds in string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="poundData"></param>
        /// <returns></returns>
        public int ValidatePoundNumberInString(string input, out List<string> poundData)
        {
            string[] split = input.Split(new Char[] { ' ', '\n' });

            int counter = 0;

            poundData = new List<string>();

            foreach (string s in split)
            {
                if (s.Contains("£"))
                {
                    poundData.Add(s);
                    counter++;
                }
            }

            return counter;
        }

        /// <summary>
        /// check if string contains pound sign
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool HavePoundSign(string input)
        {
            if (input.Contains("£"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// if have no errors wile validating then return false
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ValidatePaundsList(List<string> input)
        {
            bool hasError = false;

            foreach (string s in input)
            {
                if (s == null)
                {
                    hasError = true;
                }
                if (!HavePoundSign(s))
                {
                    hasError = true;
                }
            }

            return hasError;
        }

        /// <summary>
        /// Validate both Lists for coincidence
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="insertCells"></param>
        /// <returns></returns>
        public bool ValidateUnmergedCells(List<Cell> cells, List<InsertCell> insertCells)
        {
            bool flag = true;
            int cellCounter = 0;
            foreach (InsertCell cell in insertCells)
            {
                if (!(cell.cellWithData.unicId == cells[cellCounter].unicId))
                {
                    flag = false;
                }

                cellCounter++;
            }
            return flag;
        }

        /// <summary>
        /// If true then good
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ValidateStyle(List<StyleData> data)
        {
            bool flag = true;
            foreach (StyleData d in data)
            {
                foreach (string s in d.style)
                {
                    Helper help = new Helper();
                    string checkString = help.ClearString(s);
                    if (checkString == "")
                    {
                        flag = false;
                    }
                }

            }
            return flag;
        }
    }
}
