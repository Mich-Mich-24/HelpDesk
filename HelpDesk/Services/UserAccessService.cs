using System.Security.Claims;

namespace HelpDesk.Services
{
    public static class UserAccessService
    {
       
        public static string GetUserId(this ClaimsPrincipal user)
        {
          if(!user.Identity.IsAuthenticated)
          {
                return null;
          }
          else
          {
             ClaimsPrincipal currentLoggedinUser = user;
             if(currentLoggedinUser != null)
             {
                    return currentLoggedinUser.FindFirst(ClaimTypes.NameIdentifier).Value;
             }
             else
             {
                    return null;
             }
          }
        }
        public static string GetUserName(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }
            else
            {
                ClaimsPrincipal currentLoggedinUser = user;
                if (currentLoggedinUser != null)
                {
                    return currentLoggedinUser.FindFirstValue(ClaimTypes.Name);
                }
                else
                {
                    return null;
                }
            }
        }
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }
            else
            {
                ClaimsPrincipal currentLoggedinUser = user;
                if (currentLoggedinUser != null)
                {
                    return currentLoggedinUser.FindFirstValue(ClaimTypes.Email);
                }
                else
                {
                    return null;
                }
            }
        }
        public static string GetUserRole(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }
            else
            {
                ClaimsPrincipal currentLoggedinUser = user;
                if (currentLoggedinUser != null)
                {
                    return currentLoggedinUser.FindFirst("RoleId").Value;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
