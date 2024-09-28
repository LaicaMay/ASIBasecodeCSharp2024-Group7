using ExpenseTracker.Data.Data;
using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker
{
    public class RolesInDBAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        public RolesInDBAuthorizationHandler() { }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement); // Succeed if the user is authenticated
            }
            else
            {
                context.Fail(); // Fail if the user is not authenticated
            }

            return Task.CompletedTask;
        }
    }
}

