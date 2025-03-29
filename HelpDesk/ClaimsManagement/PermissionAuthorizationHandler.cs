using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Security.Claims;

namespace HelpDesk.ClaimsManagement
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
        where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attributes = new List<PermissionAttribute>();

            var action = (context.Resource as Endpoint)?.Metadata.ToList();

            if (action != null)
            {
                var perm = action.FirstOrDefault(i => i.GetType().FullName == typeof(PermissionAttribute).FullName) as PermissionAttribute;
                if (perm != null)
                {
                    attributes.Add(perm);
                }
            }
            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, IEnumerable<PermissionAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
    }

    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        // Add any custom requirements
    }

    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Name { get; }

        public PermissionAttribute(string name) : base("Permission")
        {
            Name = name;
        }
    }

    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, IEnumerable<PermissionAttribute> attributes)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
                return;

            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(context.User, permissionAttribute.Name))
                {
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(ClaimsPrincipal user, string permission)
        {
            var userProfiles = user.FindFirstValue("UserProfile")?.ToLower();
            if (string.IsNullOrEmpty(userProfiles))
                return Task.FromResult(false);

            try
            {
                if (userProfiles.Contains(permission.ToLower()))
                {
                    return Task.FromResult(true);
                }
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(false);
        }
    }
}