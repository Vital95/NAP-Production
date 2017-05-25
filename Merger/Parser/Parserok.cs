using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merger.Data_structures;

namespace Merger.Parser
{
    public class Parserok
    {

        /// <summary>
        /// Gets unicID from cells
        /// </summary>
        /// <param name="unicId"></param>
        /// <returns></returns>
        public int ParseUID(string unicId)
        {
            string[] split = unicId.Split(new Char[] { ' ', '\n' });
            int number = 0;
            int.TryParse(split[0], out number);
            return number;
        }

        /// <summary>
        /// Gets number of " " and "\n" when splited
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int NumberparseUID(string input)
        {
            string[] split = input.Split(new Char[] { ' ', '\n' });
            int realCounter = 0;
            for (int i = 0; i < split.Length; i++)
            {
                if (split[i] == "")
                {
                    continue;
                }
                else
                {
                    realCounter++;
                }
            }

            return realCounter;
        }

        /// <summary>
        /// Merge cells
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="incertCells"></param>
        /// <returns></returns>
        public List<InsertCell> MergeData(List<Cell> cell, List<InsertCell> incertCells)
        {
            int i = 0;
            foreach (InsertCell insertCell in incertCells)
            {
                insertCell.cellWithData = cell[i];
                i++;
            }
            return incertCells;
        }

        /// <summary>
        /// Delete all empty strings in StyleData list
        /// </summary>
        /// <param name="styleData"></param>
        /// <returns></returns>
        public List<StyleData> FiltreMissingStyle(List<StyleData> styleData)
        {
            List<StyleData> missingStyleData = new List<StyleData>();

            bool flag = false;

            foreach (StyleData d in styleData)
            {
                foreach (string s in d.style)
                {
                    Helper help = new Helper();
                    string checkString = help.ClearString(s);
                    
                    if (checkString == "")
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    missingStyleData.Add(d);
                    flag = false;
                }
            }
            return missingStyleData;
        }

    }
}
