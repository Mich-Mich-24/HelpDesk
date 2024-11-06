using System.ComponentModel;

namespace HelpDesk.Models
{
    public class Ticket
    {
        [DisplayName("No")]
        public int Id { get; set; }


        [DisplayName("Title")]
        public string Title { get; set; }


        [DisplayName("Description")]
        public string Description { get; set; }


        [DisplayName("Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }


        [DisplayName("Priority")]
        public int PriorityId { get; set; }
        public SystemCodeDetail Priority { get; set; }


        [DisplayName("Created By")]
        public string CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }


        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }


        [DisplayName("Ticket Sub-Category")]
        public int? SubCategoryId { get; set; }
        public TicketSubCategory SubCategory { get; set; }


        [DisplayName("Document Attachments")]
        public string Attachment { get; set; }

        public ICollection<Comment> TicketComments { get; set; }

        public int? AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }

        public DateTime? AssignedOn { get; set; }
    }
}
