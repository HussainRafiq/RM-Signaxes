namespace RM_Signaxes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(),
                        Designation = c.String(),
                        SkillLevel = c.String(),
                        Department = c.String(),
                        IsActive = c.Boolean(),
                        AddedByID = c.Int(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.AddedByID)
                .Index(t => t.AddedByID);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        AddedByID = c.Int(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.AddedByID)
                .Index(t => t.AddedByID);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Make = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Area = c.String(),
                        Department = c.String(),
                        IsActive = c.Boolean(),
                        AddedByID = c.Int(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.AddedByID)
                .Index(t => t.AddedByID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Machines", "AddedByID", "dbo.Actors");
            DropForeignKey("dbo.Jobs", "AddedByID", "dbo.Actors");
            DropForeignKey("dbo.Actors", "AddedByID", "dbo.Actors");
            DropIndex("dbo.Machines", new[] { "AddedByID" });
            DropIndex("dbo.Jobs", new[] { "AddedByID" });
            DropIndex("dbo.Actors", new[] { "AddedByID" });
            DropTable("dbo.Machines");
            DropTable("dbo.Jobs");
            DropTable("dbo.Actors");
        }
    }
}
