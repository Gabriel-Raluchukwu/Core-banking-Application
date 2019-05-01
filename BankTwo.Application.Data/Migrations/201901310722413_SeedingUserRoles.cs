namespace BankTwo.Application.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [EncryptedId], [FirstName], [LastName], [OtherNames], [UserRole], [GLAccountId], [BranchId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6f1b84da-713d-46a3-81c3-e9bf7f1af930', N'NmYxYjg0ZGEtNzEzZC00NmEzLTgxYzMtZTliZjdmMWFmOTMw', N'Raluchukwu', N'Mbaebie', N'Gabriel', N'Super Administrator', NULL, 3, N'raluchukwu.mbaebie@gmail.com', 0, N'AOy4z60iYHYi79IHiZBA+tq8yXJtXCWRoAUEUBHmWGfCosXUUS4pLkZFloNHjOxWoQ==', N'b7d53243-f9de-4868-8d5e-441013b548ed', N'08136179852', 0, 0, NULL, 1, 0, N'Gabriel')
");
            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6f1b84da-713d-46a3-81c3-e9bf7f1af930', N'7ede6972-557e-4df1-9ce1-6d8bed50e9a2')");
        }
        
        public override void Down()
        {
        }
    }
}
