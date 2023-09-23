using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RM_Signaxes.Models
{
    public class Job
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public int? AddedByID { get; set; }

        [ForeignKey("AddedByID")]
        public virtual Actor AddedBy { get; set; }
        public bool? IsDelete { get; set; }
        public virtual List<OperatorsJobs> Jobs { get; set; }
    }
}