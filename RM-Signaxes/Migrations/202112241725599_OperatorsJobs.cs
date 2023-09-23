namespace RM_Signaxes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OperatorsJobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OperatorsJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Opened = c.DateTimeOffset(nullable: false, precision: 7),
                        Closed = c.DateTimeOffset(precision: 7),
                        OperatorID = c.Int(),
                        MachineID = c.Int(),
                        JobID = c.Int(),
                        SpentHours = c.Time(precision: 7),
                        Status = c.String(),
                        Description = c.String(),
                        ActualSpentHours = c.Time(precision: 7),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobID)
                .ForeignKey("dbo.Machines", t => t.MachineID)
                .ForeignKey("dbo.Actors", t => t.OperatorID)
                .Index(t => t.OperatorID)
                .Index(t => t.MachineID)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperatorsJobs", "OperatorID", "dbo.Actors");
            DropForeignKey("dbo.OperatorsJobs", "MachineID", "dbo.Machines");
            DropForeignKey("dbo.OperatorsJobs", "JobID", "dbo.Jobs");
            DropIndex("dbo.OperatorsJobs", new[] { "JobID" });
            DropIndex("dbo.OperatorsJobs", new[] { "MachineID" });
            DropIndex("dbo.OperatorsJobs", new[] { "OperatorID" });
            DropTable("dbo.OperatorsJobs");
        }
    }
}
