using BankTwo.Application.Core.Entities;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.InterfaceClasses
{
    public interface IEODRepository
    {
        Task<bool> AddEODEntryToDB(EOD EODEntry);

        // IEnumerable<EOD> RetrieveAllEODEntries();

        Task<bool> UpdateEODEntry(EOD eodUpdate);

        EOD RetrieveLastEntry();
     
    }
}
