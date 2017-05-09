namespace Freelance.Provider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_setting_imageName_to_profile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "ImageName");
        }
    }
}
