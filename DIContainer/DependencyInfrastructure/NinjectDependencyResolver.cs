
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BankTwo.Application.Core.Entities;
using BankTwo.Application.Data.ConcreteClasses;
using BankTwo.Application.Data.InterfaceClasses;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using BankTwo.Application.Logic.InterfaceClasses;
using BankTwo.Application.Logic.ConcreteClasses;

namespace DIContainer.DependencyInfrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            // put bindings here

            kernel.Bind<IEODRepository>().To<EODRepository>();
            kernel.Bind<IEODLogic>().To<EODLogic>();

            kernel.Bind<IApplicationUser>().To<ApplicationUserDB>();
            kernel.Bind<IBranch<Branch>>().To<BankBranch>();
            kernel.Bind<IBranchLogic>().To<BranchLogic>();

            //Roles Dependencies
            kernel.Bind<IRoleLogic>().To<RoleLogic>();
            kernel.Bind<IRoleRepository<IdentityRole>>().To<RolesRepository>();

            //GL Category Dependencies
            kernel.Bind<IGLCategoryRepository<GLCategory>>().To<GLCategoryRepository>();
            kernel.Bind<IGLCategoryLogic>().To<GLCategoryLogic>();

            //GL Account Dependencies
            kernel.Bind<IGLAccountRepository<GLAccount>>().To<GLAccountRepository>();
            kernel.Bind<IGLAccountLogic>().To<GLAccountLogic>();

            //Customer Dependencies 
            kernel.Bind<ICustomerRepository<Customer>>().To<CustomerRepository>();
            kernel.Bind<ICustomerLogic>().To<CustomerLogic>();

            //Customer Account Dependencies 
            kernel.Bind<ICustomerAccountRepository<CustomerAccount>>().To<CustomerAccountRepository>();
            kernel.Bind<ICustomerAccountLogic>().To<CustomerAccountLogic>();

            //Transaction Dependencies 
            kernel.Bind<ITransactionRepository>().To<TransactionRepository>();
            kernel.Bind<ITransactionLogic>().To<TransactionLogic>();

            kernel.Bind<ITellerManagement>().To<TellerManagement>();
            kernel.Bind<ITellerPostRepository>().To<TellerPostRepository>();

            kernel.Bind<ITellerPost>().To<TellerPosts>();

            //Account Configuration Dependencies
            kernel.Bind<IAccountConfigurationRepository>().To<AccountConfigurationRepository>();
            kernel.Bind<IAccountConfigurationLogic>().To<AccountConfigurationLogic>();

            kernel.Bind<ILoanAccountRepository>().To<LoanAccountRepository>();
            kernel.Bind<ILoanAccountLogic>().To<LoanAccountLogic>();

            //Financial Report
            kernel.Bind<IFinancialReportLogic>().To<FinancialReportLogic>();

            
        }
    }
}