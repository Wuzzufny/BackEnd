using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models.DTOs.Response
{
     public class CompanyResponseDto : BaseReponseDto
    {
        public string CompanyName { get; set; }
        public string CompanyImage { get; set; }
    }
}
