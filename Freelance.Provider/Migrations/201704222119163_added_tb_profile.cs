namespace Freelance.Provider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_tb_profile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(),
                        CategoryId = c.Guid(nullable: false),
                        DescriptionProfile = c.String(nullable: false),
                        TimeFrom = c.Time(nullable: false, precision: 7),
                        TimeTo = c.Time(nullable: false, precision: 7),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profiles");
        }
    }
}
