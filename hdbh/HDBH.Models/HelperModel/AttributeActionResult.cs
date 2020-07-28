using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.HelperModel
{
    public class AttributeActionResult
    {
        public bool Successful { get; set; }
        public string Id { get; set; }
        public string ErrorMessage { get; set; }
    }
}
