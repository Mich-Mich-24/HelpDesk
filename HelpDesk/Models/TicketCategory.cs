using System.ComponentModel;

namespace HelpDesk.Models
{
    public class TicketCategory : UserActivity
    {
        [DisplayName("Category No")]
        public int Id { get; set; }
        [DisplayName("Category Code")]
        public string Code { get; set; }

        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
