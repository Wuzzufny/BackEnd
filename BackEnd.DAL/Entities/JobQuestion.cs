using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class JobQuestion
    {
        public int Id { get; set; }
        public string jobquestions { get; set; }
        [ForeignKey("Job")]
        public int  jobid { get; set; }
        public virtual Job Job { get; set; }

    }
}
