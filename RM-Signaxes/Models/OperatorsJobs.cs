using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RM_Signaxes.Models
{
    public class OperatorsJobs
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset Opened { get; set; }
        public DateTimeOffset? Closed { get; set; }

        public int? OperatorID { get; set; }

        [ForeignKey("OperatorID")]
        public virtual Actor Operator { get; set; }

        public int? MachineID { get; set; }

        [ForeignKey("MachineID")]
        public virtual Machine Machine { get; set; }
        public int? JobID { get; set; }

        [ForeignKey("JobID")]
        public virtual Job Job { get; set; }
        public double? TotalSpentSeconds { get; set; }
        public String Status { get; set; }
        public String Description { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public double? TotalActualSpentHours { get { return (Opened - (Closed.HasValue ? Closed.Value : DateTime.Now)).TotalSeconds; } set { /* needed for EF */ } }
        public bool? IsDelete { get; set; }
    }
    public static class JobStatus{
        public static string Open = "Open";
        public static string Close = "Close";
    }
}