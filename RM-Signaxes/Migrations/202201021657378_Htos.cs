namespace RM_Signaxes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Htos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperatorsJobs", "TotalSpentSeconds", c => c.Double());
            DropColumn("dbo.OperatorsJobs", "TotalSpentHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OperatorsJobs", "TotalSpentHours", c => c.Double());
            DropColumn("dbo.OperatorsJobs", "TotalSpentSeconds");
        }
    }
}
