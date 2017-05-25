using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merger.Data_structures;
using Microsoft.Office.Interop.Excel;
using Marshal = System.Runtime.InteropServices.Marshal;
using Merger.Excel1;
using Merger.Parser;
using System.IO;
using System.Collections;

namespace Merger
{
    public class DelegateLogic
    {
        #region Fields

        private ExcelObject exlObj;
        private List<string> outputString = new List<string>();

        public event EventHandler Changed;

        protected virtual void OnChange(EventArgs e)
        {
            EventHandler handler = Changed;
            if (handler != null)
            {
                handler(this, e);
            }

        }

        /// <summary>
        /// importing old logic
        /// </summary>
        public List<List<InsertCell>> allData = new List<List<InsertCell>>();
        public List<PositionOfValues> allPositions = new List<PositionOfValues>();
        public List<int> errorCounter = new List<int>();
        public int n = 1;
        public List<List<StyleData>> styleData = new List<List<StyleData>>();
        public List<List<StyleData>> missingStyles = new List<List<Data_structures.StyleData>>();
        public List<List<StyleData>> allMissingStyles = new List<List<Data_structures.StyleData>>();
        public List<int> missigPages = new List<int>();
        #endregion

        #region Controling Logic

        /// <summary>
        /// Sets Excel objext to current object
        /// </summary>
        /// <param name="exl"></param>
        public void SetExcelObject(ExcelObject exl)
        {
            exlObj = exl;
            Helper help = new Helper();
            outputString.Add(help.GetFileName(exlObj.fileWithStructure)+"\n");
        }

        /// <summary>
        /// Gets output string for view
        /// </summary>
        /// <returns></returns>
        public List<string> GetOutputString()
        {
            return outputString;
        }

        /// <summary>
        /// Get number of current excel file sheets count
        /// </summary>
        /// <returns></returns>
        public int GetMaximumStepCount()
        {
            Workbook exelWithoutData = OpenExellFile(exlObj.fileWithStructure);
            int z = exelWithoutData.Sheets.Count;
            exelWithoutData.Close();
            Marshal.ReleaseComObject(exelWithoutData);
            return z;
        }


