using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class EODLogic : IEODLogic
    {
        AccountConfiguration_Current currentAccountConfiguration;
        AccountConfiguration_Savings savingsAccountConfiguration;
        AccountConfiguration_Loan loanAccountConfiguration;

        IEODRepository EODRepository;
        IAccountConfigurationRepository accountConfigurationRepository;
        ICustomerAccountRepository<CustomerAccount> customerAccountRepository;
        IGLAccountRepository<GLAccount> gLAccountRepository;
        ILoanAccountRepository loanAccountRepository;
        public EODLogic(IEODRepository eODRepository,
            IAccountConfigurationRepository accountConfigRepo,
            ICustomerAccountRepository<CustomerAccount> custAccntRepo,
            IGLAccountRepository<GLAccount> glAccntRepo,
            ILoanAccountRepository loanAccntRepo
            )
        {
            EODRepository = eODRepository;
            accountConfigurationRepository = accountConfigRepo;
            customerAccountRepository = custAccntRepo;
            gLAccountRepository = glAccntRepo;
            loanAccountRepository = loanAccntRepo;
        }
       
        private static DateTime FinancialDate;
        private const int numOfMonthsInYear = 12;

        private void LaunchEODProcess()
        {
            RetrieveAccountConfigurations(); //Retrieve All Account Configurations
            //bool check = VerifyConfigurationSettings();
            RunEODProcess();
        }

        private void RunEODProcess()
        {
            var CustomerAccounts = customerAccountRepository.RetrieveAllFromDB(); //Retrieve All Customers
            var savingsInterestExpenseAccount = gLAccountRepository.RetrieveById(savingsAccountConfiguration.IEAccountId); //Get Saving Interest Expense GL Account
            var CurrentInterestExpenseAccount = gLAccountRepository.RetrieveById(currentAccountConfiguration.IEAccountId); //Get Current Interest Expense GL Account
            var COTIncomeAccount = gLAccountRepository.RetrieveById(currentAccountConfiguration.COTIncomeId);              //Get COTIncome GL Account 
          //  var LoanInterestIncomeAccount = gLAccountRepository.RetrieveById(loanAccountConfiguration.InterestIncomeId);   //Get Loan Interest Income Account 
            foreach (var customerAccount in CustomerAccounts)
            {
                if (customerAccount.AccountTypeEnum == Core.Entities.AccountTypeEnum.Savings)
                {
                   
                    if (customerAccount.IsClosed == false)
                    {
                        decimal CustomerAmount = customerAccount.AccountBalance;
                        decimal interest = CalculateInterest(savingsAccountConfiguration.SavingsCreditInterestRate, customerAccount.AccountBalance);
                        customerAccount.DailyInterest = interest;
                        if (customerAccount.NumberOfWithdrawals <= 3) //Bank Logic: If number of Withdrawal is greater than three, no Interest
                        {                                     
                            //Credit Customers Account
                            customerAccount.AccountBalance = customerAccount.AccountBalance + interest;
                            //Debit Appropriate GL Account
                            savingsInterestExpenseAccount.AccountBalance = savingsInterestExpenseAccount.AccountBalance + interest;
                            //Reset Daily Interest and Number of Withdrawals
                            customerAccount.DailyInterest = 0M;
                            customerAccount.NumberOfWithdrawals = 0;

                        }
                        else
                        {
                            customerAccount.DailyInterest = 0M;
                        }
                        //Update respective data tables
                        gLAccountRepository.UpdateDB(savingsInterestExpenseAccount);
                        customerAccountRepository.UpdateDB(customerAccount);
                    }


                }
                if (customerAccount.AccountTypeEnum == Core.Entities.AccountTypeEnum.Current)
                {
                    if (customerAccount.IsClosed == false)
                    {

                        decimal CustomerAmount = customerAccount.AccountBalance;
                        decimal interest = CalculateInterest(currentAccountConfiguration.CurrentCreditInterestRate, CustomerAmount);
                        customerAccount.DailyInterest = interest;
                        if (customerAccount.NumberOfWithdrawals <= 3)
                        {
                            
                            //Credit Customers Account
                            customerAccount.AccountBalance = customerAccount.AccountBalance + customerAccount.DailyInterest;
                            //Debit Appropriate GL Account
                            CurrentInterestExpenseAccount.AccountBalance = CurrentInterestExpenseAccount.AccountBalance +customerAccount.DailyInterest;
                            gLAccountRepository.UpdateDB(CurrentInterestExpenseAccount);
                            //Reset Daily Interest
                            customerAccount.DailyInterest = 0.0M;
                        }
                        else
                        {
                            //Reset Daily Interest if number of withdrawals exceed 3
                            customerAccount.DailyInterest = 0.0M;
                        }
                        //Remove COT charges
                        //Debit Customer Account
                        customerAccount.AccountBalance = customerAccount.AccountBalance - customerAccount.Charges;

                        //Credit COTIncome GL Account
                        COTIncomeAccount.AccountBalance = COTIncomeAccount.AccountBalance + customerAccount.Charges;
                        gLAccountRepository.UpdateDB(COTIncomeAccount);

                        //Reset Charges and Number of Withdrawals
                        customerAccount.NumberOfWithdrawals = 0;
                        customerAccount.Charges = 0M;
                        customerAccountRepository.UpdateDB(customerAccount);
                    }
                }

            }
            var loanAccounts = loanAccountRepository.RetrieveAllAccounts();// Retrieve All Loan Accounts
            var LoanInterestIncomeAccount = gLAccountRepository.RetrieveById(loanAccountConfiguration.InterestIncomeId);
            foreach (var loanAccount in loanAccounts)
            {
                //Retrieve Customer Account Attached to Loan Account
                var LinkedLoanCustomerAccount = CustomerAccounts.SingleOrDefault(m => m.Id == loanAccount.CustomerAccountId);
                //Check if customer Account is closed and if loan account is closed
                //If they are not closed calculate EOD on it
                if (!LinkedLoanCustomerAccount.IsClosed && !loanAccount.IsClosed)
                {
                    if (DateTime.Now >= loanAccount.LoanStartDate && DateTime.Now <= loanAccount.LoanDueDate)
                    {
                        decimal dailyLoanTotal = loanAccount.MonthlyLoanBalance / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                        decimal monthlyLoanPrincipal = loanAccount.LoanPrincipal / numOfMonthsInYear;
                        decimal dailyLoanPrincipal = monthlyLoanPrincipal / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                        decimal monthlyLoanInterest = (loanAccount.LoanPrincipal * (decimal)(loanAccountConfiguration.DebitInterestRate / 100) * (decimal)ConvertToYears(loanAccount.LoanDuration)) / numOfMonthsInYear;
                        decimal dailyLoanInterest = monthlyLoanInterest / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                        //Debit Customer Account
                        var CustomerAccount = customerAccountRepository.RetrieveById(loanAccount.CustomerAccountId);
                        CustomerAccount.AccountBalance = CustomerAccount.AccountBalance - dailyLoanTotal;
                        customerAccountRepository.UpdateDB(CustomerAccount);

                        //CreditLoanAccount
                        loanAccount.LoanAccountBalance = loanAccount.LoanAccountBalance - dailyLoanPrincipal;
                        if (loanAccount.LoanAccountBalance == 0)
                        {
                            loanAccount.LoanStats = LoanStatus.LoanComplete;
                        }
                        loanAccountRepository.UpdateLoanAccount(loanAccount);

                        //CreditInterestExpense
                        LoanInterestIncomeAccount.AccountBalance = LoanInterestIncomeAccount.AccountBalance + dailyLoanInterest;
                        gLAccountRepository.UpdateDB(LoanInterestIncomeAccount);


                    }
                }


            }
            //var TillAccounts = gLAccountRepository.RetrieveAllFromDB().Where(m => m.GLAccountType == GLAccountTypeEnum.Till); //Retrieve all till accounts
            //var Vault = gLAccountRepository.RetrieveAllFromDB().Where(m => m.IsVault == true);
            //if (Vault.Count() == 1)
            //{
            //    var BankVault = Vault.Single();
            //    foreach (var tillAccount in TillAccounts)
            //    {
            //        BankVault.AccountBalance = BankVault.AccountBalance + tillAccount.AccountBalance;
            //        tillAccount.AccountBalance = 0;
            //        gLAccountRepository.UpdateDB(BankVault);
            //        gLAccountRepository.UpdateDB(tillAccount);
            //    }
            //}
           
        }

        private void RetrieveAccountConfigurations()
        {
            currentAccountConfiguration = accountConfigurationRepository.RetrieveCurrentConfiguration();
            savingsAccountConfiguration = accountConfigurationRepository.RetrieveSavingsConfiguration();
            loanAccountConfiguration = accountConfigurationRepository.RetrieveLoanConfiguration();
        }
        private decimal CalculateInterest(double rate, decimal Amount)
        {
            var InterestRate = rate / 100;
            var interest = (Amount * (decimal)InterestRate) * 1 / 365;
            return interest;
        }

        public DateTime RetrieveFinancialDate()
        {
            return RetrieveLastEntry().FinancialDate;
           
        }
        public bool IsBusinessClosed()
        {
            return RetrieveLastEntry().IsClosed;
        }
        public async Task<bool> OpenBusiness()
        {
            EOD lastEntry = RetrieveLastEntry();
            FinancialDate = lastEntry.FinancialDate;
            EOD eodEntry = new EOD()
            {
                IsClosed = false,
                FinancialDate = lastEntry.FinancialDate
            };
            return await InsertEOD(eodEntry);
        }
        
      
        public async Task<bool> CloseBusiness()
        {
            EOD lastEntry = RetrieveLastEntry();
            FinancialDate = lastEntry.FinancialDate;
            LaunchEODProcess();
            lastEntry.IsClosed = true;
            lastEntry.FinancialDate = lastEntry.FinancialDate.AddDays(1);
            return await UpdateEntry(lastEntry);
        }
        public bool VerifyConfigurationSettings()
        {
            if (true)
            {

            }
            return true;
        }
        private double ConvertToYears(int loanDuration)
        {
            double yearsConversation = (loanDuration * 1) / 12;
            return yearsConversation;
        }

        //Database Operations
        public async Task<bool> UpdateEntry(EOD eODEntry)
        {
            var check = EODRepository.UpdateEODEntry(eODEntry);
            return await check;
        }
        public EOD RetrieveLastEntry()
        {
            var eod = EODRepository.RetrieveLastEntry();
            return eod;
        }
        public async Task<bool> InsertEOD(EOD eod)
        {
            return await EODRepository.AddEODEntryToDB(eod);
        }

    }
}