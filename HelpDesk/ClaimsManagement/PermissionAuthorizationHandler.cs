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
            //Retrieve the attributes associated with the current endpoint
            var attributes = GetAttributesFromEndpoint(context.Resource);

            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TRequirement requirement, 
            IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributesFromEndpoint(object resource)
        {
            var endpoint = resource as Endpoint;
            if(endpoint != null)
            {
                return endpoint.Metadata.OfType<TAttribute>().ToList();
            }

            return Enumerable.Empty<TAttribute>();
        }
    }

    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        // Add any custom requirements properties or methods if needed
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
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
            if (attributes == null || !attributes.Any())
            {
                context.Fail();
                return;
            }

            foreach (var permissionAttribute in attributes)
            {
                var hasPermission = await AuthorizeAsync(context.User, permissionAttribute.Name);
                if (!hasPermission)
                {
                    context.Fail();
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(ClaimsPrincipal user, string permission)
        {
            var userPermissions = user.FindFirstValue("UserPermission")?.ToLower();
           
            var haspermission = Task.FromResult(userPermissions != null && userPermissions.Contains(permission));

            return haspermission;
        }
    }
}