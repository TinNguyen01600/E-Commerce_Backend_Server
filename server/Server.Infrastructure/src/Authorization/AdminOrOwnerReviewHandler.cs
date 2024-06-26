using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Server.Core.src.ValueObject;
using Server.Service.src.DTO;

namespace Server.Infrastructure.src.Authorization
{
    public class AdminOrOwnerReviewHandler : AuthorizationHandler<AdminOrOwnerReviewRequirement, ReviewReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerReviewRequirement requirement, ReviewReadDTO review)
        {
            var claims = context.User;
            var userRole = claims.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

            if (userId == review.User.Id.ToString() || userRole == Role.Admin.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class AdminOrOwnerReviewRequirement : IAuthorizationRequirement { }
}