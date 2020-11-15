using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.ApiRoute
{
  public static class ApiRoute
  {
    public const string Root= "api";
    public const string Version = "v1";
    public const string Base =Root+"/"+Version;

    public static class Identity
    {
      public const string Login = Base + "Identity/Login";
      public const string Register = Base + "/Identity/Register";
      public const string Roles = Base + "/Identity/Roles";
      public const string ForgotPassword = Base + "/Identity/ForgotPassword";
      public const string ResetPassword = Base + "/Identity/ResetPassword";
    }

        public static class Register
        {
            public const string Employer = Base + "/Register/Employer";
        }

    }
}
