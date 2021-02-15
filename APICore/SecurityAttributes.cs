using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace sudokuBackEnd.APICore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class SecurityAttributes : ActionFilterAttribute
    {
        private static string Authority => ConfigurationManager.AppSettings["AuthAuthority"];
    }
}
