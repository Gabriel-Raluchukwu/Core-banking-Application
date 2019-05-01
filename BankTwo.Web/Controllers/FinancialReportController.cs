using BankTwo.Application.Logic.InterfaceClasses;
using System.Web.Mvc;
using ViewModels;

namespace BankTwo.Web.Controllers
{
    public class FinancialReportController : Controller
    {
        IFinancialReportLogic financialReportLogic;
        public FinancialReportController(IFinancialReportLogic reportLogic)
        {
            financialReportLogic = reportLogic;
        }
        
        public ActionResult ProfitAndLoss()
        {
            FinancialReport profitAndLossReport = new FinancialReport
            {
                IncomeAccounts = financialReportLogic.GetAllIncomeAccounts(),
                ExpenseAccounts = financialReportLogic.GetAllExpenseAccounts()
            };
            return View(profitAndLossReport);
        }
        public ActionResult BalanceSheet()
        {
            FinancialReport BalanceSheet = new FinancialReport
            {
                AssetAccounts = financialReportLogic.GetAllAssetAccounts(),
                CapitalAccounts=financialReportLogic.GetAllCapitalAccounts(),
                LiabilitiesAccounts = financialReportLogic.GetAllLiabilitiesAccounts(),
                CustomerAccounts = financialReportLogic.GetAllCustomerAccounts(),
                LoanAccounts = financialReportLogic.GetAllLoanAccounts()
            };
            return View(BalanceSheet);
        }
        public ActionResult TrialBalance()
        {
            FinancialReport TrialBalance = new FinancialReport
            {
                AssetAccounts = financialReportLogic.GetAllAssetAccounts(),
                CapitalAccounts = financialReportLogic.GetAllCapitalAccounts(),
                LiabilitiesAccounts = financialReportLogic.GetAllLiabilitiesAccounts(),
                CustomerAccounts = financialReportLogic.GetAllCustomerAccounts(),
                IncomeAccounts = financialReportLogic.GetAllIncomeAccounts(),
                ExpenseAccounts = financialReportLogic.GetAllExpenseAccounts(),
                LoanAccounts = financialReportLogic.GetAllLoanAccounts()
            };
            return View(TrialBalance);
        }
    }
}