        /// <summary>
        /// Executes Fill Style method to fill styles 
        /// </summary>
        public void FillStyles(){
            //imported

            Workbook exelWithoutData = OpenExellFile(exlObj.fileWithStructure);
            Workbook exelWithData = OpenExellFile(exlObj.fileWithData);
            n = exelWithoutData.Sheets.Count;

            for (int i = 1; i <= n; i++)
            {
                FillEmptyStyles(exelWithData, exelWithoutData, i);
                OnChange(EventArgs.Empty);
            }

            Parserok parser = new Parserok();
            Validator validator = new Validator();

            int q = 0;
            foreach (List<Data_structures.StyleData> style in styleData)
            {
                q++;
                if (validator.ValidateStyle(style))
                {
                    continue;
                }
                else
                {
                    missingStyles.Add(parser.FiltreMissingStyle(style));
                    missigPages.Add(q);
                    //outputString.Add(q.ToString()+"\n");
                }
            }

            Excel excelWorker = new Excel();
            int y = 0;
            bool flag = false;
            foreach (int i in missigPages)
            {
                allMissingStyles.Add(excelWorker.GetGoodStylesFromBadExcel((Worksheet)exelWithData.Sheets[missigPages[y]], missingStyles[y], out flag));
                y++;
            }


            //write all to global insertion list
            exelWithoutData.Close();
            exelWithData.Close();

            Marshal.ReleaseComObject(exelWithoutData);
            Marshal.ReleaseComObject(exelWithData);
            //end import
            if (exlObj.mode == 1)
            {
                Workbook exelWithoutDatanew = OpenExellFile(exlObj.fileWithStructure);
                int l = 0;
                foreach (List<Data_structures.StyleData> someStyles in allMissingStyles)
                {
                    Worksheet sheetBook = (Worksheet)exelWithoutDatanew.Sheets[missigPages[l]];
                    foreach (Data_structures.StyleData s in someStyles)
                    {
                        sheetBook.Cells[s.insertPosition.Y, s.insertPosition.X] = s.style[0];
                        sheetBook.Cells[s.insertPosition.Y + 1, s.insertPosition.X] = s.style[1];
                        sheetBook.Cells[s.insertPosition.Y + 2, s.insertPosition.X] = s.style[2];
                    }
                    l++;
                }
                Helper help = new Helper();
                string fileName = help.GetFileName(exlObj.fileWithStructure);
                //oh man what a bad code
                try
                {
                    exelWithoutDatanew.SaveAs(Directory.GetCurrentDirectory() + "\\" + fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                catch (Exception ex)
                {
                    outputString.Add("error:" + ex.Message + "\n");
                }
                finally
                {
                    exelWithoutDatanew.Close();
                    Marshal.ReleaseComObject(exelWithoutDatanew);
                }

                foreach (int i in errorCounter)
                {
                    outputString.Add("error" + i.ToString() + "\n");
                }

                PosCheckMissingStyles();

                outputString.Add("DONE!" + "\n");
            }
            
        }

        /// <summary>
        /// Cheking missing Styles
        /// </summary>
        public void PosCheckMissingStyles()
        {
            Helper help = new Helper();
            string fileName = help.GetFileName(exlObj.fileWithStructure);
            //oh man what a bad code
            missingStyles.Clear();
            string createdFileWithSomeMissingStyles = Directory.GetCurrentDirectory() + "\\" + fileName + ".xls";
            Workbook exelWithoutData = OpenExellFile(createdFileWithSomeMissingStyles);
            Workbook exelWithData = OpenExellFile(exlObj.fileWithData);
            n = exelWithoutData.Sheets.Count;
            styleData.Clear();


            for (int i = 1; i <= n; i++)
            {
                
                FillEmptyStyles(exelWithData, exelWithoutData, i);

                OnChange(EventArgs.Empty);
            }

            Parserok parser = new Parserok();
            Validator validator = new Validator();

            int q = 0;
            foreach (List<Data_structures.StyleData> style in styleData)
            {
                q++;
                if (validator.ValidateStyle(style))
                {
                    continue;
                }
                else
                {
                    missingStyles.Add(parser.FiltreMissingStyle(style));
                    outputString.Add(q.ToString()+"\n");
                }
            }

            exelWithoutData.Close();
            exelWithData.Close();

            Marshal.ReleaseComObject(exelWithoutData);
            Marshal.ReleaseComObject(exelWithData);
        }
        
        /// <summary>
        /// Executes FillPounds method to fill pounds
        /// </summary>
        public void FillPounds(){
            //logic to fill pounds
            //imported code

            Workbook exelWithoutData = OpenExellFile(exlObj.fileWithStructure);
            Workbook exelWithData = OpenExellFile(exlObj.fileWithData);
            n = exelWithoutData.Sheets.Count;

            for (int i = 1; i <= n; i++)
            {
                DoMerge(exelWithData, exelWithoutData, i);
                //on update
                OnChange(EventArgs.Empty);
            }

            exelWithoutData.Close();
            exelWithData.Close();

            Marshal.ReleaseComObject(exelWithoutData);
            Marshal.ReleaseComObject(exelWithData);

            int p = 1;
            Workbook exelWithoutDatanew = OpenExellFile(exlObj.fileWithStructure);
            foreach (List<InsertCell> cel in allData)
            {
                int q = (int)(((p + n) * 100) / (n * 2));
                Worksheet sheetBook = (Worksheet)exelWithoutDatanew.Sheets[p];
                foreach (InsertCell cell in cel)
                {
                    if (cell.cellWithData.poundData.Count < 3)
                    {
                        errorCounter.Add(q);
                    }
                    else
                    {
                        sheetBook.Cells[cell.insertPosition.X, allPositions[p - 1].xPosition[0]] = cell.cellWithData.poundData[0];
                        sheetBook.Cells[cell.insertPosition.X + 1, allPositions[p - 1].xPosition[0]] = cell.cellWithData.poundData[1];
                        sheetBook.Cells[cell.insertPosition.X + 1, allPositions[p - 1].xPosition[1]] = cell.cellWithData.poundData[2];

                    }

                }
                p++;
            }

            //to do insert missing styles

            int l = 0;
            foreach (List<Data_structures.StyleData> someStyles in allMissingStyles)
            {
                Worksheet sheetBook = (Worksheet)exelWithoutDatanew.Sheets[missigPages[l]];
                foreach (Data_structures.StyleData s in someStyles)
                {
                    sheetBook.Cells[s.insertPosition.Y, s.insertPosition.X] = s.style[0];
                    sheetBook.Cells[s.insertPosition.Y + 1, s.insertPosition.X] = s.style[1];
                    sheetBook.Cells[s.insertPosition.Y + 2, s.insertPosition.X] = s.style[2];
                }
                l++;
            }
            Helper help = new Helper();
            string fileName = help.GetFileName(exlObj.fileWithStructure);
            //oh man what a bad code
            try
            {
                exelWithoutDatanew.SaveAs(Directory.GetCurrentDirectory() + "\\" + fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                outputString.Add("error:" + ex.Message + "\n");
            }
            finally
            {
                exelWithoutDatanew.Close();
                Marshal.ReleaseComObject(exelWithoutDatanew);
            }

            foreach (int i in errorCounter)
            {
                outputString.Add("error" + i.ToString() + "\n");
            }

            PosCheckMissingStyles();

            outputString.Add("DONE!" + "\n");
            //end import
        }

        #endregion

        #region Imported code

        /// <summary>
        /// open exel file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Workbook OpenExellFile(string filePath)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, XlPlatform.xlWindows, "", false, false, 0, false, 1, 0);
            Marshal.ReleaseComObject(xlApp);
            return xlWorkbook;
        }

        /// <summary>
        /// Fill Empty styles in file with missing styles
        /// </summary>
        /// <param name="exel1"></param>
        /// <param name="exel2"></param>
        /// <param name="sheetNumber"></param>
        public void FillEmptyStyles(Workbook exel1, Workbook exel2, int sheetNumber)
        {
            List<Data_structures.StyleData> coolCell = new List<Data_structures.StyleData>();

            Excel excelWorker = new Excel();

            coolCell = excelWorker.GetAllStylesFromGoodExcel((Worksheet)exel2.Sheets[sheetNumber]);

            styleData.Add(coolCell);
        }

        /// <summary>
        /// Merge data with cells but without positions
        /// </summary>
        /// <param name="exel1"></param>
        /// <param name="exel2"></param>
        /// <param name="sheetNumber"></param>
        public void DoMerge(Workbook exel1, Workbook exel2, int sheetNumber)
        {

            List<Cell> coolCell = new List<Cell>();

            Excel excelWorker = new Excel();

            coolCell = excelWorker.GetAllDataWithUnicID((Worksheet)exel1.Sheets[sheetNumber]);

            int errorCount = excelWorker.GetErrorCount();

            if (errorCount >= 1)
            {
                errorCounter.Add(n);
                errorCounter.Add(errorCount);
            }

            List<InsertCell> insertCell = new List<InsertCell>();

            insertCell = excelWorker.GetInsertCells((Worksheet)exel2.Sheets[sheetNumber]);

            int badValue = 1111111111;

            Validator validator = new Validator();

            //if (!(validator.ValidateUnmergedCells(coolCell, insertCell)))
            //{
            //   errorCounter.Add(badValue);
            //}

            Data_structures.PositionOfValues values = new Data_structures.PositionOfValues("SalesCost", "StockUnits");

            foreach (string s in values.stringValue)
            {
                values.xPosition.Add(excelWorker.GetXAxisOfNeededCell((Worksheet)exel2.Sheets[sheetNumber], s));
            }

            Parserok parser = new Parserok();

            insertCell = parser.MergeData(coolCell, insertCell);

            allData.Add(insertCell);

            allPositions.Add(values);

        }
        #endregion

    }
}
