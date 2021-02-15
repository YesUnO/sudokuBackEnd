using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using sudokuBackEnd.DB.Data;
using sudokuBackEnd.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace sudokuBackEnd.APICore
{
    public class RestApiController : ControllerBase
    {
       protected int LoggedInUser
        {
            get
            {
                try
                {
                    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return userId;
                }
                catch
                {
                    return 0;
                }


            }
        }

    }
}
