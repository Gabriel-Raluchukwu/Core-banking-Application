using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.ConcreteClasses
{
    public class MainCategory
    {
        ApplicationDbContext dbContext;
        public MainCategory()
        {
            dbContext = new ApplicationDbContext();
        }
        public MainCategories RetrieveById(int id)
        {
            var mainCategory = dbContext.MainCategories.Find(id);
            return mainCategory;
        }
    }
}
