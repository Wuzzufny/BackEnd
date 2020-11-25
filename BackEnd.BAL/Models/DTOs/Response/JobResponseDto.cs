using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models.DTOs.Response
{
    public class JobResponseDto :BaseReponseDto
    {
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public int MinYearsEx { get; set; }
        public int MaxYearsEx { get; set; }
        public string skills { get; set; }
        public string ReceiveApplicants { get; set; }
        public JobTypeResponseDto JobTypeResponse { get; set; }
        public CareerLevelResponseDto careerLevel { get; set; }
        public JobRoleResponseDto jobRole { get; set; }
        public CompanyResponseDto companyResponse { get; set; }
        public List <JobQuestionsDto> JobQuestionsDto { get; set; }
       
    }
}


