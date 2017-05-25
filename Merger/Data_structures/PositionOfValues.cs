using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merger.Data_structures
{
    public class PositionOfValues
    {
        public List<string> stringValue = new List<string>();
        public List<int> xPosition = new List<int>();

        /// <summary>
        /// Fills List with array values
        /// </summary>
        /// <param name="values"></param>
        public PositionOfValues(params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                stringValue.Add(values[i]);
            }
        }

    }
}
