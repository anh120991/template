using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services.Interface
{
    public interface ICustomerService
    {
        CustomerModel searchCustomer(CustomerInputSearchModel input);
    }
}
