using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using Merger.Data_structures;
using Merger.Parser;

namespace Merger.Excel1
{
    public class Excel
    {
        #region private fields

        private int rowCount;
        private int colCount;
        private int errorCounter = 0;
        private Worksheet xlWorksheet;

        #endregion

        #region Get and Set

        /// <summary>
        /// Set worker sheet
        /// </summary>
        /// <param name="xlWorksheet"></param>
        private void SetWorkSheet(Worksheet xlWorksheet)
        {
            this.xlWorksheet = xlWorksheet;
        }

        /// <summary>
        /// Set row and column count for current sheet
        /// </summary>
        /// <param name="xlRange"></param>
        private void SetRowAndColumnCount(Range xlRange)
        {
            rowCount = xlRange.Rows.Count;
            colCount = xlRange.Columns.Count;
        }

        /// <summary>
        /// Gets error count
        /// </summary>
        /// <returns></returns>
        public int GetErrorCount()
        {
            return errorCounter;
        }

        #endregion

        #region Main Logic

        /// <summary>
        /// Get X position of cell with needed string
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetXAxisOfNeededCell(Worksheet xlWorksheet, string input)
        {
            SetWorkSheet(xlWorksheet);

            Range xlRange = xlWorksheet.UsedRange;

            SetRowAndColumnCount(xlRange);

            int result = 0;
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    try
                    {
                        string main = (xlWorksheet.Cells[i, j] as Range).Text as string;
                        Helper help = new Helper();

                        main = help.ClearString(main);

                        if (main == input)
                        {
                            result = j;
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        return result;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all cells with unicID inside
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <returns></returns>
        public List<Cell> GetAllDataWithUnicID(Worksheet xlWorksheet)
        {
            SetWorkSheet(xlWorksheet);

            Range xlRange = xlWorksheet.UsedRange;

            SetRowAndColumnCount(xlRange);

            List<Cell> cells = new List<Cell>();

            for (int i = 1; i < rowCount; i++)
            {
                try
                {

                    string main = (xlWorksheet.Cells[i, 1] as Range).Text as string;
                    Parserok parser = new Parserok(); 
                    
                    int parsed = parser.ParseUID(main);

                    ///parsed >= 3 becourse of uniq code length
                    if (parsed != 0 && parsed.ToString().Length >=3)
                    {
                        Cell cell = new Cell();

                        if (parser.NumberparseUID(main) >= 3)
                        {

                            cell = GetHardCodedDataWay2(i);
                        }
                        else
                        {
                            cell = GetHardCodedDataWay1(i);
                        }
                        cell.unicId = parsed.ToString();

                        cells.Add(cell);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    errorCounter++;
                    string message = ex.Message;
                }
            }

            return cells;
        }

        /// <summary>
        /// working on this
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <returns></returns>
        public List<StyleData> GetAllStylesFromGoodExcel(Worksheet xlWorksheet)
        {

            SetWorkSheet(xlWorksheet);

            Range xlRange = xlWorksheet.UsedRange;

            SetRowAndColumnCount(xlRange);

            List<StyleData> cells = new List<StyleData>();

            for (int i = 1; i < rowCount; i++)
            {
                try
                {

                    string main = (xlWorksheet.Cells[i, 1] as Range).Text as string;
                    Parserok parser = new Parserok();
                    int parsed = parser.ParseUID(main);

                    if (parsed != 0)
                    {

                        StyleData data = new StyleData();

                        data.insertPosition.Y = i;
                        data.insertPosition.X = GetXAxisOfNeededCell(xlWorksheet, "StyleNumber");

                        for (int m = 0; m < 3; m++)
                        {
                            string style = (xlWorksheet.Cells[i + m, data.insertPosition.X] as Range).Text as string;
                            data.style.Add(style);
                        }

                        data.unicID = parsed.ToString();

                        cells.Add(data);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }

            return cells;
        }

        /// <summary>
        /// Gets Style from file with bad structure
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <param name="data"></param>
        /// <param name="canFill"></param>
        /// <returns></returns>
        public List<StyleData> GetGoodStylesFromBadExcel(Worksheet xlWorksheet, List<StyleData> data, out bool canFill)
        {
            SetWorkSheet(xlWorksheet);

            Range xlRange = xlWorksheet.UsedRange;

            canFill = true;

            SetRowAndColumnCount(xlRange);

            List<StyleData> cells = new List<StyleData>();
            Helper help = new Helper();
            foreach (StyleData emptyStyleData in data)
            {
                for (int i = 1; i < rowCount; i++)
                {
                    try
                    {

                        string main = (xlWorksheet.Cells[i, 1] as Range).Text as string;

                        Parserok parser = new Parserok();

                        int parsed = parser.ParseUID(main);

                        if (parsed != 0)
                        {
                            if (emptyStyleData.unicID == parsed.ToString())
                            {
                                if (parser.NumberparseUID(main) >= 3)
                                {
                                    canFill = false;
                                    break;
                                }
                                else
                                {
                                    StyleData newData = new StyleData();
                                    
                                    for (int q = 1; q < colCount; q++)
                                    {
                                        string style = (xlWorksheet.Cells[i, q] as Range).Text as string;
                                        if (help.ClearString(style) == emptyStyleData.style[0])
                                        {
                                            for (int m = 0; m < 3; m++)
                                            {
                                                string newStyle = (xlWorksheet.Cells[i + m, q] as Range).Text as string;
                                                newData.style.Add(newStyle);
                                            }
                                            newData.unicID = emptyStyleData.unicID;
                                            newData.insertPosition = emptyStyleData.insertPosition;

                                            cells.Add(newData);
                                        }
                                    }

                                }
                            }


                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
            }

            return cells;

        }

        /// <summary>
        /// Gets all fields with unicID and position for insert chosen values
        /// </summary>
        /// <param name="xlWorksheet"></param>
        /// <returns></returns>
        public List<InsertCell> GetInsertCells(Worksheet xlWorksheet)
        {
            SetWorkSheet(xlWorksheet);

            Range xlRange = xlWorksheet.UsedRange;

            SetRowAndColumnCount(xlRange);

            List<InsertCell> cells = new List<InsertCell>();

            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    string main = (xlWorksheet.Cells[i, 1] as Range).Text as string;

                    Parserok parser = new Parserok();

                    int parsed = parser.ParseUID(main);

                    if (parsed != 0)
                    {
                        InsertCell insertCell = new InsertCell();
                        Cell cell = new Cell();
                        insertCell.cellWithData = cell;
                        insertCell.cellWithData.unicId = parsed.ToString();

                        insertCell.insertPosition.X = i;

                        cells.Add(insertCell);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    errorCounter++;
                }
            }

            return cells;
        }

        /// <summary>
        /// Get all data by stucture way 2 (when multiply string data in 1 cell)
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        private Cell GetHardCodedDataWay2(int currentRow)
        {
            Cell cell = new Cell();
            Validator validator = new Validator();
            List<string> poundData;

            for (int n = 1; n < colCount + 1; n++)
            {
                string main = (xlWorksheet.Cells[currentRow, n] as Range).Text as string;
                if (validator.ValidatePoundNumberInString(main, out poundData) == 2)
                {
                    foreach (string s in poundData)
                    {
                        cell.poundData.Add(s);
                    }
                }
                if (validator.ValidatePoundNumberInString(main, out poundData) == 1)
                {
                    foreach (string s in poundData)
                    {
                        cell.poundData.Add(s);
                    }
                    break;
                }
            }
            return cell;
        }

        /// <summary>
        /// Get all data by stucture way 1
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        private Cell GetHardCodedDataWay1(int currentRow)
        {
            Cell cell = new Cell();

            Helper help = new Helper();
            Validator validator = new Validator();

            for (int n = 1; n < colCount; n++)
            {
                string main = (xlWorksheet.Cells[currentRow, n] as Range).Text as string;

                help.ClearString(main);

                if (validator.HavePoundSign(main))
                {
                    cell.poundData.Add(main);
                    string nextItem = (xlWorksheet.Cells[currentRow + 1, n] as Range).Text as string;

                    help.ClearString(nextItem);

                    cell.poundData.Add(nextItem);

                    for (int q = n + 1; q < colCount + 1; q++)
                    {

                        string lastItem = (xlWorksheet.Cells[currentRow + 1, q] as Range).Text as string;
                        if (validator.HavePoundSign(lastItem))
                        {
                            cell.poundData.Add(lastItem);
                            break;
                        }
                    }
                    break;
                }
            }
            return cell;
        }

        #endregion
    }
}
