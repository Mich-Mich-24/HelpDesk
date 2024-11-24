using AutoMapper;
using HelpDesk.Models;
using HelpDesk.ViewModels;

namespace HelpDesk.Services
{
    public class AutomapperProfileService : Profile
    {
        public AutomapperProfileService()
        {
            CreateMap<TicketViewModel, Ticket>().ReverseMap();
            CreateMap<SystemCodeViewModel, SystemCode>().ReverseMap();
        }
    }
}
