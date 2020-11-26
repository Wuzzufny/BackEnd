using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Job
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int MinYearsEx { get; set; }
        public int MaxYearsEx { get; set; }
        public string JobLocation { get; set; }
        public string skills { get; set; }
        public string ReceiveApplicants { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("JobType")]
        public int JobTypeId { get; set; }
        public virtual JobType JobType { get; set; }   //FullTime - PartTime
        [ForeignKey("CareerLevel")]
        public int CareerLevelId { get; set; }
        public virtual CareerLevel CareerLevel { get; set; }    // Senior  Junior  /// career path
        public virtual Company Company { get; set; }
        [ForeignKey("JobRole")]
        public int JobRoleId { get; set; }
        public virtual JobRole JobRole { get; set; }    // JobRole is The Department of job HR-Finance
        public virtual ICollection<JobQuestion> JobQuestions { get; set; } // one job has many questions


    }
}
