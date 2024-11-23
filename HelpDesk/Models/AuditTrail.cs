using System.ComponentModel;

namespace HelpDesk.Models
{
    public class AuditTrail
    {
        [DisplayName("No")]
        public int Id { get; set; }

        [DisplayName("Action Type")]
        public string Action {  get; set; }

        [DisplayName("Module")]
        public string Module { get; set; }

        [DisplayName("Affected Table")]
        public string AffectedTable { get; set; }

        [DisplayName("Created On")]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [DisplayName("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [DisplayName("Old Values")]
        public string? OldValues { get; set; }

        [DisplayName("New Values")]
        public string? NewValues { get; set; }

        [DisplayName("Affected Colmns")]
        public string? AffectedColumns { get; set; }

        [DisplayName("Primary Key")]
        public string? PrimaryKey { get; set; }
    }
}
