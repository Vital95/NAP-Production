using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Merger.Data_structures;

namespace Merger
{
    public class View
    {

        #region Work with view

        /// <summary>
        /// Check listBoxes items for expected values
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public bool ValidateListBoxItems(ListBox list1, ListBox list2)
        {
            bool result = true;
            for(int i = 0; i< list1.Items.Count; i++)
            {
                try {
                    Helper help = new Helper();
                    string listString1 = list1.Items[i].ToString();
                    string listString2 = list2.Items[i].ToString();
                    if (help.IsMatching(listString1, listString2) && help.HaveGoodExt(listString1, listString2))
                    {
                        continue;
                    }
                    else
                    {
                        return result;
                    }
                }
                catch(Exception ex)
                {
                    if( ex != null)
                    {
                        return false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Crteates a list of Excel Objects from view
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<ExcelObject> CreateExcelObjects(ListBox list1, ListBox list2, object obj)
        {
            List<ExcelObject> excelObjects = new List<ExcelObject>();
            for (int i = 0; i < list1.Items.Count; i++)
            {
                Helper help = new Helper();
                ExcelObject excelObj = new ExcelObject();

                int mode = GetModeFromView(obj);

                excelObj = help.CreateInstanceOfExcelObject(list1.Items[i].ToString(), list2.Items[i].ToString(), mode);

                excelObjects.Add(excelObj);
            }
            return excelObjects;
        }

        /// <summary>
        /// Gets work modefrom group box 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int GetModeFromView(object obj)
        {
            int mode = 0;
            if(obj is GroupBox)
            {
                foreach(Control childObject in (obj as GroupBox).Controls)
                {
                    if(childObject is RadioButton)
                    {
                        if((childObject as RadioButton).Checked == true && (childObject as RadioButton).Text == "Style")
                        {
                            return 1;
                        }
                        else
                        {
                            mode = 2;
                        }
                    }
                }
            }
            return mode;
        }

        #endregion
     
    }
}
