using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class JobQuestions
    {
        public int Id { get; set; }
        public string jobquestions { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

    }
}
