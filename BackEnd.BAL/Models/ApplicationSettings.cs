using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class ApplicationSettings
  {
    public string JwtSecret { get; set; }

    public string ClientUrl { get; set; }

    public string ReportConnection { get; set; }
  }
}
