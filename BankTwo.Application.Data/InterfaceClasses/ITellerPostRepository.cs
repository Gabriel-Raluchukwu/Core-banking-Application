﻿using BankTwo.Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data.InterfaceClasses
{
    public interface ITellerPostRepository
    {
         Task<bool> AddTellerPostToDB(TellerPost tellerTransaction);

         IEnumerable<TellerPost> RetrieveAllTellerPosts();
       
    }
}
