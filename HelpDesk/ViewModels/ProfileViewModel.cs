using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace HelpDesk.ViewModels
{
    public class ProfileViewModel
    {
        public ICollection<SystemTask> SystemTasks { get; set; }

        public ICollection<IdentityRole> SystemRoles { get; set; }

        public ICollection<int> RightsIdsAssigned { get; set; }

        [DisplayName("Role Name")]
        public string RoleId { get; set; }

        [DisplayName("System Task")]
        public string TaskId { get; set; }
    }
}
