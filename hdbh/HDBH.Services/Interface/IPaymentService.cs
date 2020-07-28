using HDBH.Models.DatabaseModel.Payment;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services.Interface
{
    public interface IPaymentService
    {
        ResultSearchListPayment searchRepayment(SearchPaymentModel input);
        GetListScheduleUnpaid getListScheduleUnpaid(InputGetDetailPaymentInsert input);
        ResultModel insertRepayment(InputInsertUpdatePayment input);
        GetPaymentDetailModel getDetailRepayment(InputGetDetailPaymentApprove input);
        ResultModel updateRepayment(InputInsertUpdatePayment input);
        ResultModel approveRepayment(ApprovePaymentModel input);
        ResultSearchListPayment searchRepaymentInau(SearchPaymentModel input);
        ResultSearchListRepaymentSchedule searchRepaymentSchedule(SearchRepaymentSchedule input);

    }
}
