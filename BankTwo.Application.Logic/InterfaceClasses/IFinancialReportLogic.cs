using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface IFinancialReportLogic
    {
        IEnumerable<FinancialReportGLAccount> GetAllAssetAccounts();

        IEnumerable<FinancialReportGLAccount> GetAllLiabilitiesAccounts();

        IEnumerable<FinancialReportGLAccount> GetAllIncomeAccounts();

        IEnumerable<FinancialReportGLAccount> GetAllExpenseAccounts();

        IEnumerable<FinancialReportGLAccount> GetAllCapitalAccounts();

        IEnumerable<FinancialReportCustomerAccount> GetAllCustomerAccounts();
       
        IEnumerable<FinancialReportLoanAccount> GetAllLoanAccounts();

    }
}
