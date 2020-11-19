using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class StaticPages
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string PageContent { get; set; }
    }
}
