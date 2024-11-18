using System.ComponentModel;

namespace HelpDesk.Models
{
    public class SystemTask : UserActivity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public SystemTask Parent {  get; set; }

        public ICollection<SystemTask> ChildTasks { get; }

        [DisplayName("Order No")]
        public int? OrderNumber {  get; set; } 


    }
}
