using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            resultList = new List<CustomerItemModel>();
        }
        public List<CustomerItemModel> resultList { get; set; }
        public int TotalRecord { get; set; }
    }
    public class CustomerItemModel
    {
        public string cif { get; set; }
        public string customerName { get; set; }
        public string customerShortName { get; set; }
        public string custGroupId { get; set; }
        public string refT24 { get; set; }
        public string customerShortcutName { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
    }
}
