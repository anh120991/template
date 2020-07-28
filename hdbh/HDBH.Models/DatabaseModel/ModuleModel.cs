using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    [Serializable]
    public class ModuleModel
    {
        public string moduleCode { get; set; }
        public string moduleName { get; set; }
        public string moduleUrl { get; set; }
        public string isSubModule { get; set; }
        public string parentModuleId { get; set; }
        public string modulePermission { get; set; }
        public string moduleDescription { get; set; }
        public int isActive { get; set; }
    }
}
