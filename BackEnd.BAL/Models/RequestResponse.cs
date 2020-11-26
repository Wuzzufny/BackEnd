using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
    public class RequestResponse
    {
        public bool Success { get; set; }
        public object Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
