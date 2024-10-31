using System.ComponentModel;

namespace HelpDesk.ViewModels
{
    public class RolesViewModel
    {
        [DisplayName("Role No")]
        public string Id { get; set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}
