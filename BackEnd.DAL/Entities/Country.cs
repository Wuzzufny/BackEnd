using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public virtual Company Company { get; set; }

    }
}
