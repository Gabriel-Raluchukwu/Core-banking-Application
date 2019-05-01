using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using BankTwo.Application.Logic.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class FinancialReportLogic:IFinancialReportLogic
    {
        ICustomerAccountRepository<CustomerAccount> customerAccountRepository;
        IGLAccountRepository<GLAccount> gLAccountRepository;
        ILoanAccountRepository loanAccountRepository;
        public FinancialReportLogic(ICustomerAccountRepository<CustomerAccount> customerAccounts, IGLAccountRepository<GLAccount> gLAccounts, ILoanAccountRepository loanAccounts)
        {
            customerAccountRepository = customerAccounts;
            gLAccountRepository = gLAccounts;
            loanAccountRepository = loanAccounts;
        }
        public IEnumerable<FinancialReportGLAccount> GetAllAssetAccounts()
        {
            var assetGLAccounts = gLAccountRepository.RetrieveAllFromDB().Where(a => a.GLAccountCode.ToString().Substring(0, 1) == "1");
            var assetGLAccountViewModel = assetGLAccounts.Select(Mapper.Map<GLAccount, FinancialReportGLAccount>).ToList();
            return assetGLAccountViewModel;
        }
        public IEnumerable<FinancialReportGLAccount> GetAllLiabilitiesAccounts()
        {
            var liabilitiesGLAccounts = gLAccountRepository.RetrieveAllFromDB().Where(a => a.GLAccountCode.ToString().Substring(0, 1) == "2");
            var liabilitiesGLAccountViewModel = liabilitiesGLAccounts.Select(Mapper.Map<GLAccount, FinancialReportGLAccount>).ToList();
            return liabilitiesGLAccountViewModel;
        }
        public IEnumerable<FinancialReportGLAccount> GetAllIncomeAccounts()
        {
            var incomeGLAccounts = gLAccountRepository.RetrieveAllFromDB().Where(a => a.GLAccountCode.ToString().Substring(0, 1) == "4");
            var incomeGLAccountViewModel = incomeGLAccounts.Select(Mapper.Map<GLAccount, FinancialReportGLAccount>).ToList();
            return incomeGLAccountViewModel;
        }
        public IEnumerable<FinancialReportGLAccount> GetAllExpenseAccounts()
        {
            var expenseGLAccounts = gLAccountRepository.RetrieveAllFromDB().Where(a => a.GLAccountCode.ToString().Substring(0, 1) == "5");
            var expenseGLAccountViewModel = expenseGLAccounts.Select(Mapper.Map<GLAccount, FinancialReportGLAccount>).ToList();
            return expenseGLAccountViewModel;
        }
        public IEnumerable<FinancialReportGLAccount> GetAllCapitalAccounts()
        {
            var capitalGLAccounts = gLAccountRepository.RetrieveAllFromDB().Where(a => a.GLAccountCode.ToString().Substring(0, 1) == "3");
            var capitalGLAccountViewModel = capitalGLAccounts.Select(Mapper.Map<GLAccount, FinancialReportGLAccount>).ToList();
            return capitalGLAccountViewModel;
        }
        public IEnumerable<FinancialReportCustomerAccount> GetAllCustomerAccounts()
        {
            var customerAccounts = customerAccountRepository.RetrieveAllFromDB();
            var customerAccountViewModel = customerAccounts.Select(Mapper.Map<CustomerAccount, FinancialReportCustomerAccount>).ToList();
            return customerAccountViewModel;
        }
        public IEnumerable<FinancialReportLoanAccount> GetAllLoanAccounts()
        {
            var loanAccounts = loanAccountRepository.RetrieveAllAccounts();
            var loanAccountViewModel = loanAccounts.Select(Mapper.Map<LoanAccount, FinancialReportLoanAccount>).ToList();
            return loanAccountViewModel;
        }
    }
    
}
