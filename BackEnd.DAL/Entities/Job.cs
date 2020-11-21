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
        [ForeignKey("JobType")]
        public int JobTypeId { get; set; }
        public  virtual JobType JobType { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public string CompanyImage { get; set; }
        [ForeignKey("CareerLevel")]
        public int CareerLevelId { get; set; }
        public virtual CareerLevel CareerLevel { get; set; }
        public int MinYearsEx { get; set; }
        public int MaxYearsEx { get; set; }
        [ForeignKey("JobRole")]
        public int JobRoleId { get; set; }
        public virtual JobRole JobRole { get; set; }
        public string JobLocation { get; set; }
        [ForeignKey("JobQuestions")]
        public int JobQuestionsId { get; set; }
        public virtual JobQuestions JobQuestions { get; set; }
        public string skills { get; set; }
        public string ReceiveApplicants { get; set; }
    }
}
