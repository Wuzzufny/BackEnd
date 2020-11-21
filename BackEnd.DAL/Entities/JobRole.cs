using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class JobRole
    {
        public int Id { get; set; }
        public string jobrole { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
