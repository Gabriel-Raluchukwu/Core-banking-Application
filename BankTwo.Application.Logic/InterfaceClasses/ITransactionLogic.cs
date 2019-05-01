using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BankTwo.Application.Logic.InterfaceClasses
{
    public interface ITransactionLogic
    {
        Task<bool> AddPosting(TransactionViewModel transactionViewModel);

        IEnumerable<TransactionViewModel> ViewPostings();

        IEnumerable<GLAccountDropDownViewModel> PopulateDropDown();

        bool PostToGLAccount(TransactionViewModel transactionViewModel);

    }
}
