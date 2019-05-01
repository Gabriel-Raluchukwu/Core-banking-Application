using AutoMapper;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.InterfaceClasses;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using BankTwo.Application.Logic.InterfaceClasses;

namespace BankTwo.Application.Logic.ConcreteClasses
{
    public class TellerManagement:ITellerManagement
    {
        IGLAccountRepository<GLAccount> gLAccountRepo;
        public TellerManagement(IGLAccountRepository<GLAccount> accountRepository)
        {
            gLAccountRepo = accountRepository;

        }
        public IEnumerable<GLAccountDropDownViewModel> RetrieveNonTillGLAccount()
        {
            var nonTillAccounts = gLAccountRepo.RetrieveAllFromDB().Where(p => p.GLAccountType == GLAccountTypeEnum.Default).Where(p => p.GLCategory.GLCategoryName.Contains("Cash Assets")).
              Select(Mapper.Map<GLAccount,GLAccountDropDownViewModel>).ToList();  

            return nonTillAccounts;
        }
        public IEnumerable<GLAccountDropDownViewModel> RetrieveTillGLAccount()
        {
            var tillAccounts = gLAccountRepo.RetrieveAllFromDB().Where(p => p.GLAccountType == GLAccountTypeEnum.Till).Where(p => p.GLCategory.GLCategoryName.Contains("Cash Assets")).
              Select(Mapper.Map<GLAccount, GLAccountDropDownViewModel>).ToList(); /* Where(p => p.GLCategory.GLCategoryName == "Cash Assets").*/

            return tillAccounts;
        }
        
    }

}
