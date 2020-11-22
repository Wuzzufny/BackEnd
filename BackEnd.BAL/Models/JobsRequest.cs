using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
    public class JobsRequest
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public int JobTypeId { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public string CompanyImage { get; set; }
        public virtual CareerLevelRequest CareerLevel { get; set; }
        public int MinYearsEx { get; set; }
        public int MaxYearsEx { get; set; }
        public virtual JobRoleRequest JobRole { get; set; }
        public string JobLocation { get; set; }
        public virtual JobQuestionsRequest JobQuestions { get; set; }
        public string skills { get; set; }
        public string ReceiveApplicants { get; set; }
    }
}



