namespace RM_Signaxes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "AddedByID", "dbo.Actors");
            DropIndex("dbo.Actors", new[] { "AddedByID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Actors", "AddedByID");
            AddForeignKey("dbo.Actors", "AddedByID", "dbo.Actors", "Id");
        }
    }
}
