using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Merger.Data_structures
{
    public class ExcelObject
    {
        public ExcelObject(string path1,string path2, int mode = 0)
        {
            fileWithData = path1;
            fileWithStructure = path2;
            this.mode = mode;
        }

        public ExcelObject()
        {
        }

        public int mode { get; set; }

        public string fileWithData { get; set; }

        public string fileWithStructure { get; set; }
    }
}
