using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpDesk.ClaimsManagement
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MyUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
            _context = context;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            var userrole = userRoles.FirstOrDefault();

            var allUserPermisons = "";

            if(userrole != null)
            {
                var userRole = await _context.Roles.SingleAsync(i => i.Name == userrole);

                var userPersmisions = _context.UserRoleProfiles.Where(p=>p.RoleId==userRole.Id).Select(p=>p.Task.Parent.Name + " " + p.Task.Name).ToList();

                foreach(var permission in userPersmisions)
                {
                    allUserPermisons += $"@{permission}";
                }
            }
            identity.AddClaim(new Claim("UserPermisssion", allUserPermisons));

            return identity;
        }

    }
}
