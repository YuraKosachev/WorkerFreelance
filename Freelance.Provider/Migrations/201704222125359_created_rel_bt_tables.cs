namespace Freelance.Provider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created_rel_bt_tables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Profiles", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Profiles", "UserId");
            CreateIndex("dbo.Profiles", "CategoryId");
            CreateIndex("dbo.Offers", "ProfileId");
            CreateIndex("dbo.Offers", "UserId");
            AddForeignKey("dbo.Profiles", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Offers", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Offers", "UserId", "dbo.User", "Id");
            AddForeignKey("dbo.Profiles", "UserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "UserId", "dbo.User");
            DropForeignKey("dbo.Offers", "UserId", "dbo.User");
            DropForeignKey("dbo.Offers", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Offers", new[] { "UserId" });
            DropIndex("dbo.Offers", new[] { "ProfileId" });
            DropIndex("dbo.Profiles", new[] { "CategoryId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            AlterColumn("dbo.Profiles", "UserId", c => c.String());
            AlterColumn("dbo.Offers", "UserId", c => c.String());
        }
    }
}
