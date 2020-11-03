﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagment.ServiceLayer;
using Microsoft.AspNet.Identity;

namespace LeaveManagement.Filters
{
    public class SpecialPermissionAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
           
            if (filterContext.HttpContext.User.IsInRole("HR") == false)
            {
                IHrRoleService hr = new HrRoleService();
                var user = hr.GetHRByEmployeeID(filterContext.HttpContext.User.Identity.GetUserId());
                var SpecialPermission = user.IsSpecialPermission;
                if (SpecialPermission == false)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}