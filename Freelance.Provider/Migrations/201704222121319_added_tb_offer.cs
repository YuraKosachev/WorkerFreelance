namespace Freelance.Provider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_tb_offer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProfileId = c.Guid(nullable: false),
                        UserId = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        DateOfCreate = c.DateTime(nullable: false),
                        FreelancerConfirm = c.Boolean(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Offers");
        }
    }
}
