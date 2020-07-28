using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class SearchPagging
    {
        public int start { get; set; }
        public int length { get; set; }
        private int _pageSize;

        public int pageSize
        {
            get { return length; }
            set { _pageSize = value; }
        }
        private int _pageNumber;

        public int pageNumber
        {
            get { return start != 0 ? start / length + 1 : 1; }
            set { _pageNumber = value; }
        }
    }
}
