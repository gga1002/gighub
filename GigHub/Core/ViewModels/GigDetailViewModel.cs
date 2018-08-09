using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailViewModel
    {
        public Gig Gig { get; set; }
        public bool ShowActions { get; set; }
        public bool Following { get; set; }
    }
}