using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Merger.Data_structures
{
    public  class Cell
    {
        public string unicId;
        public List<string> poundData = new List<string>();
    }

    public class InsertCell
    {
        public Cell cellWithData;
        public Point insertPosition;
    }
}
