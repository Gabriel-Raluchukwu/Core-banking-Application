namespace BankTwo.Application.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Branches] ON
INSERT INTO [dbo].[Branches] ([Id], [Location], [Address], [DateAdded], [DateLastUpdated]) VALUES (3, N'Ikoyi', N'51, Awolowo Road, Ikoyi, Lagos State, Nigeria', N'2001-01-01 00:00:00', N'2001-01-01 00:00:00')
SET IDENTITY_INSERT [dbo].[Branches] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
