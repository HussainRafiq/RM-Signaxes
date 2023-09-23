using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RM_Signaxes.Models
{
    public class Actor
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
        public string SkillLevel { get; set; }
        public string Department { get; set; }
        public bool? IsActive { get; set; }
        public int? AddedByID { get; set; }
        public bool? IsDelete { get; set; }
        public virtual List<OperatorsJobs> Jobs { get; set; }
    }
}
