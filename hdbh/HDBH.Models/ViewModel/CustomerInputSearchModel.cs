using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CustomerInputSearchModel : SearchPagging
    {
       public  string cif { get; set; }
        public string name { get; set; }
    }
}
