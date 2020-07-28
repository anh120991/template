using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class InsuranceContractFormModel
    {
        public string contractFormCode { get; set; }

        public string contractFormName { get; set; }

        public string contractFormStatus { get; set; }

        public string lastUserUpdated { get; set; }

        public DateTime? lastDatetimeUpdated { get; set; }
        public int isNonlifeInsurance { get; set; }
    }
}
