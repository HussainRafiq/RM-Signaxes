namespace RM_Signaxes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperatorsJobs", "TotalSpentHours", c => c.Double());
            AddColumn("dbo.OperatorsJobs", "TotalActualSpentHours", c => c.Double());
            DropColumn("dbo.OperatorsJobs", "SpentHours");
            DropColumn("dbo.OperatorsJobs", "ActualSpentHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OperatorsJobs", "ActualSpentHours", c => c.Time(precision: 7));
            AddColumn("dbo.OperatorsJobs", "SpentHours", c => c.Time(precision: 7));
            DropColumn("dbo.OperatorsJobs", "TotalActualSpentHours");
            DropColumn("dbo.OperatorsJobs", "TotalSpentHours");
        }
    }
}
