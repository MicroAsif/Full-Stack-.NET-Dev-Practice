namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addConcelToGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "IsCancel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gigs", "IsCancel");
        }
    }
}
