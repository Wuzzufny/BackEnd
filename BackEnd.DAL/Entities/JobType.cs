using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class JobType
    {
        public int Id { get; set; }
        public string jobtype { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
