namespace BankTwo.Application.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seedingroles : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd748852c-971b-4b44-8e84-681e35bb0d59', N'Administrator')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e2bfc570-18c6-4038-a646-1b0566d7bcf9', N'Customer Care')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7ede6972-557e-4df1-9ce1-6d8bed50e9a2', N'Super Administrator')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'834d406c-eec7-46c1-b5a2-01e40c3a4c2d', N'Teller')
");
        }
        
        public override void Down()
        {
        }
    }
}
