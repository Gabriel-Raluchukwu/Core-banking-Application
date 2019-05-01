using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.InterfaceClasses
{
    public interface IDatabaseOperations<T>
    {
        Task<bool> InsertToDB( T item );
        T RetrieveById(int item);
        IEnumerable<T> RetrieveAllFromDB();
        Task<bool> UpdateDB(T item);
        Task<bool> DeleteFromDB(int item);
        
    }
}
