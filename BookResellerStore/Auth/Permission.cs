using BookResellerStore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookResellerStore.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class Permission : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly List<string> _roleRights = new List<string>();

        public Permission(params string[] roleRights)
        {
            if (roleRights != null)
            {
                _roleRights.AddRange(roleRights.Select(item => item.ToString()).ToList());
            }
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_roleRights.Any())
            {
                var hasRoleRight = _roleRights.Where(right => context.HttpContext.User.IsInRight(right)).ToList();
                if (!hasRoleRight.Any())
                {
                    //context.HttpContext.Response.Redirect("/Error/NoPermission");
                    context.Result = new RedirectToRouteResult
                        (
                        new RouteValueDictionary(new
                        {
                            action = "NoPermission",
                            controller = "Error"
                        }));
                    return;
                }
            }
        }
    }
}
