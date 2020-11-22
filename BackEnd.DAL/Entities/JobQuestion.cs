using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class JobQuestion
    {
        public int Id { get; set; }
        public string jobquestions { get; set; }
        public virtual Job job { get; set; }

    }
}
