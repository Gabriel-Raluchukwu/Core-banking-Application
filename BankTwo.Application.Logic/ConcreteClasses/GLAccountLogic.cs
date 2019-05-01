using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.Utilities;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class GLAccountLogic:IGLAccountLogic
    {
        public IBranchLogic branchLogic;
        private IGLCategoryLogic gLCategoryLogic;
        private IGLAccountRepository<GLAccount> gLAccountRepository;

        public GLAccountLogic(IGLAccountRepository<GLAccount> accountRepository,
            IGLCategoryLogic gLCategory, IBranchLogic branch)
        {
            this.gLAccountRepository = accountRepository;
            this.gLCategoryLogic = gLCategory;
            this.branchLogic = branch;
        }

        public async Task<bool> AddAccount(GLAccountViewModel gLAccountViewModel)
        {
            int MainCategoryNumber = gLCategoryLogic.GetMainAccountId(gLAccountViewModel.GLCategoryId.Value);
            int AccountCode = AutoGenerator.GenerateAccountCode(MainCategoryNumber,gLAccountViewModel.GLCategoryId.Value);
            gLAccountViewModel.GLAccountCode = AccountCode;

            var glAccount = Mapper.Map<GLAccountViewModel, GLAccount>(gLAccountViewModel);

            return await gLAccountRepository.InsertToDB(glAccount);

        }
        public IEnumerable<GLAccountDisplayViewModel> GLAccountDisplay()
        {
            IEnumerable<GLAccountDisplayViewModel> gLAccountDisplayViewModels;
            var accounts = gLAccountRepository.RetrieveAllFromDB();
            if (accounts != null)
            {
                gLAccountDisplayViewModels = accounts.Select(Mapper.Map<GLAccount,GLAccountDisplayViewModel>).ToList();
                return gLAccountDisplayViewModels;
            }
            return null;
        }
        public GLAccountViewModel RetrieveAccountById(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            var retrievedAccount = gLAccountRepository.RetrieveById(Id);
            if (retrievedAccount == null)
            {
                return null;
            }
            var gLAccounViewModel = Mapper.Map<GLAccount, GLAccountViewModel>(retrievedAccount);

            return gLAccounViewModel;
        }
        public GLAccount RetrieveGLAccount(int id)
        {
            var retrievedAccount = gLAccountRepository.RetrieveById(id);
            return retrievedAccount;
        }
        public async Task<bool> EditAccount(GLAccountViewModel gLAccountViewModel)
        {
            int id = int.Parse(Encrypt.Decode(gLAccountViewModel.EncryptedId));
            var accountToUpdate = gLAccountRepository.RetrieveById(id);
            //ManualMapping
            accountToUpdate.DateLastUpdated = gLAccountViewModel.DateLastUpdated;
            accountToUpdate.GLAccountName = gLAccountViewModel.GLAccountName;
            accountToUpdate.BranchId = gLAccountViewModel.BranchId;

            return await gLAccountRepository.UpdateDB(accountToUpdate);
        }
        public async Task<bool> UpdateAccountBalance(GLAccount gLAccount)
        {
            var accountToUpdate = gLAccountRepository.RetrieveById(gLAccount.Id);
            //ManualMapping
            accountToUpdate.AccountBalance = gLAccount.AccountBalance;
            return await gLAccountRepository.UpdateDB(accountToUpdate);
        }
        public async Task<bool> DeleteAccount(string id)
        {
            int Id = int.Parse(Encrypt.Decode(id));
            return await gLAccountRepository.DeleteFromDB(Id);
        }

        public IEnumerable<GLAccountDropDownViewModel> populateGLAccountDropDown()
        {
            var accounts = gLAccountRepository.RetrieveAllFromDB();
            var gLaccountDropDown = accounts.Select(Mapper.Map<GLAccount, GLAccountDropDownViewModel>).ToList();
            return gLaccountDropDown;

        }

        //Helper Methods
        public async Task<bool> MakeTill(int id)
        {
            var glAccount = RetrieveGLAccount(id);
            glAccount.GLAccountType = GLAccountTypeEnum.Till;
            return await gLAccountRepository.UpdateDB(glAccount);
        }
        public async Task<bool> MakeVault(int id)
        {
            var glAccount = RetrieveGLAccount(id);
            glAccount.IsVault = true;
            return await gLAccountRepository.UpdateDB(glAccount);
        }
        public async Task<bool> ResetVault()
        {
            var glAccount = gLAccountRepository.RetrieveAllFromDB().Where(m => m.IsVault == true).ToList();
            var single = glAccount.Single();
            single.IsVault = false;
            return await gLAccountRepository.UpdateDB(single);
        }
        public GLAccountDropDownViewModel RetrieveVaultAccount()
        {
            var glAccount = gLAccountRepository.RetrieveAllFromDB().SingleOrDefault(m => m.IsVault == true);
            return Mapper.Map<GLAccount, GLAccountDropDownViewModel>(glAccount);
            
        }
        public IEnumerable<BranchViewModel> PopulateBranchDropDown()
        {
            return branchLogic.PopulateBranchDropDownList();
        }

        public IEnumerable<GLCategoryDropDownViewModel> PopulateCategoryDropDown()
        {
            return gLCategoryLogic.PopulateCategoryDropDown();
        }
    }
}
