using AutoMapper;
using BankTwo.Application.Core.Entities;
using System;
using ViewModels;
using BankTwo.Application.Logic.Utilities;

namespace BankTwo.Application.Logic
{
    public class MappingProfile : Profile
    {
       
        public MappingProfile()
        {
            CreateMap<Branch, BranchViewModel>().ReverseMap();

            //
            CreateMap<ApplicationUser, RegisterViewModel>().
                BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id));
            CreateMap<RegisterViewModel, ApplicationUser>();
            
            //
            CreateMap<GLCategory, GLCategoryDisplayViewModel>().
                BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id)).
                ForMember(dest => dest.MainCategoryName,opt => opt.MapFrom(src => Enum.GetName(typeof(MainCategoriesEnum),src.MainCategoriesId)));
            CreateMap<GLCategoryViewModel, GLCategory>();
            CreateMap<GLCategory, GLCategoryDropDownViewModel>();
            CreateMap<GLCategory, GLCategoryViewModel>().
                ForMember(dest => dest.MainCategoryEnum, opt => opt.MapFrom(src => (MainCategoriesEnum)src.MainCategoriesId));

            //
            CreateMap<GLAccount, GLAccountDisplayViewModel>().
                BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id)).
                ForMember(dest => dest.GLCategoryName, opt => opt.MapFrom(src => src.GLCategory.GLCategoryName)).
                ForMember(dest => dest.BranchLocation,opt => opt.MapFrom(src => src.Branch.Location));
            CreateMap<GLAccount, GLAccountViewModel>();
            CreateMap<GLAccountViewModel, GLAccount>();
            CreateMap<GLAccount, GLAccountDropDownViewModel>();


            //
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<Customer, CustomerViewModel>().
                 BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id));

            //
            CreateMap<CustomerAccount, CustomerAccountViewModel>().
                BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id)).
                ForMember(dest => dest.BranchName,opt => opt.MapFrom(src => src.Branch.Location)).
                ForMember(dest => dest.AccountTypeName, opt => opt.MapFrom(src =>Enum.GetName(typeof(Core.Entities.AccountTypeEnum), src.AccountTypeEnum)));
            CreateMap<CustomerAccountViewModel, CustomerAccount>();

            //
            CreateMap<TransactionViewModel,Transaction>();
            CreateMap<Transaction, TransactionViewModel>().
                ForMember(dest => dest.DebitGLAccountName,opt => opt.MapFrom(src => src.DebitGLAccount.GLAccountName)).
                ForMember(dest => dest.DebitGLAccountNumber, opt => opt.MapFrom(src => src.DebitGLAccount.GLAccountCode)).
                ForMember(dest => dest.CreditGLAccountName, opt => opt.MapFrom(src => src.CreditGLAccount.GLAccountName)).
                ForMember(dest => dest.CreditGLAccountNumber, opt => opt.MapFrom(src => src.CreditGLAccount.GLAccountCode));
            CreateMap<TellerPostViewModel, TellerPost>();
            CreateMap<TellerPost, TellerPostViewModel>().
                ForMember(dest => dest.CustomerAccountName, opt => opt.MapFrom(src => src.CustomerAccount.CAccountName)).
                ForMember(dest => dest.CustomerAccountNumber, opt => opt.MapFrom(src => src.CustomerAccount.CAccountNumber));

            //Customer Account Configuration Mappings
            CreateMap<AccountConfiguration_Current, ConfigCurrentViewModel>().ReverseMap();
            CreateMap<AccountConfiguration_Savings, ConfigSavingsViewModel>().ReverseMap();
            CreateMap<AccountConfiguration_Loan, ConfigLoanViewModel>().ReverseMap();

            CreateMap<LoanAccount, LoanAccountViewModel>().
                BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id));
            CreateMap<LoanAccountViewModel, LoanAccount>();
            CreateMap<LoanAccount, LoanAccountDisplayViewModel>().
                BeforeMap((s, d) => s.EncryptedId = Encrypt.Encode(s.Id)).
                 ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Location));


            //Financial Reports Mapping
            CreateMap<GLAccount, FinancialReportGLAccount>();
            CreateMap<CustomerAccount, FinancialReportCustomerAccount>();
            CreateMap<LoanAccount, FinancialReportLoanAccount>();
        }
    }
 
}
