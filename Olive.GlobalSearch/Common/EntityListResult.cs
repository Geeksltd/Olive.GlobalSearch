using System;
using System.Collections.Generic;
using System.Text;

namespace Olive.GlobalSearch
{
    /// <summary>
    /// Represent a collection of data item include additional parameteres to store pagination arguments
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityListResult<T>
    {
        /// <summary>
        /// Represent the Collection of Data which is varied base on T element 
        /// </summary>
        public IEnumerable<T> Data;

        /// <summary>
        /// Represent the total count of items exsits 
        /// </summary>
        public long TotalCount { set; get; }

        /// <summary>
        /// Represent the start index (0 based) of the Data, which filled in Data property
        /// </summary>
        public int StartIndex { set; get; }

        /// <summary>
        /// Represent the Items per page 
        /// </summary>
        public int Size { set; get; }
    }
}
