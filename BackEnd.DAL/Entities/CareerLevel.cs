using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class CareerLevel
    {
        public int Id { get; set; }
        public string careerlevel { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

    }
}
